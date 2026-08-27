using OpenQA.Selenium;
using Enterprise.Core.Logging;

namespace Enterprise.Pages;

public class LoginPage : BasePage
{
    private readonly By _usernameInput = By.Id("username");
    private readonly By _passwordInput = By.Id("password");
    private readonly By _submitButton = By.Id("submit");
    private readonly By _successHeader = By.ClassName("post-title");

    public LoginPage(IWebDriver driver) : base(driver) { }

    public LoginPage NavigateTo(string url)
    {
        LoggerService.Information($"Navigating to URL: {url}");
        Driver.Navigate().GoToUrl(url);
        return this;
    }

    public LoginPage EnterUsername(string username)
    {
        LoggerService.Information($"Entering username: {username}");
        SendKeys(_usernameInput, username);
        return this;
    }

    public LoginPage EnterPassword(string password)
    {
        LoggerService.Information("Entering password...");
        SendKeys(_passwordInput, password);
        return this;
    }

    public LoginPage ClickLogin()
    {
        LoggerService.Information("Clicking on Submit button.");
        Click(_submitButton);
        return this;
    }

    public LoginPage PerformLogin(string username, string password)
    {
        return EnterUsername(username)
               .EnterPassword(password)
               .ClickLogin();
    }

    public string GetSuccessMessage()
    {
        LoggerService.Information("Fetching success message header text.");
        return GetText(_successHeader);
    }
}