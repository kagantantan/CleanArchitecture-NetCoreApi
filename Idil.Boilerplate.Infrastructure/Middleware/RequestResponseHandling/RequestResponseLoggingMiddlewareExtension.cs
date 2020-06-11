using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Idil.Boilerplate.Infrastructure.Middleware.RequestResponseHandling
{
    public static class RequestResponseLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
