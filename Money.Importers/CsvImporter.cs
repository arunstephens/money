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
    public class CsvImporter<T> : IImporter where T : IBankTransaction, new()
    {
        private bool _hasHeaderRow = true;
        private bool _skipFirstRow = false;

        public CsvImporter(bool hasHeaderRow, bool skipFirstRow)
        {
            _hasHeaderRow = hasHeaderRow;
            _skipFirstRow = skipFirstRow;
        }

        public async IAsyncEnumerable<Transaction> Import(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = _hasHeaderRow;

                if (_skipFirstRow)
                {
                    await csv.ReadAsync();
                }

                T placeholder = new T();

                await foreach (var bankTx in csv.EnumerateRecordsAsync<T>(placeholder))
                {
                    var tx = bankTx.ToTransaction();

                    yield return tx;
                }
            }
        }
    }
}
