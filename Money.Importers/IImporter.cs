using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Importers
{
    public interface IImporter
    {
        IAsyncEnumerable<Transaction> Import(string filename, Func<string, int> accountIdMapper);
    }
}
