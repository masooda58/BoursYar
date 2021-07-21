using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary2.Models
{
    public partial class CallWebServiceSetting
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Faal { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string ClassType { get; set; }
        public string ClassJsonType { get; set; }
        public bool NeedAddDate { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
    }
}
