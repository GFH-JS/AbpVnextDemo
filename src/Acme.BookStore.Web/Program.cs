using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Acme.BookStore.Web;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        try
        {
            var logPath = $"Logs/{DateTime.Now.ToString("yyyyMMdd")}/"; 
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("serilog.json").Build())
               
                .CreateLogger();

             //.WriteTo.File($"{logPath}log-error-.log",
             //                 rollingInterval: RollingInterval.Day,
             //                 fileSizeLimitBytes: 10485760,
             //                 rollOnFileSizeLimit: true,
             //                 restrictedToMinimumLevel: LogEventLevel.Error)


            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
//                .UseSerilog((context, services, loggerConfiguration) =>
//                {
//                    loggerConfiguration
//#if DEBUG
//                        .MinimumLevel.Debug()
//#else
//            .MinimumLevel.Information()
//#endif
//                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
//                        .Enrich.FromLogContext()
//                        .WriteTo.Async(c => c.File("Logs/logs.txt"))
//                        .WriteTo.Async(c => c.Console())
//                        .WriteTo.Async(c => c.AbpStudio(services));
//                });
            await builder.AddApplicationAsync<BookStoreWebModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
