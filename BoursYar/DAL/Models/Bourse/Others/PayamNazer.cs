using Newtonsoft.Json;
using Dapper.Contrib.Extensions;

namespace DAL
{
   
    [Table("PayamNazer")] // dapper contrib
    public class PayamNazer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
       
        [JsonProperty("head", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Head { get; set; }

        [JsonProperty("time", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Time { get; set; }

        [JsonProperty("text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

    }
}
