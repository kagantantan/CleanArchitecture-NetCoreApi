using Idil.Boilerplate.Core.Interfaces.Repositories;
using Idil.Boilerplate.Core.Interfaces.Repositories.Products;
using Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer;
using Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer.Repositories;
using Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace Idil.Boilerplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .MSSqlServer(
                    connectionString: configuration.GetConnectionString("ApplicationConnection"),
                    sinkOptions: new SinkOptions { TableName = "AuditLogs", AutoCreateSqlTable = true },
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
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IProductRepository, EfProductRepository>();
        }
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
        }
    }
}
