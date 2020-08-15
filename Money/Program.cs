using Money.Data.Model;
using Money.Importers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Money
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IImporter importer = new CsvImporter<Importers.Model.BnzTransaction>(true, false);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\Joint---Main-9AUG2018-to-9AUG2020.csv"));

            importer = new CsvImporter<Importers.Model.KiwibankCreditCardTransaction>(false, true);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\4833-48 - -3016_10Aug.CSV"));

            importer = new CsvImporter<Importers.Model.KiwibankBankTransaction>(true, false);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-02_10Aug.CSV"));
            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-01_15Aug.CSV"));
            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-03_15Aug.CSV"));
        }

        private static async Task WriteTransactions(IAsyncEnumerable<Transaction> transactions)
        {
            await foreach (var tx in transactions)
            {
                Console.WriteLine(tx.TransactionDate + " " + tx.Amount + " " + tx.ExternalId);
            }
        }
    }
}
