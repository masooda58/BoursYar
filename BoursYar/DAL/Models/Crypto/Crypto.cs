using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Dapper.Contrib.Extensions;

namespace DAL
{
    

    public  class CryptoAll
    {
        [JsonProperty("last_update", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LastUpdate { get; set; }

        [JsonProperty("data", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Crypto> Data { get; set; }
    }
    [Table("Crypto")]
    public  class Crypto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; } 
        public DateTime Date { get; set; }

        [JsonProperty("symbol", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

       [JsonProperty("icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
       public string Icon { get; set; }

        [JsonProperty("price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }

        [JsonProperty("change_percent_24h", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChangePercent24H { get; set; }

        [JsonProperty("volume_24h", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Volume24H { get; set; }

        [JsonProperty("market_cap", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MarketCap { get; set; }
    }


}
