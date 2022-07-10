using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DAL
{
    [Table("Arz")]
    public class Arz
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        [JsonProperty("slug", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverterDouble))]
        public string Price { get; set; }

        [JsonProperty("change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double Change { get; set; }

        [JsonProperty("change_percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ChangePercent { get; set; }

        [JsonProperty("min_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MinPrice { get; set; }

        [JsonProperty("max_price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MaxPrice { get; set; }

        [JsonProperty("last_update", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LastUpdate { get; set; }

        [JsonProperty("jalali_last_update", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string JalaliLastUpdate { get; set; }
    }
    public class LArz
    {
        public List<Arz> data { get; set; }
    }
}
