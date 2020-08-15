using CsvHelper.Configuration.Attributes;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Importers.Model
{
    public class AsbCreditCardTransaction : IBankTransaction
    {
        // Date Processed,Date of Transaction,Unique Id,Tran Type,Reference,Description,Amount
        [Index(0)]
        [Format("yyyy/MM/dd")]
        public DateTime DateProcessed { get; set; }

        [Index(1)]
        [Format("yyyy/MM/dd")]
        public DateTime TransactionDate { get; set; }

        [Index(2)]
        public string UniqueId { get; set; }

        [Index(3)]
        public string TransactionType { get; set; }

        [Index(4)]
        public string Reference { get; set; }

        [Index(5)]
        public string Description { get; set; }

        [Index(6)]
        public decimal Amount { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                ProcessedDate = DateProcessed,
                TransactionDate = TransactionDate,
                Reference = Reference,
                PayeeName = Description,
                ExternalId = GetExternalId(),
                Amount = -Amount,
            };
        }

        private string GetExternalId()
        {
            return ModelHelper.GetSignature(UniqueId);
        }
    }
}
