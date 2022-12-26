using Microsoft.EntityFrameworkCore.Migrations;

namespace Jwt.Identity.Data.Migrations
{
    public partial class SessionAddOther : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentitySettings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Device",
                table: "UserLogInOutLogs");

            migrationBuilder.DropColumn(
                name: "IpAdress",
                table: "UserLogInOutLogs");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "UserLogInOutLogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SessionEntity",
                columns: table => new
                {
                    SessionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionEntity", x => x.SessionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogInOutLogs_SessionId",
                table: "UserLogInOutLogs",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogInOutLogs_SessionEntity_SessionId",
                table: "UserLogInOutLogs",
                column: "SessionId",
                principalTable: "SessionEntity",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogInOutLogs_SessionEntity_SessionId",
                table: "UserLogInOutLogs");

            migrationBuilder.DropTable(
                name: "SessionEntity");

            migrationBuilder.DropIndex(
                name: "IX_UserLogInOutLogs_SessionId",
                table: "UserLogInOutLogs");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "UserLogInOutLogs");

            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "UserLogInOutLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAdress",
                table: "UserLogInOutLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentitySettings",
                columns: new[] { "Id", "CaptchStrategy", "DefaultLockoutTimeSpanMinute", "MaxFailedAccessAttempts", "RequireConfirmedAccount", "RequireDigit", "RequireLowercase", "RequireNonAlphanumeric", "RequireUppercase", "RequiredLength", "RequiredUniqueChars", "TokenLifespanHour", "TotpLifeSpanMinute" },
                values: new object[] { 1, 2, 30, 3, false, false, false, false, false, 1, 1, 8, 2 });
        }
    }
}
