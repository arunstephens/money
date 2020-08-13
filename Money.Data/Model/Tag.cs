using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Data.Model
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Transaction> Transactions { get; set; }
    }
}
