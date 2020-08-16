using CsvHelper.Configuration.Attributes;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Money.Importers.Model
{
    public class AsbBankTransaction : IBankTransaction
    {
        // Date Processed,Date of Transaction,Unique Id,Tran Type,Reference,Description,Amount
        [Index(0)]
        [Format("yyyy/MM/dd")]
        public DateTime TransactionDate { get; set; }

        [Index(1)]
        public string UniqueId { get; set; }

        [Index(2)]
        public string TransactionType { get; set; }

        [Index(3)]
        public string ChequeNumber { get; set; }

        [Index(4)]
        public string Payee { get; set; }

        [Index(5)]
        public string Memo { get; set; }

        [Index(6)]
        public decimal Amount { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                TransactionDate = TransactionDate,
                PayeeName = $"{Payee} {Memo}",
                ExternalId = GetExternalId(),
                Amount = Amount,
            };
        }

        private string GetExternalId()
        {
            return ModelHelper.GetSignature(UniqueId);
        }
    }
}
