using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    [Table("BourseIndex")] // dapper contrib

    public class BourseIndex
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        [Required]
        public string Market { get; set; }

        [JsonProperty("state", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("index", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double Index { get; set; }


        [JsonProperty("index_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double IndexChange { get; set; }

        [JsonProperty("index_change_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double IndexChangePercent { get; set; }

        [JsonProperty("index_h", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double IndexH { get; set; }

        [JsonProperty("index_h_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double IndexHChange { get; set; }

        [JsonProperty("index_h_change_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double IndexHChangePercent { get; set; }

        [JsonProperty("market_value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double MarketValue { get; set; }

        [JsonProperty("trade_number", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TradeNumber { get; set; }

        [JsonProperty("trade_value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double TradeValue { get; set; }

        [JsonProperty("trade_volume", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public double TradeVolume { get; set; }
    }



    public partial class Index
    {
        [JsonProperty("bourse", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public BourseIndex Bourse { get; set; }
        [JsonProperty("fara-bourse", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public BourseIndex FaraBourse { get; set; }
    }
}
