using Reqnroll;
using Enterprise.Core;
using Enterprise.Core.Config;

namespace Enterprise.Tests.Hooks;

[Binding]
public class TestHooks
{
    [BeforeScenario]
    public void BeforeScenario()
    {
        // Fetches configuration and initializes a thread-isolated driver for each scenario
        var config = ConfigReader.Instance;
        DriverFactory.CreateDriver(config.BrowserSettings.Type, config.BrowserSettings.Headless, config.ExplicitWaitTimeout);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        // Quits the browser and cleans up thread memory after scenario execution
        DriverFactory.QuitDriver();
    }
}