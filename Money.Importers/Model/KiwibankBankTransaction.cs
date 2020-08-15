using CsvHelper.Configuration.Attributes;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Money.Importers.Model
{
    public class KiwibankBankTransaction : IBankTransaction
    {
        [Name("Account number")]
        public string AccountNumber { get; set; }

        [Format("dd-MM-yyyy")]
        public DateTime Date { get; set; }

        [Name("Memo/Description")]
        public string Memo { get; set; }

        [Name("Source Code (payment type)")]
        public string SourceCode { get; set; }

        [Name("TP ref")]
        public string ThisPartyReference { get; set; }
        [Name("TP part")]
        public string ThisPartyParticulars { get; set; }
        [Name("TP code")]
        public string ThisPartyCode { get; set; }

        [Name("OP ref")]
        public string OtherPartyReference { get; set; }
        [Name("OP part")]
        public string OtherPartyParticulars { get; set; }
        [Name("OP code")]
        public string OtherPartyCode { get; set; }

        [Name("OP name")]
        public string OtherPartyName { get; set; }
        [Name("OP Bank Account Number")]
        public string OtherPartyBankAccount { get; set; }

        [Name("Amount (credit)")]
        public decimal? AmountCredit { get; set; }
        [Name("Amount (debit)")]
        public decimal? AmountDebit { get; set; }
        [Name("Amount")]
        public decimal Amount { get; set; }
        [Name("Balance")]
        public decimal Balance { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                TransactionDate = Date,
                Amount = Amount,
                PayeeName = Memo,
                Particulars = ThisPartyParticulars,
                Code = ThisPartyCode,
                Reference = ThisPartyReference,
                OtherPartyAccountNumber = OtherPartyBankAccount,
                ExternalId = GetExternalId(),
            };
        }

        private string GetExternalId()
        {
            var raw = $"{Date:yyyy-MM-dd}:{Memo}:{Amount:0.00}:{Balance:0.00}";
            return ModelHelper.GetSignature(raw);
        }
    }
}
