using Microsoft.Extensions.Configuration;

namespace Enterprise.Core.Config;

public static class ConfigReader
{
    private static readonly Lazy<TestSettings> _settings = new(InitializeSettings);

    public static TestSettings Instance => _settings.Value;

    private static TestSettings InitializeSettings()
    {
        // 1. Determine active environment (Defaults to QA if not supplied via CLI)
        string environment = System.Environment.GetEnvironmentVariable("TEST_ENV") ?? "QA";

        // 2. Build configuration pipeline
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        IConfiguration configuration = builder.Build();

        // 3. Bind JSON configuration directly to strongly-typed TestSettings object
        var settings = new TestSettings();
        configuration.Bind(settings);

        return settings;
    }
}