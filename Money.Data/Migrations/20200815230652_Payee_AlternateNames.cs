using Microsoft.EntityFrameworkCore.Migrations;

namespace Money.Data.Migrations
{
    public partial class Payee_AlternateNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayeeAlternateName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PayeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayeeAlternateName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayeeAlternateName_Payee_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "Payee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayeeAlternateName_PayeeId",
                table: "PayeeAlternateName",
                column: "PayeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayeeAlternateName");
        }
    }
}
