using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class Logger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReqTime { get; set; }
        public string Status { get; set; }
        public bool Success { get; set; }
    }
}
