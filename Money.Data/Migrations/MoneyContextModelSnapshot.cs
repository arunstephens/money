﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Money.Data;

namespace Money.Data.Migrations
{
    [DbContext(typeof(MoneyContext))]
    partial class MoneyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Money.Data.Model.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BankBatchNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankSerial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankTransactionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankTransactionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardSuffix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginatingBankBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherPartyAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Particulars")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PayeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Serial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
