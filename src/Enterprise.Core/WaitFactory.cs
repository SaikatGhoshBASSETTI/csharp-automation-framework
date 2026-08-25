using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Enterprise.Core;

public static class WaitFactory
{
    public static IWebElement WaitForElementVisible(this IWebDriver driver, By locator, int timeoutInSeconds = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
        
        return wait.Until(d =>
        {
            var element = d.FindElement(locator);
            return element.Displayed ? element : null;
        }) ?? throw new NoSuchElementException($"Element located by '{locator}' was not visible within {timeoutInSeconds} seconds.");
    }

    public static IWebElement WaitForElementClickable(this IWebDriver driver, By locator, int timeoutInSeconds = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));

        return wait.Until(d =>
        {
            var element = d.FindElement(locator);
            return (element.Displayed && element.Enabled) ? element : null;
        }) ?? throw new ElementNotInteractableException($"Element located by '{locator}' was not clickable within {timeoutInSeconds} seconds.");
    }
}