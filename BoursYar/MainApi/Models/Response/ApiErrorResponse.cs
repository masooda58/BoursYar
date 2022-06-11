using System.Collections.Generic;

namespace IdentityApi.Models.Response
{
    public class ApiErrorResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public ApiErrorResponse(string errorMessage):this(new List<string>(){errorMessage})
        {
            
        }

        public ApiErrorResponse(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
