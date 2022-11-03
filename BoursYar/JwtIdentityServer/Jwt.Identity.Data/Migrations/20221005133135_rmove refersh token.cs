using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jwt.Identity.Data.Migrations
{
    public partial class rmoverefershtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "Lockout",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lockout",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TempRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.IdGuid);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
