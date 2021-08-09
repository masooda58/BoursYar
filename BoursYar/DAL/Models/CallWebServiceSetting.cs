using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Dapper.Contrib.Extensions.Table("CallWebServiceSetting")]
   public class CallWebServiceSetting
    {

        // public int Id { get; set; }
        [ExplicitKey] 
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        public string Name { get; set; }
        [Display(Name = "فعال")]
        public bool Faal { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string FinishTime { get; set; }
        public string ClassType { get; set; }
        public string ClassJsonType { get; set; }
        public bool NeedAddDate { get; set; }
        public string Url { get; set; }
        [Required]
        public int Interval { get; set; }
    }
}
