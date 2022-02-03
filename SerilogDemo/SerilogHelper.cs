using Destructurama;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace SerilogDemo
{
    public static class SerilogHelper
    {
        public static void InitLoggingConfiguration()
        {
            Serilog.Debugging.SelfLog.Enable(s => Console.WriteLine(s));

            var configuration = new ConfigurationBuilder()
            .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
            // Use appsettings file for each environment to override specific configuration.
            // Example : Configure Seq only in Development as it's only used for local dev.
            .AddJsonFile(path: $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}", optional: true, reloadOnChange: true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                // If you like using attribute to ignore, mask property from logging.
                .Destructure.UsingAttributes()
                // When you would log serialized 'dynamic' objects
                .Destructure.JsonNetTypes()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
