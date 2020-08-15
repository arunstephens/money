using Money.Data;
using Money.Data.Model;
using Money.Importers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money
{
    class Program
    {
        static MoneyContext _context = new MoneyContext();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var accounts = await GetOrCreateAccounts();

            Func<int, Func<string, Account>> accountGetter = accountId => _ => accounts.Single(a => a.Number == accountId.ToString());

            ITransactionImporter importer;

            importer = new CsvTransactionImporter<Importers.Model.BnzTransaction>(true, false);

            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\Joint---Main-9AUG2018-to-9AUG2020.csv", accountGetter(1)));

            importer = new CsvTransactionImporter<Importers.Model.KiwibankCreditCardTransaction>(false, true);

            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\4833-48 - -3016_10Aug.CSV", accountGetter(2)));

            importer = new CsvTransactionImporter<Importers.Model.KiwibankBankTransaction>(true, false);

            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-02_10Aug.CSV", accountGetter(3)));
            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-01_15Aug.CSV", accountGetter(4)));
            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-03_15Aug.CSV", accountGetter(5)));

            importer = new CsvTransactionImporter<Importers.Model.AsbBankTransaction>(false, false, true);

            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\Export20200815213103.csv", accountGetter(6)));

            importer = new CsvTransactionImporter<Importers.Model.AsbCreditCardTransaction>(false, false, true);

            await InsertOrUpdateTransactions(importer.Import(@"C:\Users\a\Documents\Money\Export20200815213632.csv", accountGetter(7)));
        }

        private static async Task WriteTransactions(IAsyncEnumerable<Transaction> transactions)
        {
            await foreach (var tx in transactions)
            {
                Console.WriteLine($"{PadOrTruncate(tx.Account.Name, 20)} {tx.TransactionDate:yyyy-MM-dd} {PadOrTruncate(tx.PayeeName, 40)} {tx.Amount,10:0.00} {tx.ExternalId}");
            }
        }

        private static async Task InsertOrUpdateTransactions(IAsyncEnumerable<Transaction> transactions)
        {
            await foreach (var tx in transactions)
            {
                var savedTx = await _context.Transactions.SingleOrDefaultAsync(t => t.Account.Id == tx.Account.Id && t.ExternalId == tx.ExternalId);

                if (savedTx != null)
                {
                    // Ignore it
                }
                else
                {
                    await _context.AddAsync(tx);
                }
            }

            _context.SaveChanges();
        }

        private static string PadOrTruncate(string value, int maxLength)
        {
            return (value.Length < maxLength ? value : value.Substring(0, maxLength)).PadRight(maxLength);
        }

        private static async Task<IEnumerable<Account>> GetOrCreateAccounts()
        {
            var importer = new CsvAccountImporter();
            var accounts = importer.Import(@"C:\Users\a\Documents\Money\accounts.csv");

            await foreach (var account in accounts)
            {
                var savedAccount = _context.Accounts.SingleOrDefault(a => a.Number == account.Number);

                if (savedAccount == null)
                {
                    _context.Accounts.Add(account);
                }
                else
                {
                    savedAccount.Name = account.Name;
                }
            }

            await _context.SaveChangesAsync();

            return _context.Accounts;
        }
    }
}
