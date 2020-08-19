using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Data.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string PocketSmithId { get; set; }

        public decimal OpeningBalance { get; set; }

        public int? AccountTypeId { get; set; }

        private AccountType _accountType;

        [Computed]
        public virtual AccountType AccountType
        {
            get => _accountType;
            set
            {
                _accountType = value;
                AccountTypeId = value?.Id;
            }
        }
    }
}
