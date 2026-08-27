using Reqnroll;
using AventStack.ExtentReports;
using Enterprise.Core;
using Enterprise.Core.Config;
using Enterprise.Core.Logging;
using Enterprise.Core.Reporting;
using Enterprise.Core.Utils;

namespace Enterprise.Tests.Hooks;

[Binding]
public class TestHooks
{
    private readonly ScenarioContext _scenarioContext;

    public TestHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        // রিপোর্ট ইঞ্জিন ইনিশিয়ালাইজ করা
        _ = ExtentManager.Instance;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        string scenarioName = _scenarioContext.ScenarioInfo.Title;
        LoggerService.Information($"=== Scenario Started: {scenarioName} ===");

        // ExtentReports-এ নতুন টেস্ট যোগ করা
        ExtentManager.CreateTest(scenarioName);

        var config = ConfigReader.Instance;
        DriverFactory.CreateDriver(config.BrowserSettings.Type, config.BrowserSettings.Headless, config.ExplicitWaitTimeout);
    }

    [AfterStep]
    public void AfterStep()
    {
        var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
        var stepName = _scenarioContext.StepContext.StepInfo.Text;
        var extentTest = ExtentManager.GetTest();

        if (_scenarioContext.TestError == null)
        {
            extentTest?.Pass($"{stepType} {stepName}");
        }
        else
        {
            extentTest?.Fail($"{stepType} {stepName} - Error: {_scenarioContext.TestError.Message}");
        }
    }

    [AfterScenario]
    public void AfterScenario()
    {
        string scenarioName = _scenarioContext.ScenarioInfo.Title;
        var extentTest = ExtentManager.GetTest();

        if (_scenarioContext.TestError != null)
        {
            LoggerService.Error($"Scenario Failed: {scenarioName}");
            var driver = DriverFactory.GetDriver();
            string cleanScenarioName = scenarioName.Replace(" ", "_");
            
            // স্ক্রিনশট ক্যাপচার ও রিপোর্টে সংযুক্ত করা
            string screenshotPath = ScreenshotUtils.CaptureScreenshot(driver, $"FAIL_{cleanScenarioName}");
            if (!string.IsNullOrEmpty(screenshotPath))
            {
                extentTest?.Fail("Screenshot on Failure", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
        }
        else
        {
            LoggerService.Information($"Scenario Passed: {scenarioName}");
        }

        DriverFactory.QuitDriver();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        // টেস্ট রান শেষে রিপোর্ট ফাইল ফ্লাশ করে ডিস্কে রাইট করা
        ExtentManager.Flush();
    }
}