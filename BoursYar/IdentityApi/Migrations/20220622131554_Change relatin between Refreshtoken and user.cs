using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityApi.Migrations
{
    public partial class ChangerelatinbetweenRefreshtokenanduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokenDtos_UserId",
                table: "RefreshTokenDtos");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenDtos_UserId",
                table: "RefreshTokenDtos",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokenDtos_UserId",
                table: "RefreshTokenDtos");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenDtos_UserId",
                table: "RefreshTokenDtos",
                column: "UserId");
        }
    }
}
