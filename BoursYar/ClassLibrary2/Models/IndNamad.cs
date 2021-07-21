using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class IndNamad
    {
        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        public string Market { get; set; }
        public string Name { get; set; }
        public string FinalPrice { get; set; }
        public string Effect { get; set; }
    }
}
