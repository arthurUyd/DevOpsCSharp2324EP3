using Microsoft.Playwright.NUnit;
using Shouldly;
using System.Threading.Tasks;

namespace SVK.PlaywrightTests.Laadbonnen;

[Parallelizable(ParallelScope.Self)]
public class LaadbonToevoegenTests : PageTest
{
    private const string ServerBaseUrl = "https://localhost:7093";
    private const string TransportOpdrachtId = "1";  // Replace with an actual ID or parameterize it as needed

    [Test]
    public async Task Should_Add_Laadbon_Successfully()
    {
        // Simulate login
        await SimulateLoginAsync();
        Console.WriteLine("done");

        // Navigate to Laadbon toevoegen page
        var url = $"{ServerBaseUrl}/transportopdracht/laadbon/{TransportOpdrachtId}";
        Console.WriteLine($"Navigating to: {url}");
        await Page.GotoAsync(url);
        await Page.WaitForSelectorAsync("[data-test-id='create']");
        var pageTitle = await Page.TitleAsync();
        pageTitle.ShouldBe("Laadbon Toevoegen");

        await Page.WaitForSelectorAsync("form");

        // Form invullen
        await Page.FillAsync("data-test-id=nummer", "12345");

        await Page.FillAsync("[data-test-id='transporteur']", "Sample Transporteur");

        await Page.ClickAsync("[data-test-id='productknop']"); 
        await Page.FillAsync("input.input[placeholder='Zoek producten...']", "ijzeren");
        await Page.CheckAsync("input[type='checkbox']"); 

        await Page.SetInputFilesAsync("input[type='file']", "../SampleFiles/leveringsbon.pdf");

        await Page.FillAsync("[data-test-id='al1']", "123 Sample Street");
        await Page.FillAsync("[data-test-id='al2']", "Apt 4B");
        await Page.FillAsync("[data-test-id='postcode']", "12345");
        await Page.FillAsync("[data-test-id='city']", "Sample City");
        await Page.FillAsync("[data-test-id='country']", "Sample Country");

        await Page.ClickAsync("button[type='submit']");

       //success check
        await Page.WaitForSelectorAsync("[data-test-id='detail']");

    }

    private async Task SimulateLoginAsync()
    {
        await Page.GotoAsync($"{ServerBaseUrl}/authentication/login");

        await Page.FillAsync("input[name='username']", "lader@lader.com");
        await Page.FillAsync("input[name='password']", "Lader.123");
        await Page.ClickAsync("button[type='submit']");

        await Page.WaitForURLAsync($"{ServerBaseUrl}/");
    }
}
