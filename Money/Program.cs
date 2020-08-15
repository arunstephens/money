using Money.Importers;
using System;
using System.Threading.Tasks;

namespace Money
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IImporter importer = new CsvImporter<Importers.Model.BnzTransaction>(true, false);

            await foreach (var tx in importer.Import(@"C:\Users\a\Documents\Money\Joint---Main-9AUG2018-to-9AUG2020.csv"))
            {
                Console.WriteLine(tx.TransactionDate + " " + tx.Amount + " " + tx.ExternalId);
            }

            importer = new CsvImporter<Importers.Model.KiwibankCreditCardTransaction>(false, true);

            await foreach (var tx in importer.Import(@"C:\Users\a\Documents\Money\4833-48 - -3016_10Aug.CSV"))
            {
                Console.WriteLine(tx.TransactionDate + " " + tx.Amount + " " + tx.ExternalId);
            }

            
        }
    }
}
