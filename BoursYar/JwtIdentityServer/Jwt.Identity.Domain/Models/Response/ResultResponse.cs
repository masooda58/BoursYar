using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Identity.Domain.Models.ResultModels.Response
{
  public  class ResultResponse
    {
        public bool Successed { get; set; }
        public IEnumerable<string> ErrorMessage{ get; set; }
        public ResultResponse( bool successed ,IEnumerable<string> errorMessage)
        {

            Successed = successed;
            ErrorMessage = errorMessage;
        }

        public ResultResponse(bool successed, string errorMessage) :
            this(successed, new List<string>(){ errorMessage }){}
    }
}
