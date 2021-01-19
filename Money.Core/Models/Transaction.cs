using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Money.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int AccountId { get; set; }
        public int? OtherAccountId { get; set; }
        public int? PayeeId { get; set; }

        private Payee _payee;

        [Computed]
        public Payee Payee
        {
            get => _payee;
            set
            {
                PayeeId = value.Id;
                _payee = value;
            }
        }

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

        private Account _account;
        private Account _otherAccount;

        [Computed]
        public virtual Account Account
        {
            get => _account;
            set
            {
                _account = value;
                AccountId = value.Id;
            }
        }

        [Computed]
        public virtual Account OtherAccount
        {
            get => _otherAccount;
            set
            {
                _otherAccount = value;
                OtherAccountId = value?.Id;
            }
        }

        [Computed]
        public IEnumerable<string> Tags { get; set; }
        
        public string DataSource { get; set; }
    }
}