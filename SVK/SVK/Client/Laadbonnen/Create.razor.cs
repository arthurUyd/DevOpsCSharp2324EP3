using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SVK.Client.Files;
using SVK.Shared.Laadbonnen;
using SVK.Shared.Producten;

namespace SVK.Client.Laadbonnen;

public partial class Create
{
    private readonly LaadbonDto.Mutate laadbon = new();
    private string searchText = string.Empty;
    private List<ProductDto.Index> filteredProducts;
    private IBrowserFile file;

    private List<ProductDto.Index> selectedProducts = new List<ProductDto.Index>();
    private bool isDropdownOpen = false;

    private IEnumerable<ProductDto.Index>? producten;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ILaadbonService LaadbonService { get; set; }
    [Inject] public IProductService ProductService { get; set; }
    [Parameter, EditorRequired] public int transportOpdrachtId { get; set; }
    [Inject] public IStorageService StorageService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ProductRequest.Index request = new();
        var response = await ProductService.GetIndexAsync(request);
        producten = response.Products;
        filteredProducts = producten.ToList();

    }
    private void UpdateFilter(ChangeEventArgs e)
    {
        // Update the filtered list as the user types in the search box
        searchText = e.Value.ToString();
        filteredProducts = producten
            .Where(p => p.ProductNaam.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
    private void ToggleDropdown()
    {
        isDropdownOpen = !isDropdownOpen;
    }
    private void ToggleProductSelection(ChangeEventArgs e, ProductDto.Index product)
    {
        // Toggle the product selection
        if ((bool)e.Value)
        {
            if (!selectedProducts.Contains(product))
            {
                selectedProducts.Add(product);
            }
        }
        else
        {
            selectedProducts.Remove(product);
        }
        isDropdownOpen = true;

    }

    private async Task CreateLaadbonAsync()
    {
        laadbon.Producten = selectedProducts;
        LaadbonResult.Create result = await LaadbonService.CreateAsync(transportOpdrachtId, laadbon);
        await StorageService.UploadImageAsync(result.UploadUri, file!);

        NavigationManager.NavigateTo($"transportopdracht/{transportOpdrachtId}");
    }
    private void LoadFile(InputFileChangeEventArgs e)
    {
        file = e.File;
        laadbon.ImageContentType = file.ContentType;
    }

}