using Microsoft.AspNetCore.Components;
using SVK.Shared.Laadbonnen;
using SVK.Shared.Producten;
using SVK.Shared.TransportOpdrachten;

namespace SVK.Client.TransportOpdrachten;

public partial class Detail
{
    private TransportOpdrachtDto.Detail? opdracht;
    private List<string> productnamen = default!;
    [Parameter] public int Id { get; set; }
    [Inject] public ITransportOpdrachtService Service { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    private string FormattedOpdrachtDate => opdracht!.Datum?.ToString("yyyy-MM-dd") ?? string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        await GetOpdrachtAsync();
       
    }
   
    private async Task GetOpdrachtAsync()
    {
        opdracht = await Service.GetDetailAsync(Id);
    }
   


}