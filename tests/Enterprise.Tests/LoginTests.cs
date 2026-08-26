using NUnit.Framework;
using FluentAssertions;
using Enterprise.Pages;

namespace Enterprise.Tests;

[TestFixture]
public class LoginTests : BaseTest
{
    [Test]
    public void VerifySuccessfulLogin()
    {
        var loginPage = new LoginPage(Driver);
        
        loginPage.NavigateTo(Config.BaseUrl);
        
        // Pause so you can visually watch Chrome on screen
        System.Threading.Thread.Sleep(5000);

        loginPage.PerformLogin("student", "Password123");
        
        string successMessage = loginPage.GetSuccessMessage();
        successMessage.Should().Be("Logged In Successfully");
    }
}