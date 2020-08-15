using Microsoft.EntityFrameworkCore;
using Money.Data.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Money.Data
{
    public class MoneyContext : DbContext
    {
        public MoneyContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public MoneyContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=.\SQLEXPRESS;Trusted_Connection=True;MultipleActiveResultSets=true;Database=MoneyDB";

            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
