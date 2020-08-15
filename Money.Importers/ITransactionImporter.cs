using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Importers
{
    public interface ITransactionImporter
    {
        IAsyncEnumerable<Transaction> Import(string filename, Func<string, Account> accountMapper);
    }
}
