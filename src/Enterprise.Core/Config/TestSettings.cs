namespace Enterprise.Core.Config;

public class TestSettings
{
    public string Environment { get; set; } = "QA";
    public string BaseUrl { get; set; } = string.Empty;
    public int ExplicitWaitTimeout { get; set; } = 10;
    public BrowserSettings BrowserSettings { get; set; } = new();
}

public class BrowserSettings
{
    public BrowserType Type { get; set; } = BrowserType.Chrome;
    public bool Headless { get; set; } = false;
}