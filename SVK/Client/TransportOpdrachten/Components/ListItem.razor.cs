using Microsoft.AspNetCore.Components;
using Shared.TransportOpdrachten;

namespace Client.TransportOpdrachten.Components;

public partial class ListItem
{
    [Parameter, EditorRequired] public TransportOpdrachtDto.Index Opdracht { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    private string FormattedOpdrachtDate => Opdracht.Datum?.ToString("yyyy-MM-dd") ?? string.Empty;

}