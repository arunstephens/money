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
    public class KiwibankCreditCardImporter : IImporter
    {
        public async IAsyncEnumerable<Transaction> Import(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //csv.Configuration.RegisterClassMap<ClassMap>();
                csv.Configuration.HasHeaderRecord = false;

                var accountNumber = await reader.ReadLineAsync();

                KiwibankCreditCardTransaction placeholder = new KiwibankCreditCardTransaction();

                await foreach (var bankTx in csv.EnumerateRecordsAsync<KiwibankCreditCardTransaction>(placeholder))
                {
                    var tx = bankTx.ToTransaction();

                    // TODO: Do something with the account number

                    yield return tx;
                }
            }
        }

        class ClassMap : ClassMap<KiwibankCreditCardTransaction>
        {
            public ClassMap()
            {
                Map(x => x.Date).TypeConverterOption.Format("dd-MM-yyyy");
                Map(x => x.Amount);
                Map(x => x.CardSuffix);
                Map(x => x.Payee);
            }
        }
    }
}
