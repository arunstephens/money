using CsvHelper.Configuration;
using Money.Data.Model;
using Money.Importers.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Money.Importers
{
    public class CsvTransactionImporter<T> : ITransactionImporter where T : IBankTransaction, new()
    {
        private readonly bool _hasHeaderRow = true;
        private readonly bool _skipFirstRow = false;
        private readonly bool _waitForEmptyLine = false;

        public CsvTransactionImporter(bool hasHeaderRow = true, bool skipFirstRow = false, bool waitForEmptyLine = false)
        {
            _hasHeaderRow = hasHeaderRow;
            _skipFirstRow = skipFirstRow;
            _waitForEmptyLine = waitForEmptyLine;
        }

        public async IAsyncEnumerable<Transaction> Import(string filename, Func<Account, Task<Account>> accountMapper, Func<Category, Task<Category>> categoryMapper, Func<Payee, Task<Payee>> payeeMapper)
        {
            var dataSourceName = GetType().FullName;

            using var reader = new StreamReader(filename);

            if (_waitForEmptyLine)
            {
                string line = null;
                while (line != string.Empty)
                {
                    line = await reader.ReadLineAsync();
                }
            }

            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Configuration.HasHeaderRecord = _hasHeaderRow;
            csv.Configuration.TrimOptions = TrimOptions.Trim;

            if (_skipFirstRow)
            {
                await csv.ReadAsync();
            }

            T placeholder = new T();

            await foreach (var bankTx in csv.EnumerateRecordsAsync<T>(placeholder))
            {
                var tx = bankTx.ToTransaction();

                if (accountMapper != null)
                {
                    var account = await accountMapper(tx.Account);

                    tx.Account = account;

                    account = await accountMapper(tx.OtherAccount);
                    account.AccountType = new AccountType { Code = "EXPENSE" };

                    tx.OtherAccount = account;
                }

                if (payeeMapper != null)
                {
                    var payee = await payeeMapper(tx.Payee);

                    tx.Payee = payee;
                }

                tx.DataSource = dataSourceName;

                yield return tx;
            }
        }
    }
}
