﻿using CsvHelper.Configuration.Attributes;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Money.Importers.Model
{
    public class KiwibankCreditCardTransaction : IBankTransaction
    {
        [Index(0)]
        [Format("dd-MM-yyyy")]
        public DateTime Date { get; set; }
        [Index(1)]
        public string Payee { get; set; }
        [Index(2)]
        public string CardSuffix { get; set; }
        [Index(3)]
        public decimal Amount { get; set; }

        public Transaction ToTransaction()
        {
            return new Transaction
            {
                TransactionDate = Date,
                PayeeName = Payee,
                Amount = Amount,
                CardSuffix = CardSuffix,
                ExternalId = GetExternalId(),
            };
        }

        private readonly static SHA256Managed sha = new SHA256Managed();

        private string GetExternalId()
        {
            var raw = $"{Date:yyyy-MM-dd}:{Payee}:{CardSuffix}:{Amount:0.00}";
            var rawData = Encoding.UTF8.GetBytes(raw);
            var signature = sha.ComputeHash(rawData);
            var signatureString = Convert.ToBase64String(signature);
            return signatureString;
        }
    }
}
