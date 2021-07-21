using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class Codal
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string LetterNumber { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Pdf { get; set; }
        public string Excel { get; set; }
    }
}
