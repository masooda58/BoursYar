using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class FavNamad
    {
        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public string FinalPrice { get; set; }
        public string FinalPriceChange { get; set; }
        public string ClosePrice { get; set; }
        public string ClosePriceChange { get; set; }
        public string LowestPrice { get; set; }
        public string HighestPrice { get; set; }
        public string N { get; set; }
        public string Volume { get; set; }
        public string Value { get; set; }
    }
}
