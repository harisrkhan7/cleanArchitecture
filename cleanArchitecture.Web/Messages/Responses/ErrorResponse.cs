using System;
namespace cleanArchitecture.Web.Messages.Responses
{
    public class ErrorResponse
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public ErrorResponse()
        {
        }

        
    }
}
