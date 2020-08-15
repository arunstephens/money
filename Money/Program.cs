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

            IImporter importer;

            importer = new CsvImporter<Importers.Model.BnzTransaction>(true, false);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\Joint---Main-9AUG2018-to-9AUG2020.csv", _ => 1));

            importer = new CsvImporter<Importers.Model.KiwibankCreditCardTransaction>(false, true);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\4833-48 - -3016_10Aug.CSV", _ => 2));

            importer = new CsvImporter<Importers.Model.KiwibankBankTransaction>(true, false);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-02_10Aug.CSV", _ => 3));
            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-01_15Aug.CSV", _ => 4));
            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\38-9018-0371564-03_15Aug.CSV", _ => 5));

            importer = new CsvImporter<Importers.Model.AsbBankTransaction>(false, false, true);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\Export20200815213103.csv", _ => 6));

            importer = new CsvImporter<Importers.Model.AsbCreditCardTransaction>(false, false, true);

            await WriteTransactions(importer.Import(@"C:\Users\a\Documents\Money\Export20200815213632.csv", _ => 7));
        }

        private static async Task WriteTransactions(IAsyncEnumerable<Transaction> transactions)
        {
            const int PayeeLength = 40;

            await foreach (var tx in transactions)
            {
                Console.WriteLine($"{tx.AccountId,3} {tx.TransactionDate:yyyy-MM-dd} {(tx.PayeeName.Length < PayeeLength ? tx.PayeeName : tx.PayeeName.Substring(0, PayeeLength)).PadRight(PayeeLength)} {tx.Amount,10:0.00} {tx.ExternalId}");
            }
        }
    }
}
