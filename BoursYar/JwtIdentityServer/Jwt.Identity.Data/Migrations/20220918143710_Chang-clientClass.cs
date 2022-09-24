using Microsoft.EntityFrameworkCore.Migrations;

namespace Jwt.Identity.Data.Migrations
{
    public partial class ChangclientClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailLandingPage",
                table: "Clients",
                newName: "EmailResetPage");

            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmPage",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmPage",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "EmailResetPage",
                table: "Clients",
                newName: "EmailLandingPage");
        }
    }
}
