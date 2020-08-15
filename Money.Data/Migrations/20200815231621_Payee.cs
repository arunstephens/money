using Microsoft.EntityFrameworkCore.Migrations;

namespace Money.Data.Migrations
{
    public partial class Payee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayeeAlternateName_Payee_PayeeId",
                table: "PayeeAlternateName");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payee_PayeeId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payee",
                table: "Payee");

            migrationBuilder.RenameTable(
                name: "Payee",
                newName: "Payees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payees",
                table: "Payees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayeeAlternateName_Payees_PayeeId",
                table: "PayeeAlternateName",
                column: "PayeeId",
                principalTable: "Payees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payees_PayeeId",
                table: "Transactions",
                column: "PayeeId",
                principalTable: "Payees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayeeAlternateName_Payees_PayeeId",
                table: "PayeeAlternateName");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payees_PayeeId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payees",
                table: "Payees");

            migrationBuilder.RenameTable(
                name: "Payees",
                newName: "Payee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payee",
                table: "Payee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayeeAlternateName_Payee_PayeeId",
                table: "PayeeAlternateName",
                column: "PayeeId",
                principalTable: "Payee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payee_PayeeId",
                table: "Transactions",
                column: "PayeeId",
                principalTable: "Payee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
