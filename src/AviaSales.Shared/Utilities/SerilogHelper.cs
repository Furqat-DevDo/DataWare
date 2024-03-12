using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;
using Serilog.Sinks.TelegramBot;

namespace AviaSales.Shared.Utilities;

/// <summary>
/// Helper class for configuring Serilog in an ASP.NET Core application.
/// </summary>
public static class SerilogHelper
{
    /// <summary>
    /// Configuration action for setting up Serilog with various enrichers, sinks, and options.
    /// </summary>
    public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
        (context, configuration) =>
        {
            var tgLogOptions = context.Configuration.GetSection("Serilog:Telegram").Get<TelegramLogOptions>();
            
            configuration
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationIdHeader("CorrelationId")
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Destructure.ToMaximumDepth(9)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File("./Logs/log.txt", 
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true);

            if (tgLogOptions != default && !string.IsNullOrWhiteSpace(tgLogOptions.Token)
                && !string.IsNullOrWhiteSpace(tgLogOptions.ChatId))
            {
                configuration.WriteTo.TelegramBot(
                    tgLogOptions.Token,
                    tgLogOptions.ChatId,
                    context.HostingEnvironment.ApplicationName.ToLower().Replace(".", "-"),
                    restrictedToMinimumLevel: LogEventLevel.Warning
                );
            }

            configuration.ReadFrom.Configuration(context.Configuration, 
                readerOptions: new ConfigurationReaderOptions(typeof(ConsoleLoggerConfigurationExtensions).Assembly));
        };
    
}

public class TelegramLogOptions
{
    public string? Token { get; set; }
    public string? ChatId { get; set; }
}