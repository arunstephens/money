using CsvHelper.Configuration;
using Money.Data.Model;
using Money.Importers.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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

        public async IAsyncEnumerable<Transaction> Import(string filename, Func<string, Account> accountMapper)
        {
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

            if (_skipFirstRow)
            {
                await csv.ReadAsync();
            }

            T placeholder = new T();

            await foreach (var bankTx in csv.EnumerateRecordsAsync<T>(placeholder))
            {
                var tx = bankTx.ToTransaction();

                tx.AccountId = accountMapper(null).Id;

                yield return tx;
            }
        }
    }
}
