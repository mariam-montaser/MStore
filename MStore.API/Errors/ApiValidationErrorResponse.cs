using System.Collections.Generic;

namespace MStore.API.Errors
{
    public class ApiValidationErrorResponse: ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse():base(400)
        {

        }
    }
}
