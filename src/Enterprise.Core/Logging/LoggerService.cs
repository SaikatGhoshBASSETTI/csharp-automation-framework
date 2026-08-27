using Serilog;

namespace Enterprise.Core.Logging;

public static class LoggerService
{
    static LoggerService()
    {
        string logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs", "automation_.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}")
            .Enrich.FromLogContext()
            .CreateLogger();
    }

    public static void Information(string message) => Log.Information(message);
    public static void Warning(string message) => Log.Warning(message);
    public static void Error(string message, Exception? ex = null) => Log.Error(ex, message);
}