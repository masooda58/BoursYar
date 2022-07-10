using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Migrations
{
    public partial class F1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllNamadInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Market = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstanceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamadCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPrice = table.Column<long>(type: "bigint", nullable: true),
                    YesterdayPrice = table.Column<long>(type: "bigint", nullable: true),
                    ClosePrice = table.Column<long>(type: "bigint", nullable: true),
                    ClosePriceChange = table.Column<long>(type: "bigint", nullable: true),
                    ClosePriceChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPrice = table.Column<long>(type: "bigint", nullable: true),
                    FinalPriceChange = table.Column<long>(type: "bigint", nullable: true),
                    FinalPriceChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeFloat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighestPrice = table.Column<long>(type: "bigint", nullable: true),
                    LowestPrice = table.Column<long>(type: "bigint", nullable: true),
                    DailyPriceHigh = table.Column<long>(type: "bigint", nullable: true),
                    DailyPriceLow = table.Column<long>(type: "bigint", nullable: true),
                    PE = table.Column<double>(type: "float", nullable: true),
                    TradeNumber = table.Column<long>(type: "bigint", nullable: true),
                    TradeVolume = table.Column<long>(type: "bigint", nullable: true),
                    TradeValue = table.Column<long>(type: "bigint", nullable: true),
                    AllStocks = table.Column<long>(type: "bigint", nullable: true),
                    BasisVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealSellVolume = table.Column<long>(type: "bigint", nullable: true),
                    CoSellVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyValue = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyValue = table.Column<long>(type: "bigint", nullable: true),
                    RealSellValue = table.Column<long>(type: "bigint", nullable: true),
                    CoSellValue = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyCount = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyCount = table.Column<long>(type: "bigint", nullable: true),
                    RealSellCount = table.Column<long>(type: "bigint", nullable: true),
                    CoSellCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    MarketValue = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllNamadInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllNamadInfo_Daily",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Market = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstanceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamadCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPrice = table.Column<long>(type: "bigint", nullable: true),
                    YesterdayPrice = table.Column<long>(type: "bigint", nullable: true),
                    ClosePrice = table.Column<long>(type: "bigint", nullable: true),
                    ClosePriceChange = table.Column<long>(type: "bigint", nullable: true),
                    ClosePriceChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPrice = table.Column<long>(type: "bigint", nullable: true),
                    FinalPriceChange = table.Column<long>(type: "bigint", nullable: true),
                    FinalPriceChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeFloat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighestPrice = table.Column<long>(type: "bigint", nullable: true),
                    LowestPrice = table.Column<long>(type: "bigint", nullable: true),
                    DailyPriceHigh = table.Column<long>(type: "bigint", nullable: true),
                    DailyPriceLow = table.Column<long>(type: "bigint", nullable: true),
                    PE = table.Column<double>(type: "float", nullable: true),
                    TradeNumber = table.Column<long>(type: "bigint", nullable: true),
                    TradeVolume = table.Column<long>(type: "bigint", nullable: true),
                    TradeValue = table.Column<long>(type: "bigint", nullable: true),
                    AllStocks = table.Column<long>(type: "bigint", nullable: true),
                    BasisVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealSellVolume = table.Column<long>(type: "bigint", nullable: true),
                    CoSellVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyValue = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyValue = table.Column<long>(type: "bigint", nullable: true),
                    RealSellValue = table.Column<long>(type: "bigint", nullable: true),
                    CoSellValue = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyCount = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyCount = table.Column<long>(type: "bigint", nullable: true),
                    RealSellCount = table.Column<long>(type: "bigint", nullable: true),
                    CoSellCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    MarketValue = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllNamadInfo_Daily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllNamadOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Market = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstanceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamadCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPrice = table.Column<long>(type: "bigint", nullable: true),
                    YesterdayPrice = table.Column<long>(type: "bigint", nullable: true),
                    ClosePrice = table.Column<long>(type: "bigint", nullable: true),
                    ClosePriceChange = table.Column<long>(type: "bigint", nullable: true),
                    ClosePriceChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPrice = table.Column<long>(type: "bigint", nullable: true),
                    FinalPriceChange = table.Column<long>(type: "bigint", nullable: true),
                    FinalPriceChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeFloat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighestPrice = table.Column<long>(type: "bigint", nullable: true),
                    LowestPrice = table.Column<long>(type: "bigint", nullable: true),
                    DailyPriceHigh = table.Column<long>(type: "bigint", nullable: true),
                    DailyPriceLow = table.Column<long>(type: "bigint", nullable: true),
                    PE = table.Column<double>(type: "float", nullable: true),
                    TradeNumber = table.Column<long>(type: "bigint", nullable: true),
                    TradeVolume = table.Column<long>(type: "bigint", nullable: true),
                    TradeValue = table.Column<long>(type: "bigint", nullable: true),
                    AllStocks = table.Column<long>(type: "bigint", nullable: true),
                    BasisVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealSellVolume = table.Column<long>(type: "bigint", nullable: true),
                    CoSellVolume = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyValue = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyValue = table.Column<long>(type: "bigint", nullable: true),
                    RealSellValue = table.Column<long>(type: "bigint", nullable: true),
                    CoSellValue = table.Column<long>(type: "bigint", nullable: true),
                    RealBuyCount = table.Column<long>(type: "bigint", nullable: true),
                    CoBuyCount = table.Column<long>(type: "bigint", nullable: true),
                    RealSellCount = table.Column<long>(type: "bigint", nullable: true),
                    CoSellCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyCount = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellPrice = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyPrice = table.Column<long>(type: "bigint", nullable: true),
                    The1_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The2_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The3_SellVolume = table.Column<long>(type: "bigint", nullable: true),
                    The1_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    The2_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    The3_BuyVolume = table.Column<long>(type: "bigint", nullable: true),
                    MarketValue = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllNamadOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Change = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JalaliLastUpdate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BourseIndix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Market = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<double>(type: "float", nullable: false),
                    IndexChange = table.Column<double>(type: "float", nullable: false),
                    IndexChangePercent = table.Column<double>(type: "float", nullable: false),
                    IndexH = table.Column<double>(type: "float", nullable: false),
                    IndexHChange = table.Column<double>(type: "float", nullable: false),
                    IndexHChangePercent = table.Column<double>(type: "float", nullable: false),
                    MarketValue = table.Column<double>(type: "float", nullable: false),
                    TradeNumber = table.Column<long>(type: "bigint", nullable: false),
                    TradeValue = table.Column<double>(type: "float", nullable: false),
                    TradeVolume = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BourseIndix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallWebServiceSetting",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faal = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassJsonType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeedAddDate = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallWebServiceSetting", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Codal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    letter_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    excel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crypto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangePercent24H = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume24H = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketCap = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crypto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavNamad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Market = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPriceChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosePrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosePriceChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowestPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighestPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavNamad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndNamad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Market = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndNamad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndusteryIndex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiladiDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Change = table.Column<double>(type: "float", nullable: true),
                    Percent = table.Column<double>(type: "float", nullable: true),
                    Max = table.Column<double>(type: "float", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndusteryIndex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Khodro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketPrice = table.Column<bool>(type: "bit", nullable: true),
                    LastUpdate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khodro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReqTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Success = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayamNazer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Head = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayamNazer", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "AllNamadInfo");

            migrationBuilder.DropTable(
                name: "AllNamadInfo_Daily");

            migrationBuilder.DropTable(
                name: "AllNamadOption");

            migrationBuilder.DropTable(
                name: "Arz");

            migrationBuilder.DropTable(
                name: "BourseIndix");

            migrationBuilder.DropTable(
                name: "CallWebServiceSetting");

            migrationBuilder.DropTable(
                name: "Codal");

            migrationBuilder.DropTable(
                name: "Crypto");

            migrationBuilder.DropTable(
                name: "FavNamad");

            migrationBuilder.DropTable(
                name: "IndNamad");

            migrationBuilder.DropTable(
                name: "IndusteryIndex");

            migrationBuilder.DropTable(
                name: "Khodro");

            migrationBuilder.DropTable(
                name: "Logger");

            migrationBuilder.DropTable(
                name: "PayamNazer");
        }
    }
}
