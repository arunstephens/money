using Dapper.Contrib.Extensions;
using Money.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Data.Repositories
{
    public static class DapperConfig
    {
        public static void MapTypes()
        {
            SqlMapperExtensions.TableNameMapper = TableNameMapper;
        }

        private static string TableNameMapper(Type type)
        {
            if (type == typeof(Account))
            {
                return "Accounts";
            }
            
            if (type == typeof(AccountType))
            {
                return "AccountTypes";
            }

            if (type == typeof(Payee))
            {
                return "Payees";
            }

            if (type == typeof(Tag))
            {
                return "Tags";
            }

            if (type== typeof(Transaction))
            {
                return "Transactions";
            }

            return type.Name + "s";
        }
    }
}
