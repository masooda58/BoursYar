using System;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace DAL
{
    [Table("AllNamadInfo_Daily")]
    public class AllNamadInfo_Daily
    {
        //در دیتا بیس ساخته نمی شود و فقط برای یکسان سازی وب سرویس با تاریخ و لحظه ای استفاده شده است Private فیلد
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("market", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]

        public string Market { get; set; }

        [JsonProperty("instance_code", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InstanceCode { get; set; }

        [JsonProperty("namad_code", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string NamadCode { get; set; }

        [JsonProperty("industry_code", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string IndustryCode { get; set; }

        [JsonProperty("industry", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Industry { get; set; }
        [JsonProperty("Type")]
        private string Type { set => Industry = value; }
        [JsonProperty("state", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("full_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FullName { get; set; }

        [JsonProperty("first_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FirstPrice { get; set; }

        [JsonProperty("yesterday_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? YesterdayPrice { get; set; }

        [JsonProperty("close_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ClosePrice { get; set; }

        [JsonProperty("close_price_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? ClosePriceChange { get; set; }

        [JsonProperty("close_price_change_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ClosePriceChangePercent { get; set; }

        [JsonProperty("final_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FinalPrice { get; set; }

        [JsonProperty("final_price_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? FinalPriceChange { get; set; }

        [JsonProperty("final_price_change_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FinalPriceChangePercent { get; set; }

        [JsonProperty("eps")]
        public string Eps { get; set; }

        [JsonProperty("free_float", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FreeFloat { get; set; }

        [JsonProperty("highest_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? HighestPrice { get; set; }

        [JsonProperty("lowest_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? LowestPrice { get; set; }

        [JsonProperty("daily_price_high", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        private double? _DailyPriceHigh
        {
            set => DailyPriceHigh = (long)value;
        }
        public long? DailyPriceHigh { get; set; }

        [JsonProperty("daily_price_low", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        private double? _DailyPriceLow
        {
            set => DailyPriceLow = (long)value;
        }

        public long? DailyPriceLow { get; set; }
        [JsonProperty("P:E")]
        public double? PE { get; set; }

        [JsonProperty("trade_number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TradeNumber { get; set; }

        [JsonProperty("trade_volume", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TradeVolume { get; set; }

        [JsonProperty("trade_value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TradeValue { get; set; }

        [JsonProperty("all_stocks", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? AllStocks { get; set; }

        [JsonProperty("basis_volume", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? BasisVolume { get; set; }

        [JsonProperty("real_buy_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RealBuyVolume { get; set; }

        [JsonProperty("co_buy_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CoBuyVolume { get; set; }

        [JsonProperty("real_sell_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RealSellVolume { get; set; }

        [JsonProperty("co_sell_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CoSellVolume { get; set; }

        [JsonProperty("real_buy_value")]
        public long? RealBuyValue { get; set; }

        [JsonProperty("co_buy_value")]
        public long? CoBuyValue { get; set; }

        [JsonProperty("real_sell_value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? RealSellValue { get; set; }

        [JsonProperty("co_sell_value")]
        public long? CoSellValue { get; set; }

        [JsonProperty("real_buy_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RealBuyCount { get; set; }

        [JsonProperty("co_buy_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CoBuyCount { get; set; }

        [JsonProperty("real_sell_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? RealSellCount { get; set; }

        [JsonProperty("co_sell_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CoSellCount { get; set; }

        [JsonProperty("1_sell_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The1_SellCount { get; set; }

        [JsonProperty("2_sell_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The2_SellCount { get; set; }

        [JsonProperty("3_sell_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The3_SellCount { get; set; }

        [JsonProperty("1_buy_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The1_BuyCount { get; set; }

        [JsonProperty("2_buy_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The2_BuyCount { get; set; }

        [JsonProperty("3_buy_count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The3_BuyCount { get; set; }

        [JsonProperty("1_sell_price")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The1_SellPrice { get; set; }

        [JsonProperty("2_sell_price")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The2_SellPrice { get; set; }

        [JsonProperty("3_sell_price")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The3_SellPrice { get; set; }

        [JsonProperty("1_buy_price")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The1_BuyPrice { get; set; }

        [JsonProperty("2_buy_price")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The2_BuyPrice { get; set; }

        [JsonProperty("3_buy_price")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The3_BuyPrice { get; set; }

        [JsonProperty("1_sell_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The1_SellVolume { get; set; }

        [JsonProperty("2_sell_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The2_SellVolume { get; set; }

        [JsonProperty("3_sell_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The3_SellVolume { get; set; }

        [JsonProperty("1_buy_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The1_BuyVolume { get; set; }

        [JsonProperty("2_buy_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The2_BuyVolume { get; set; }

        [JsonProperty("3_buy_volume")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? The3_BuyVolume { get; set; }

        [JsonProperty("market_value")]
        public long? MarketValue { get; set; }


    }

}

