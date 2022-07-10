using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace DAL
{
    [Table("Khodro")]
    public class Khodro
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [JsonProperty("model", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("type_en", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string TypeEn { get; set; }

        [JsonProperty("description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("year", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]

        public string Year { get; set; }

        [JsonProperty("price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }

        [JsonProperty("change_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChangePercent { get; set; }

        [JsonProperty("market_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool? MarketPrice { get; set; }

        [JsonProperty("last_update", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LastUpdate { get; set; }
    }
}
