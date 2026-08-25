using NUnit.Framework;
using OpenQA.Selenium;
using Enterprise.Core;

namespace Enterprise.Tests;

public class BaseTest
{
    // Allows derived test classes to easily read the driver instance
    protected IWebDriver Driver => DriverFactory.GetDriver();

    [SetUp]
    public void SetUp()
    {
        // Initializes Chrome for the current thread before each test
        DriverFactory.CreateDriver(BrowserType.Chrome, isHeadless: false);
    }

    [TearDown]
    public void TearDown()
    {
        // Safely quits and disposes the driver instance for the current thread
        DriverFactory.QuitDriver();
    }
}