using Serilog;

namespace TransfusionAPI.WebUI.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        var loggerConfig = new LoggerConfiguration();
        if (builder.Environment.IsDevelopment())
            loggerConfig.ConfigureForDebugging();
        else
            loggerConfig.ConfigureForProduction();
        Log.Logger = loggerConfig.CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();

        return builder;
    }
}
