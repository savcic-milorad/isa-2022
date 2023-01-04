using Serilog;
using Serilog.Events;

namespace TransfusionAPI.WebUI.Extensions;

public static class SerilogConfigExtensions
{
    /// <summary>
    /// Logging is configured so that logs get outputted to a file in a json format
    /// </summary>
    public static void ConfigureForProduction(this LoggerConfiguration loggerConfig)
    {
        loggerConfig
            // Global minimum levels applied to all sinks
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient.INameOfClient.ClientHandler", LogEventLevel.Information)

            // Enrichers
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "TransfusionAPI.API")
            .WriteTo.Console();
    }

    public static void ConfigureForDebugging(this LoggerConfiguration loggerConfig)
    {
        loggerConfig
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            // Sinks which are used in development
            .WriteTo.Console();
    }
}
