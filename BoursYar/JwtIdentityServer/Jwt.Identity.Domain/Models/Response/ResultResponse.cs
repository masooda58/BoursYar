using System.Collections.Generic;

namespace Jwt.Identity.Domain.Models.Response
{
  public  class ResultResponse
    {
        public bool Successed { get; set; }
        public IEnumerable<string> Message{ get; set; }
        public object ResponseValues{get; set; }
        public ResultResponse( bool successed ,IEnumerable<string> message, object values=null)
        {

            Successed = successed;
            Message = message;
            ResponseValues = values ?? new{};
           
        }

        public ResultResponse(bool successed, string message) :
            this(successed, new List<string>(){ message }){}
        public ResultResponse(bool successed, string message,object values) :
            this(successed, new List<string>(){ message },values){}

    }
}
