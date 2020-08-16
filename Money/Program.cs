using Dapper;
using Dapper.Contrib.Extensions;
using Money.Data;
using Money.Data.Model;
using Money.Importers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Money
{
    class Program
    {
        const string ConnectionString = @"Server=.\SQLEXPRESS;Trusted_Connection=True;MultipleActiveResultSets=true;Database=MoneyDB";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Arun's Money App");

            //var accounts = await GetOrCreateAccounts();

            //Func<int, Func<string, Account>> accountGetter = accountId => _ => accounts.Single(a => a.Number == accountId.ToString());

            //ITransactionImporter importer;

            //importer = new CsvTransactionImporter<Importers.Model.BnzTransaction>(true, false);

            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\Joint---Main-9AUG2018-to-9AUG2020.csv", accountGetter(1)));

            //importer = new CsvTransactionImporter<Importers.Model.KiwibankCreditCardTransaction>(false, true);

            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\4833-48 - -3016_10Aug.CSV", accountGetter(2)));

            //importer = new CsvTransactionImporter<Importers.Model.KiwibankBankTransaction>(true, false);

            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-02_10Aug.CSV", accountGetter(3)));
            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-01_15Aug.CSV", accountGetter(4)));
            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-03_15Aug.CSV", accountGetter(5)));

            //importer = new CsvTransactionImporter<Importers.Model.AsbBankTransaction>(false, false, true);

            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\Export20200815213103.csv", accountGetter(6)));

            //importer = new CsvTransactionImporter<Importers.Model.AsbCreditCardTransaction>(false, false, true);

            //await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\Export20200815213632.csv", accountGetter(7)));

            await AssignPayees();
        }

        private static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        private static async Task WriteTransactions(IAsyncEnumerable<Transaction> transactions)
        {
            await foreach (var tx in transactions)
            {
                Console.WriteLine($"{PadOrTruncate(tx.Account.Name, 20)} {tx.TransactionDate:yyyy-MM-dd} {PadOrTruncate(tx.PayeeName, 40)} {tx.Amount,10:0.00} {tx.ExternalId}");
            }
        }

        private static async Task<Transaction> GetTransaction(int accountId, string externalId)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstAsync<Transaction>("SELECT * FROM Transactions WHERE AccountId = @accountId AND ExternalId = @externalId",
                new { accountId, externalId });
        }

        private static async Task InsertTransaction(Transaction tx)
        {
            using var connection = GetConnection();

            await connection.InsertAsync(tx);
        }

        private static async Task InsertOrUpdateTransactions(IAsyncEnumerable<Transaction> transactions)
        {
            await foreach (var tx in transactions)
            {
                var savedTx = await GetTransaction(tx.Account.Id, tx.ExternalId);

                if (savedTx != null)
                {
                    // Ignore it
                }
                else
                {
                    await InsertTransaction(tx);
                }
            }
        }

        private static string PadOrTruncate(string value, int maxLength)
        {
            return (value.Length < maxLength ? value : value.Substring(0, maxLength)).PadRight(maxLength);
        }

        private static async Task<Account> GetAccount(string accountNumber)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstAsync<Account>("SELECCT * FROM Accounts WHERE Number = @accountNumber",
                new { accountNumber });
        }

        private static async Task InsertAccount(Account account)
        {
            using var connection = GetConnection();

            await connection.InsertAsync(account);
        }

        private static async Task UpdateAccount(Account account)
        {
            using var connection = GetConnection();

            await connection.UpdateAsync(account);
        }

        private static async Task<IEnumerable<Account>> GetAccounts()
        {
            using var connection = GetConnection();

            return await connection.GetAllAsync<Account>();
        }

        private static async Task<IEnumerable<Account>> GetOrCreateAccounts()
        {
            var importer = new CsvAccountImporter();
            var accounts = importer.Import(@"C:\Users\a\Documents\Money\accounts.csv");

            await foreach (var account in accounts)
            {
                var savedAccount = await GetAccount(account.Number);

                if (savedAccount == null)
                {
                    await InsertAccount(account);
                }
                else
                {
                    savedAccount.Name = account.Name;
                    await UpdateAccount(savedAccount);
                }
            }

            return await GetAccounts();
        }

        private static async Task<IEnumerable<Transaction>> GetTransactionsWithoutPayees()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<Transaction, Account, Transaction>("SELECT * FROM Transactions t INNER JOIN Accounts a ON t.AccountId = a.Id WHERE PayeeId IS NULL", 
                (tx, account) =>
                {
                    tx.Account = account;

                    return tx;
                });
        }

        private static async Task UpdateTransactionPayee(int transactionId, int payeeId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync("UPDATE Transactions SET PayeeId = @payeeId WHERE Id = @transactionId",
                new { payeeId, transactionId });
        }

        private static async Task AssignPayees()
        {
            foreach (var tx in await GetTransactionsWithoutPayees())
            {
                var payeeId = await LookupPayeeId(tx.PayeeName);

                if (payeeId == null)
                {
                    payeeId = await AskForPayeeId(tx.PayeeName, tx.Account.Name);
                }

                await UpdateTransactionPayee(tx.Id, payeeId.Value);
            }
        }

        private static async Task<int?> LookupPayeeId(string payeeName)
        {
            using var connection = GetConnection();

            // First, alternate names
            var payeeId = await connection.QueryFirstOrDefaultAsync<int?>("SELECT Id FROM Payees WHERE Name = @payeeName UNION SELECT PayeeId FROM PayeeAlternateNames WHERE Name = @payeeName",
                new { payeeName });

            return payeeId;

            // Second, machine learning
        }

        private static async Task<int> InsertPayee(string name, string alternateName)
        {
            using var connection = GetConnection();

            var payee = new Payee
            {
                Name = name
            };

            var payeeId = await connection.InsertAsync(payee);

            await AddAlternatePayeeName(payeeId, alternateName);

            return payeeId;
        }
        
        private static async Task AddAlternatePayeeName(int payeeId, string name)
        {
            using var connection = GetConnection();

            var alternateNameEntity = new PayeeAlternateName
            {
                Name = name,
                PayeeId = payeeId,
            };

            await connection.InsertAsync(alternateNameEntity);
        }

        private static async Task<int> AskForPayeeId(string payeeName, string accountName)
        {
            Console.WriteLine($"Payee name for: {payeeName} (account {accountName})");
            Console.Write("> ");

            var newName = Console.ReadLine();

            var payeeId = await LookupPayeeId(newName);

            if (payeeId == null)
            {
                payeeId = await InsertPayee(newName, payeeName);
            }
            else
            {
                await AddAlternatePayeeName(payeeId.Value, payeeName);
            }

            return payeeId.Value;
        }

        class AlternatePayeeNameComparer : IEqualityComparer<PayeeAlternateName>
        {
            public bool Equals([AllowNull] PayeeAlternateName x, [AllowNull] PayeeAlternateName y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode([DisallowNull] PayeeAlternateName obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
