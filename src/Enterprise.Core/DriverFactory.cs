using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Enterprise.Core;

public enum BrowserType
{
    Chrome,
    Firefox,
    Edge
}

public static class DriverFactory
{
    private static readonly ThreadLocal<IWebDriver?> _driver = new();

    public static IWebDriver GetDriver()
    {
        return _driver.Value ?? throw new InvalidOperationException("WebDriver instance not initialized for current thread. Call CreateDriver() first.");
    }

    public static IWebDriver CreateDriver(BrowserType browserType, bool isHeadless = false, int timeoutSeconds = 30)
    {
        IWebDriver driver = browserType switch
        {
            BrowserType.Chrome => CreateChromeDriver(isHeadless),
            BrowserType.Firefox => CreateFirefoxDriver(isHeadless),
            BrowserType.Edge => CreateEdgeDriver(isHeadless),
            _ => throw new ArgumentOutOfRangeException(nameof(browserType), $"Browser '{browserType}' is not supported.")
        };

        // Configure default timeouts to harden against network latency
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeoutSeconds);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0); // Standard: rely on Explicit Waits, avoid implicit delays
        driver.Manage().Window.Maximize();

        _driver.Value = driver;
        return _driver.Value;
    }

    private static IWebDriver CreateChromeDriver(bool isHeadless)
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage"); // Prevents shared memory crashes in Docker containers
        if (isHeadless) options.AddArgument("--headless=new");

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool isHeadless)
    {
        var options = new FirefoxOptions();
        if (isHeadless) options.AddArgument("--headless");
        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdgeDriver(bool isHeadless)
    {
        var options = new EdgeOptions();
        if (isHeadless) options.AddArgument("--headless=new");
        return new EdgeDriver(options);
    }

    public static void QuitDriver()
    {
        if (_driver.IsValueCreated && _driver.Value != null)
        {
            _driver.Value.Quit();
            _driver.Value.Dispose();
            _driver.Value = null;
        }
    }
}