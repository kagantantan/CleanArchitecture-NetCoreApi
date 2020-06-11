using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Idil.Boilerplate.Infrastructure.Middleware.RequestResponseHandling
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Copy a pointer to the original response body stream
            var originalBodyStream = httpContext.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                httpContext.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                var sw = Stopwatch.StartNew();
                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(httpContext);
                sw.Stop();

                //Format the response from the server
                var response = await FormatResponse(httpContext.Response);

                Log.Information($"Request Method: {httpContext.Request.Method} - Request Path: {httpContext.Request.Path} -  Response: {response}- ElapsedTime(ms): {sw.Elapsed.Milliseconds}");

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}
