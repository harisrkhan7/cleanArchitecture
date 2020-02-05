using System;
using cleanArchitecture.Web.Messages.Responses;

namespace cleanArchitecture.Web
{
    public static class ResponseManager
    {
        public static ErrorResponse FormErrorResponse(Exception ex)
        {
            var errorResponse = new ErrorResponse();

            errorResponse.Message = ex.Message;

            if(null != ex.InnerException)
            {
                errorResponse.Message += "\n" + ex.InnerException.Message;
            }

            return errorResponse;
        }
        
    }
}
