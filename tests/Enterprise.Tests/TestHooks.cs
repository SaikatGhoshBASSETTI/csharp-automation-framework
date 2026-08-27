using Reqnroll;
using Enterprise.Core;
using Enterprise.Core.Config;
using Enterprise.Core.Logging;
using Enterprise.Core.Utils;

namespace Enterprise.Tests.Hooks;

[Binding]
public class TestHooks
{
    private readonly ScenarioContext _scenarioContext;

    // Context Injection-এর মাধ্যমে ScenarioContext রিসিভ করা হচ্ছে
    public TestHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        string scenarioName = _scenarioContext.ScenarioInfo.Title;
        LoggerService.Information($"=== Scenario Started: {scenarioName} ===");

        var config = ConfigReader.Instance;
        DriverFactory.CreateDriver(config.BrowserSettings.Type, config.BrowserSettings.Headless, config.ExplicitWaitTimeout);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        string scenarioName = _scenarioContext.ScenarioInfo.Title;

        // টেস্ট ফেল করলে স্ক্রিনশট নেওয়া হবে
        if (_scenarioContext.TestError != null)
        {
            LoggerService.Error($"Scenario Failed: {scenarioName}. Error: {_scenarioContext.TestError.Message}");
            
            var driver = DriverFactory.GetDriver();
            string cleanScenarioName = scenarioName.Replace(" ", "_");
            ScreenshotUtils.CaptureScreenshot(driver, $"FAIL_{cleanScenarioName}");
        }
        else
        {
            LoggerService.Information($"Scenario Passed: {scenarioName}");
        }

        DriverFactory.QuitDriver();
        LoggerService.Information($"=== Scenario Ended: {scenarioName} ===\n");
    }
}