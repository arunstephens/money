using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Money.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    ProcessedDate = table.Column<DateTime>(nullable: false),
                    PayeeName = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Particulars = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    BankTransactionType = table.Column<string>(nullable: true),
                    OtherPartyAccountNumber = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    BankSerial = table.Column<string>(nullable: true),
                    BankTransactionCode = table.Column<string>(nullable: true),
                    BankBatchNumber = table.Column<string>(nullable: true),
                    OriginatingBankBranch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
