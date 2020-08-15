using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Money.Importers
{
    public class CsvAccountImporter
    {
        public async IAsyncEnumerable<Account> Import(string filename)
        {
            using var reader = new StreamReader(filename);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Configuration.HeaderValidated = (isValid, headerName, x, y) => { return; };
            csv.Configuration.MissingFieldFound = (x, y, z) => { return; };

            Account placeholder = new Account();

            await foreach (var account in csv.EnumerateRecordsAsync<Account>(placeholder))
            {
                yield return new Account
                {
                    Name = account.Name,
                    Number = account.Number,
                };
            }
        }

    }
}
