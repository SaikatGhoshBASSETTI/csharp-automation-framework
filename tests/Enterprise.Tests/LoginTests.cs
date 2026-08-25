using OpenQA.Selenium;

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
        Driver.Navigate().GoToUrl(url);
        return this;
    }

    public LoginPage EnterUsername(string username)
    {
        SendKeys(_usernameInput, username);
        return this;
    }

    public LoginPage EnterPassword(string password)
    {
        SendKeys(_passwordInput, password);
        return this;
    }

    public LoginPage ClickLogin()
    {
        Click(_submitButton);
        return this;
    }

    // High-level wrapper method returning 'this' for method chaining
    public LoginPage PerformLogin(string username, string password)
    {
        return EnterUsername(username)
               .EnterPassword(password)
               .ClickLogin();
    }

    public string GetSuccessMessage()
    {
        return GetText(_successHeader);
    }
}