using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Data.Model
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Computed]
        public virtual IList<Transaction> Transactions { get; set; }
    }
}
