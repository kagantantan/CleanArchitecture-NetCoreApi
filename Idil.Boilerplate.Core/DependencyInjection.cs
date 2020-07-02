using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Idil.Boilerplate.Core.Services.Products.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Idil.Boilerplate.Core
{
    public static class DependencyInjection
    {
        public static void AddCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
