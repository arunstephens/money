using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Importers
{
    public interface ITransactionImporter
    {
        IAsyncEnumerable<Transaction> Import(string filename, Func<Account, Account> accountMapper, Func<Category, Task<Category>> categoryMapper);
    }
}
