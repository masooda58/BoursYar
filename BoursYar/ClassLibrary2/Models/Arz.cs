using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class Arz
    {
        public int Id { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime MiladiDate { get; set; }
        public string ShamsiDate { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Change { get; set; }
        public string ChangePercent { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string LastUpdate { get; set; }
        public string JalaliLastUpdate { get; set; }
    }
}
