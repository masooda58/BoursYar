using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class BourseIndex
    {
        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        public string Market { get; set; }
        public string State { get; set; }
        public double Index { get; set; }
        public double IndexChange { get; set; }
        public double IndexChangePercent { get; set; }
        public double IndexH { get; set; }
        public double IndexHchange { get; set; }
        public double IndexHchangePercent { get; set; }
        public double MarketValue { get; set; }
        public long TradeNumber { get; set; }
        public double TradeValue { get; set; }
        public double TradeVolume { get; set; }
    }
}
