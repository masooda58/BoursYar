using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class AllNamadInfoDaily
    {
        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
        public string InstanceCode { get; set; }
        public string NamadCode { get; set; }
        public string IndustryCode { get; set; }
        public string Industry { get; set; }
        public string State { get; set; }
        public string FullName { get; set; }
        public long? FirstPrice { get; set; }
        public long? YesterdayPrice { get; set; }
        public long? ClosePrice { get; set; }
        public long? ClosePriceChange { get; set; }
        public string ClosePriceChangePercent { get; set; }
        public long? FinalPrice { get; set; }
        public long? FinalPriceChange { get; set; }
        public string FinalPriceChangePercent { get; set; }
        public string Eps { get; set; }
        public string FreeFloat { get; set; }
        public long? HighestPrice { get; set; }
        public long? LowestPrice { get; set; }
        public long? DailyPriceHigh { get; set; }
        public long? DailyPriceLow { get; set; }
        public double? Pe { get; set; }
        public long? TradeNumber { get; set; }
        public long? TradeVolume { get; set; }
        public long? TradeValue { get; set; }
        public long? AllStocks { get; set; }
        public long? BasisVolume { get; set; }
        public long? RealBuyVolume { get; set; }
        public long? CoBuyVolume { get; set; }
        public long? RealSellVolume { get; set; }
        public long? CoSellVolume { get; set; }
        public long? RealBuyValue { get; set; }
        public long? CoBuyValue { get; set; }
        public long? RealSellValue { get; set; }
        public long? CoSellValue { get; set; }
        public long? RealBuyCount { get; set; }
        public long? CoBuyCount { get; set; }
        public long? RealSellCount { get; set; }
        public long? CoSellCount { get; set; }
        public long? The1SellCount { get; set; }
        public long? The2SellCount { get; set; }
        public long? The3SellCount { get; set; }
        public long? The1BuyCount { get; set; }
        public long? The2BuyCount { get; set; }
        public long? The3BuyCount { get; set; }
        public long? The1SellPrice { get; set; }
        public long? The2SellPrice { get; set; }
        public long? The3SellPrice { get; set; }
        public long? The1BuyPrice { get; set; }
        public long? The2BuyPrice { get; set; }
        public long? The3BuyPrice { get; set; }
        public long? The1SellVolume { get; set; }
        public long? The2SellVolume { get; set; }
        public long? The3SellVolume { get; set; }
        public long? The1BuyVolume { get; set; }
        public long? The2BuyVolume { get; set; }
        public long? The3BuyVolume { get; set; }
        public long? MarketValue { get; set; }
    }
}
