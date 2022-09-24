using Microsoft.EntityFrameworkCore.Migrations;

namespace Jwt.Identity.Data.Migrations
{
    public partial class uniquiclientname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientName",
                table: "Clients",
                column: "ClientName",
                unique: true,
                filter: "[ClientName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientName",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
