using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Idil.Boilerplate.Infrastructure.Middleware.ExceptionHandling
{
    public static class ExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
