using System;
using Dapper.Contrib.Extensions;

namespace DAL
{
    [Table("Logger")]
    public class Logger
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReqTime { get; set; }
        public string Status { get; set; }
        public bool Success { get; set; }

    }
}
