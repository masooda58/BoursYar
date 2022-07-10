using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;

namespace DAL
{

    [Table("IndusteryIndex")] // dapper contrib
    public class IndusteryIndex
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("value", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverterDouble))]
        public double Value { get; set; }

        [JsonProperty("change", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverterDouble))]
        public double? Change { get; set; }

        [JsonProperty("percent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]

        [JsonConverter(typeof(ParseStringConverterDouble))]
        public double? Percent { get; set; }

        [JsonProperty("max", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverterDouble))]
        public double Max { get; set; }

        [JsonProperty("min", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverterDouble))]
        public double Min { get; set; }
    }
}

