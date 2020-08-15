using System;
using System.ComponentModel.DataAnnotations;

namespace Money.Data.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int AccountId { get; set; }
        public Payee Payee { get; set; }
        //public int? CategoryId { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public string PayeeName { get; set; }
        public decimal Amount { get; set; }

        public string Particulars { get; set; }
        public string Code { get; set; }
        public string Reference { get; set; }
        public string BankTransactionType { get; set; }
        public string OtherPartyAccountNumber { get; set; }
        public string Serial { get; set; }
        public string BankSerial { get; set; }
        public string BankTransactionCode { get; set; }
        public string BankBatchNumber { get; set; }
        public string OriginatingBankBranch { get; set; }

        public string CardSuffix { get; set; }

        public virtual Account Account { get; set; }
    }
}
