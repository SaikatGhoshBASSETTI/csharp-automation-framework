using NUnit.Framework;
using OpenQA.Selenium;
using Enterprise.Core;
using Enterprise.Core.Config;

namespace Enterprise.Tests;

public class BaseTest
{
    protected IWebDriver Driver => DriverFactory.GetDriver();
    protected TestSettings Config => ConfigReader.Instance;

    [SetUp]
    public void SetUp()
    {
        // Read configuration settings dynamically
        var browserType = Config.BrowserSettings.Type;
        var isHeadless = Config.BrowserSettings.Headless;
        var timeout = Config.ExplicitWaitTimeout;

        DriverFactory.CreateDriver(browserType, isHeadless: false, timeout);
    }

    [TearDown]
    public void TearDown()
    {
        DriverFactory.QuitDriver();
    }
}