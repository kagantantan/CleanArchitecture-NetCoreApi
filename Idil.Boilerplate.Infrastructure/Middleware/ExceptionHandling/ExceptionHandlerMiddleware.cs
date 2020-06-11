using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace Idil.Boilerplate.Infrastructure.Middleware.ExceptionHandling
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log our exception using Serilog.
            // Use structured logging to capture the full exception object.
            // Serilog provides the @ destructuring operator to help preserve object structure for our logs.
            _logger.LogError("Exception caught {@exception}", exception);

            //var code = HttpStatusCode.InternalServerError;
            var code = HttpStatusCode.OK; //<-- we do not respond with "InternalServerError" - but rather with isSuccess = false with exception message (this also avoids issues with OpenAPI/Swagger throwing an exception due to server response codes).
                                          //var result = JsonConvert.SerializeObject(exception, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore } );
            var result = JsonConvert.SerializeObject(new { IsSuccess = false, exceptionType = exception.GetType().ToString(), message = exception.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
