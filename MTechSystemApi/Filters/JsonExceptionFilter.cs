using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MTechSystemApi.Models;

namespace MTechSystemApi.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;

        public JsonExceptionFilter(IWebHostEnvironment environment)
        {
            _environment=environment;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            if (_environment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Details = context.Exception.StackTrace;
            }
            else
            {
                error.Message="A server error ocurred";
                error.Details=context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
