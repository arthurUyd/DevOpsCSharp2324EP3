using SVK.Client.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SVK.Shared.TransportOpdrachten;

namespace SVK.Client.TransportOpdrachten;

public partial class Create
{
    private IBrowserFile image;
    private TransportOpdrachtDto.Mutate opdracht = new();
    [Inject] public ITransportOpdrachtService Service { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IStorageService StorageService { get; set; } = default!;

    private async Task CreateOpdrachtAsync()
    {
        TransportOpdrachtResult.Create result = await Service.CreateAsync(opdracht);
        await StorageService.UploadImageAsync(result.UploadUri, image!);
        NavigationManager.NavigateTo($"transportopdrachten");
    }
    private void LoadImage(InputFileChangeEventArgs e)
    {
        image = e.File;
        opdracht.ImageContentType = image.ContentType;
    }
}