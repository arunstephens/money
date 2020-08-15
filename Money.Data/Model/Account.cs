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

        public decimal OpeningBalance { get; set; }

        public IList<Transaction> Transactions { get; set; }
    }
}
