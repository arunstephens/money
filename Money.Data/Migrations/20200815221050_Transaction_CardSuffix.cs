using Microsoft.EntityFrameworkCore.Migrations;

namespace Money.Data.Migrations
{
    public partial class Transaction_CardSuffix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardSuffix",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardSuffix",
                table: "Transactions");
        }
    }
}
