using CsvHelper.Configuration.Attributes;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Money.Importers.Model
{
    public class PocketSmithTransaction : IBankTransaction
    {
        public DateTime Date { get; set; }
        public string Merchant { get; set; }
        [Name("Merchant Changed From")]
        public string OriginalMerchant { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        [Name("Amount in base currency")]
        public decimal AmountInBaseCurrency { get; set; }
        [Name("Base currency")]
        public string BaseCurrency { get; set; }
        [Name("Transaction Type")]
        public string TransactionType { get; set; }
        public string Account { get; set; }
        [Name("Closing Balance")]
        public string ClosingBalance { get; set; }
        public string Category { get; set; }
        [Name("Parent Categories")]
        public string ParentCategories { get; set; }
        public string Labels { get; set; }
        public string Memo { get; set; }
        public string Note { get; set; }
        public string ID { get; set; }

        public Transaction ToTransaction()
        {
            Payee payee = null;

            if (Merchant.EmptyToNull() != OriginalMerchant.EmptyToNull() && Merchant.EmptyToNull() != null)
            {
                // The payee should be mapped on import
                payee = new Payee
                {
                    Name = Merchant.EmptyToNull(),
                    AlternateNames = new List<PayeeAlternateName> { new PayeeAlternateName { Name = OriginalMerchant.EmptyToNull() } }
                };
            }

            return new Transaction
            {
                TransactionDate = Date,
                PayeeName = Merchant.EmptyToNull(),
                Payee = payee,
                Account = new Account { PocketSmithId = Account.EmptyToNull() },
                Amount = Amount,
                Reference = Memo.EmptyToNull(),
                Particulars = Note.EmptyToNull(),
                OtherAccount = new Account
                {
                    Name = Category.EmptyToNull(),
                    AccountType = new AccountType { Code = "EXPENSE" }
                },
                ExternalId = ModelHelper.GetSignature(ID),
                Tags = Labels?.Split(",").Where(t => t.EmptyToNull() != null)
            };
        }
    }


}
