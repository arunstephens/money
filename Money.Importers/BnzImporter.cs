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
    public class BnzImporter : IImporter
    {
        public async IAsyncEnumerable<Transaction> Import(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                BnzTransaction placeholder = new BnzTransaction();

                await foreach (var bnzt in csv.EnumerateRecordsAsync<BnzTransaction>(placeholder))
                {
                    yield return bnzt.ToTransaction();
                }
            }
        }
    }
}