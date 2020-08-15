using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Importers.Model
{
    public interface IBankTransaction
    {
        Transaction ToTransaction();
    }
}
