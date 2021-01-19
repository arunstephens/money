using Money.Core.Models;
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
