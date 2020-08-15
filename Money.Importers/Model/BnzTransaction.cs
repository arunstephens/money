using CsvHelper.Configuration.Attributes;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Money.Importers.Model
{
    public class BnzTransaction : IBankTransaction
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Payee { get; set; }
        public string Particulars { get; set; }
        public string Code { get; set; }
        public string Reference { get; set; }
        [Name("Tran Type")]
        public string TranType { get; set; }
        [Name("This Party Account")]
        public string ThisPartyAccount { get; set; }
        [Name("Other Party Account")]
        public string OtherPartyAccount { get; set; }
        public string Serial { get; set; }
        [Name("Transaction Code")]
        public string TransactionCode { get; set; }
        [Name("Batch Number")]
        public string BatchNumber { get; set; }
        [Name("Originating Bank/Branch")]
        public string OriginatingBankBranch { get; set; }
        [Name("Processed Date")]
        public DateTime ProcessedDate { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                Amount = Amount,
                BankBatchNumber = BatchNumber,
                BankSerial = Serial,
                OriginatingBankBranch = OriginatingBankBranch,
                OtherPartyAccountNumber = OtherPartyAccount,
                BankTransactionCode = TransactionCode,
                Code = Code,
                BankTransactionType = TranType,
                Particulars = Particulars,
                PayeeName = Payee,
                ProcessedDate = ProcessedDate,
                Reference = Reference,
                Serial = Serial,
                TransactionDate = Date,
                ExternalId = GetExternalId(),
            };
        }

        private readonly static SHA256Managed sha = new SHA256Managed();

        private string GetExternalId()
        {
            var raw = $"{Date:yyyy-MM-dd}:{Serial}:{ProcessedDate:yyyy-MM-dd}:{Amount:0.00}";
            return ModelHelper.GetSignature(raw);
        }
    }
}
