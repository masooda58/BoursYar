using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jwt.Identity.Data.Migrations
{
    public partial class user_logInOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLogInOutLogs",
                columns: table => new
                {
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignInOut = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogInOutLogs", x => x.IdGuid);
                    table.ForeignKey(
                        name: "FK_UserLogInOutLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogInOutLogs_UserId",
                table: "UserLogInOutLogs",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogInOutLogs");
        }
    }
}
