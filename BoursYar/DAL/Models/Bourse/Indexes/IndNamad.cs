using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAL
{

    [Table("IndNamad")]
    public class IndNamad
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

        [JsonProperty("effect", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Effect { get; set; }
    }
}
