using NUnit.Framework;

namespace Enterprise.Tests;

// Enables parallel test execution across all tests in this fixture
[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class DemoTest : BaseTest
{
    [Test]
    public void VerifyGoogleTitle()
    {
        Driver.Navigate().GoToUrl("https://www.google.com");
        Assert.That(Driver.Title, Does.Contain("Google"));
    }

    [Test]
    public void VerifyBingTitle()
    {
        Driver.Navigate().GoToUrl("https://www.bing.com");
        Assert.That(Driver.Title, Does.Contain("Bing"));
    }
}