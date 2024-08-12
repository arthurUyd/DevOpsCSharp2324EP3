using Microsoft.Playwright.NUnit;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.PlaywrightTests.Gebruikers;

[Parallelizable(ParallelScope.Self)]
public class GebruikerTests : PageTest
{
    private const string ServerBaseUrl = "https://localhost:7093";
/*    Helper h = new Helper(ServerBaseUrl);
*/    [Test]
    public async Task Should_Display_Gebruikers_List()
    {
        await SimulateLoginAsync();

        // Navigate to the gebruikers page
        await Page.GotoAsync($"{ServerBaseUrl}/gebruikers");
        await Page.WaitForSelectorAsync(".grid");
        var pageTitle = await Page.TitleAsync();
        pageTitle.ShouldBe("Gebruikers");
      
        // check of er users zijn.
        var userLinks = await Page.QuerySelectorAllAsync(".cell .link");

        if (userLinks.Count > 0)
        {
            foreach (var userLink in userLinks)
            {
                var userName = await userLink.TextContentAsync();
                userName.ShouldNotBeNullOrWhiteSpace();
            }
        }
        else
        {
            var loadingText = await Page.TextContentAsync(".block");
            loadingText.ShouldContain("Loading...");
        }
    }
    private async Task SimulateLoginAsync()
    {
        await Page.GotoAsync($"{ServerBaseUrl}/authentication/login");

        await Page.FillAsync("input[name='username']", "test@test.com");
        await Page.FillAsync("input[name='password']", "Test.123");
        await Page.ClickAsync("button[type='submit']");

        await Page.WaitForNavigationAsync();
    }
}