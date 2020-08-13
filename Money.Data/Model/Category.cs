using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Data.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Transaction> Transactions { get; set; }
    }
}
