using Dapper.Contrib.Extensions;

namespace DAL
{

    [Table("Codal")] // dapper contrib
    public class Codal
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string letter_number { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string pdf { get; set; }
        public string excel { get; set; }
    }
}

