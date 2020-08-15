using Microsoft.EntityFrameworkCore.Migrations;

namespace Money.Data.Migrations
{
    public partial class Transaction_Payee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayeeId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payee", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayeeId",
                table: "Transactions",
                column: "PayeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payee_PayeeId",
                table: "Transactions",
                column: "PayeeId",
                principalTable: "Payee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payee_PayeeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Payee");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PayeeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PayeeId",
                table: "Transactions");
        }
    }
}
