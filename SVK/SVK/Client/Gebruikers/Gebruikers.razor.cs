using Microsoft.AspNetCore.Components;
using SVK.Shared.Gebruikers; 

namespace SVK.Client.Gebruikers;

public partial class Gebruikers
{
    private IEnumerable<GebruikerDto.Index>? gebruikers;
    private string? searchTerm;

    [Inject] public IGebruikerService GebruikersService { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        GebruikerRequest.Index request = new();
        
        var response = await GebruikersService.GetIndexAsync(request);
        Console.WriteLine(response);
        gebruikers = response.Gebruikers;
    }


}