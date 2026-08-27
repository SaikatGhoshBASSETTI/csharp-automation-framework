using Reqnroll;
using FluentAssertions;
using Enterprise.Core;
using Enterprise.Core.Config;
using Enterprise.Pages;

namespace Enterprise.Tests.Steps;

[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;

    public LoginSteps()
    {
        // Reqnroll automatically binds this during execution using our ThreadLocal driver
        _loginPage = new LoginPage(DriverFactory.GetDriver());
    }

    [Given(@"I navigate to the login page")]
    public void GivenINavigateToTheLoginPage()
    {
        _loginPage.NavigateTo(ConfigReader.Instance.BaseUrl);
    }

    [When(@"I enter credentials ""(.*)"" and ""(.*)""")]
    public void WhenIEnterCredentialsAnd(string username, string password)
    {
        _loginPage.PerformLogin(username, password);
    }

    [Then(@"I should see the success header ""(.*)""")]
    public void ThenIShouldSeeTheSuccessHeader(string expectedHeader)
    {
        string actualHeader = _loginPage.GetSuccessMessage();
        actualHeader.Should().Be(expectedHeader);
    }
}