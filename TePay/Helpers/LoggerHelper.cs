using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace TePay.Helpers;

/// <summary>
/// A helper class to create a Serilog logger with a specific configuration.
/// This class provides a method to create a logger that is configured to output logs to the console with a specified format.
/// The logger includes contextual information such as timestamp, log level, and source context for better debugging and analysis.
/// </summary>
internal static class LoggerHelper
{
    /// <summary>
    /// Creates a logger for a specific type <typeparamref name="T"/> using the provided configuration.
    /// If no configuration is provided, a default console logger is used.
    /// </summary>
    /// <typeparam name="T">The type for which the logger is created.</typeparam>
    /// <param name="loggerConfiguration">Optional: Custom Serilog configuration. If null, a default configuration is used.</param>
    /// <returns>An instance of <see cref="ILogger"/> configured for the specified type.</returns>
    public static ILogger CreateLogger<T>(LoggerConfiguration? loggerConfiguration = null)
    {
        if (loggerConfiguration == null)
        {
            loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:dd-MM-yy HH:mm} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}",
                    theme: ConsoleTheme.None
                );
        }

        return loggerConfiguration.CreateLogger().ForContext<T>();
    }
}
