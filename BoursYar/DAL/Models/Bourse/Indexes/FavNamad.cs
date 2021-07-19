using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace DAL
{

        [Table("FavNamad")] // dapper contrib
    public class FavNamad
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        [Required]
        public string Market { get; set; }
        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("final_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FinalPrice { get; set; }

        [JsonProperty("final_price_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FinalPriceChange { get; set; }

        [JsonProperty("close_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ClosePrice { get; set; }

        [JsonProperty("close_price_change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ClosePriceChange { get; set; }

        [JsonProperty("lowest_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LowestPrice { get; set; }

        [JsonProperty("highest_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string HighestPrice { get; set; }

        [JsonProperty("n", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string N { get; set; }

        [JsonProperty("volume", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Volume { get; set; }

        [JsonProperty("value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}
