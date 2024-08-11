using SVK.Client.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SVK.Shared.TransportOpdrachten;

namespace SVK.Client.TransportOpdrachten;

public partial class Edit
{
    private IBrowserFile image;
    private TransportOpdrachtDto.Mutate opdracht = new();
    [Parameter] public int Id { get; set; }
    [Inject] public ITransportOpdrachtService Service { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var detail = await Service.GetDetailAsync(Id);
        opdracht = new TransportOpdrachtDto.Mutate
        {
            Datum = detail.Datum,
            Routenummer = detail.Routenummer,
            Lader = detail.Lader.Naam,
            Nummerplaat = detail.Nummerplaat,
            
        };
    }
    private async Task EditOpdrachtAsync()
    {
        await Service.EditAsync(Id, opdracht);
        NavigationManager.NavigateTo($"transportopdracht/{Id}");
    }
   
}