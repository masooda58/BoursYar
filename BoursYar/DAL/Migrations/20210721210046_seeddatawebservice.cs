using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class seeddatawebservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CallWebServiceSetting",
                columns: new[] { "Code", "ClassJsonType", "ClassType", "Faal", "FinishTime", "Interval", "Name", "NeedAddDate", "StartTime", "Url" },
                values: new object[,]
                {
                    { 1, "System.Collections.Generic.List`1[[DAL.AllNamadInfo, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.AllNamadInfo, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "AllNamadInfo", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&all&type=0" },
                    { 2, "DAL.Index", "DAL.BourseIndex", true, "16:55:00", 120, "market_bourse", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=market_bourse" },
                    { 3, "DAL.Index", "DAL.BourseIndex", true, "16:55:00", 120, "market_farabourse", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=market_farabourse" },
                    { 4, "System.Collections.Generic.List`1[[DAL.IndusteryIndex, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.IndusteryIndex, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "IndusteryIndex", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=indices" },
                    { 5, "System.Collections.Generic.List`1[[DAL.FavNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.FavNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "fav_namad_bourse", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=fav_namad_bourse" },
                    { 6, "System.Collections.Generic.List`1[[DAL.FavNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.FavNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "fav_namad_farabourse", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=fav_namad_farabourse" },
                    { 7, "System.Collections.Generic.List`1[[DAL.IndNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.IndNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "ind_namad_bourse", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=ind_namad_bourse" },
                    { 8, "System.Collections.Generic.List`1[[DAL.IndNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.IndNamad, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "ind_namad_farabourse", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&market=ind_namad_farabourse" },
                    { 9, "System.Collections.Generic.List`1[[DAL.Codal, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.Codal, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "Codal", false, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&codal=all&p=1" },
                    { 10, "System.Collections.Generic.List`1[[DAL.PayamNazer, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.PayamNazer, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "16:55:00", 120, "Payamnazer", false, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&inspect=all" },
                    { 11, "System.Collections.Generic.List`1[[DAL.Khodro, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.Khodro, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "18:30:00", 3600, "Khodro", false, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&car=all" },
                    { 12, "System.Collections.Generic.List`1[[DAL.Crypto, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "DAL.CryptoAll", true, "18:30:00", 3600, "Crypto", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&crypto_v2=all" },
                    { 13, "System.Collections.Generic.List`1[[DAL.Arz, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.Arz, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "18:30:00", 3600, "Arz", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&currency" },
                    { 14, "System.Collections.Generic.List`1[[DAL.AllNamadInfo_Daily, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.AllNamadInfo_Daily, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "18:30:00", 3600, "AllNamadInfo_Daily", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&all&type=0" },
                    { 15, "System.Collections.Generic.List`1[[DAL.AllNamadOption, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", "System.Collections.Generic.List`1[[DAL.AllNamadOption, DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", true, "18:30:00", 3600, "AllNamadOption", true, "14:50:00", "https://sourcearena.ir/api/?token=722b65c8184942a55aebc5253895f8d9&all&type=1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CallWebServiceSetting",
                keyColumn: "Code",
                keyValue: 15);
        }
    }
}
