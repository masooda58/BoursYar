using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class IndusteryIndex
    {
        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double? Change { get; set; }
        public double? Percent { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
    }
}
