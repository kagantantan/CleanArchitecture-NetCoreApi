using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Interfaces;
using Idil.Boilerplate.Core.Interfaces.Repositories;
using Idil.Boilerplate.Core.Interfaces.Repositories.Products;
using Idil.Boilerplate.Core.Services.Products.Queries;
using Idil.Boilerplate.Infrastructure.Middleware.ExceptionHandling;
using Idil.Boilerplate.Infrastructure.Middleware.RequestResponseHandling;
using Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer;
using Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer.Repositories;
using Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer.Repositories.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace Idil.Boilerplate.AspNetCoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .MSSqlServer(
                    connectionString: Configuration.GetConnectionString("ApplicationConnection"),
                    sinkOptions: new SinkOptions { TableName = "AuditLogs",AutoCreateSqlTable = true},
                    restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();

            var exceptionLogger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.Async(a =>
                {
                    a.RollingFile("Log-{Date}.txt");
                })
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(exceptionLogger));

            services.AddDbContext<ApplicationContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));

            services.AddMediatR(typeof(GetProductsHandler).GetTypeInfo().Assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("idil.Boilerplate.Api", new OpenApiInfo { Title = "idil.Boilerplate.Api", Version = "v1" });
            });


            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IProductRepository,EfProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint("/swagger/idil.Boilerplate.Api/swagger.json", "idil.Boilerplate.Api V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandlerMiddleware();
            app.UseRequestResponseLoggingMiddleware();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
