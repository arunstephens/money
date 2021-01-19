using Money.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Money.Importers
{
    public interface ITransactionImporter
    {
        IAsyncEnumerable<Transaction> Import(string filename, Func<Account, Task<Account>> accountMapper, Func<Payee, Task<Payee>> payeeMapper);
    }
}
