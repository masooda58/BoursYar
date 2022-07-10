using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public partial class CandelPattern

    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Date { get; set; }

        [JsonProperty("data", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<Datum>> Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("instance_code", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string InstanceCode { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

}
