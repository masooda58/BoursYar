using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class Crypto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ChangePercent24H { get; set; }
        public string Volume24H { get; set; }
        public string MarketCap { get; set; }
        public string Icon { get; set; }
    }
}
