using OpenQA.Selenium;
using Enterprise.Core.Logging;

namespace Enterprise.Core.Utils;

public static class ScreenshotUtils
{
    public static string CaptureScreenshot(IWebDriver driver, string screenshotName)
    {
        try
        {
            var takesScreenshot = (ITakesScreenshot)driver;
            var screenshot = takesScreenshot.GetScreenshot();

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = $"{screenshotName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string filePath = Path.Combine(directoryPath, fileName);

            screenshot.SaveAsFile(filePath);
            LoggerService.Information($"Screenshot saved successfully at: {filePath}");
            return filePath;
        }
        catch (Exception ex)
        {
            LoggerService.Error("Failed to capture screenshot", ex);
            return string.Empty;
        }
    }
}