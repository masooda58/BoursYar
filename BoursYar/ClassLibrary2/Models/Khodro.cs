using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class Khodro
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string TypeEn { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Price { get; set; }
        public string ChangePercent { get; set; }
        public bool? MarketPrice { get; set; }
        public string LastUpdate { get; set; }
    }
}
