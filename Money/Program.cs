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

            var importer = new BnzImporter();

            await foreach (var tx in importer.Import(@"C:\Users\a\Documents\Money\Joint---Main-9AUG2018-to-9AUG2020.csv"))
            {
                Console.WriteLine(tx.TransactionDate + " " + tx.Amount + " " + tx.ExternalId);
            }
        }
    }
}
