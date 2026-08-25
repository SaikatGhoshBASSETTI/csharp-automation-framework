using OpenQA.Selenium;

namespace Enterprise.Pages;

// Inherit from BasePage to reuse driver interactions and dynamic waits
public class LoginPage : BasePage
{
    // 1. Private Locators: Strictly encapsulated so test classes cannot manipulate raw selectors
    private readonly By _usernameInput = By.Id("username");
    private readonly By _passwordInput = By.Id("password");
    private readonly By _submitButton = By.Id("submit");
    private readonly By _successHeader = By.ClassName("post-title");

    // 2. Constructor: Passes the thread-isolated driver instance up to BasePage
    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    // 3. Encapsulated Action Methods
    public void NavigateTo(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public void PerformLogin(string username, string password)
    {
        // Calls SendKeys and Click inherited from BasePage, which trigger automatic dynamic waits
        SendKeys(_usernameInput, username);
        SendKeys(_passwordInput, password);
        Click(_submitButton);
    }

    public string GetSuccessMessage()
    {
        // Returns header text after waiting for visibility
        return GetText(_successHeader);
    }
}