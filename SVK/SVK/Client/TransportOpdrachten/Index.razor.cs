using Microsoft.AspNetCore.Components;
using SVK.Shared.TransportOpdrachten;
namespace SVK.Client.TransportOpdrachten;

public partial class Index
{
    private IEnumerable<TransportOpdrachtDto.Index>? opdrachten;
    private string? searchTerm;

    [Inject] public ITransportOpdrachtService Service { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        TransportOpdrachtRequest.Index request = new()
        {
            Page = 1,
            PageSize = 100,
            Searchterm = Searchterm
        };

        searchTerm = Searchterm;
        var response = await Service.GetIndexAsync(request);
        opdrachten = response.TransportOpdrachten;
    }

    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterOpdrachten();
    }
    
    private void FilterOpdrachten()
    {
        Dictionary<string, object?> parameters = new();

        parameters.Add(nameof(searchTerm), searchTerm);

        var uri = NavigationManager.GetUriWithQueryParameters(parameters);

        NavigationManager.NavigateTo(uri);
    }
}