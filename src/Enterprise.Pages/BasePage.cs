using OpenQA.Selenium;
using Enterprise.Core;

namespace Enterprise.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;

    protected BasePage(IWebDriver driver)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    protected void Click(By locator)
    {
        Driver.WaitForElementClickable(locator).Click();
    }

    protected void SendKeys(By locator, string text)
    {
        var element = Driver.WaitForElementVisible(locator);
        element.Clear();
        element.SendKeys(text);
    }

    protected string GetText(By locator)
    {
        return Driver.WaitForElementVisible(locator).Text;
    }
}