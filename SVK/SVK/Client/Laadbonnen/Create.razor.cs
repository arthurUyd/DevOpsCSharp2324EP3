using Microsoft.AspNetCore.Components;
using SVK.Shared.Laadbonnen;
using SVK.Shared.Producten;

namespace SVK.Client.Laadbonnen;

public partial class Create
{
    private readonly LaadbonDto.Mutate laadbon = new();
    private IEnumerable<ProductDto.Index>? producten;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ILaadbonService LaadbonService { get; set; }
    [Inject] public IProductService ProductService { get; set; }
    [Parameter, EditorRequired] public int transportOpdrachtId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ProductRequest.Index request = new();
        var response = await ProductService.GetIndexAsync(request);
        producten = response.Products;

    }
    private void ToggleProductSelection(ChangeEventArgs e, ProductDto.Index product)
    {
       
        if ((bool)e.Value)
        {
            if (!laadbon.Producten.Contains(product.ProductNaam))
            {
                laadbon.Producten.Add(product.ProductNaam);
            }
        }
        else
        {
            laadbon.Producten.Remove(product.ProductNaam);
        }
    }

    private async Task CreateLaadbonAsync()
    {
        int laadbonId = await LaadbonService.CreateAsync(transportOpdrachtId, laadbon);
        NavigationManager.NavigateTo($"transportopdracht/{transportOpdrachtId}");
    }

}