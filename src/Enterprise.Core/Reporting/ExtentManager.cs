using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace Enterprise.Core.Reporting;

public static class ExtentManager
{
    private static readonly Lazy<ExtentReports> _extent = new(InitializeExtent);
    private static readonly ThreadLocal<ExtentTest?> _test = new();

    public static ExtentReports Instance => _extent.Value;

    private static ExtentReports InitializeExtent()
    {
        string reportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "reports");
        if (!Directory.Exists(reportDirectory))
        {
            Directory.CreateDirectory(reportDirectory);
        }

        string reportPath = Path.Combine(reportDirectory, "ExecutionReport.html");
        
        var sparkReporter = new ExtentSparkReporter(reportPath);
        sparkReporter.Config.DocumentTitle = "Automation Test Execution Report";
        sparkReporter.Config.ReportName = "Enterprise UI Test Results";
        sparkReporter.Config.Theme = Theme.Standard;

        var extent = new ExtentReports();
        extent.AttachReporter(sparkReporter);
        
        // v5-এ 'SetSystemInfo'-এর বদলে 'AddSystemInfo' ব্যবহার করা হয়
        extent.AddSystemInfo("Framework", "C# .NET 8 / Selenium 4");
        extent.AddSystemInfo("Environment", "QA");
        extent.AddSystemInfo("User", Environment.UserName);

        return extent;
    }

    public static ExtentTest CreateTest(string testName, string? description = null)
    {
        var test = Instance.CreateTest(testName, description);
        _test.Value = test;
        return test;
    }

    public static ExtentTest? GetTest() => _test.Value;

    public static void Flush()
    {
        if (_extent.IsValueCreated)
        {
            Instance.Flush();
        }
    }
}