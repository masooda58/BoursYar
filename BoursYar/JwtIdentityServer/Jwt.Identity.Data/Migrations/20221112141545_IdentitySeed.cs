using Microsoft.EntityFrameworkCore.Migrations;

namespace Jwt.Identity.Data.Migrations
{
    public partial class IdentitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IdentitySettings",
                columns: new[] { "Id", "CaptchStrategy", "DefaultLockoutTimeSpanMinute", "MaxFailedAccessAttempts", "RequireConfirmedAccount", "RequireDigit", "RequireLowercase", "RequireNonAlphanumeric", "RequireUppercase", "RequiredLength", "RequiredUniqueChars", "TokenLifespanHour", "TotpLifeSpanMinute" },
                values: new object[] { 1, 2, 30, 3, false, false, false, false, false, 1, 1, 8, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentitySettings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
