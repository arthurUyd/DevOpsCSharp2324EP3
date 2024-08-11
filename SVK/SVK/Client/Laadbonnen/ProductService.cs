using SVK.Client.Extensions;
using SVK.Shared.Producten;
using SVK.Shared.TransportOpdrachten;
using System.Net.Http.Json;

namespace SVK.Client.Laadbonnen;

public class ProductService:IProductService
{
    private readonly HttpClient client;
    private const string endpoint = "api/product";

    public ProductService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ProductResult.Index> GetIndexAsync(ProductRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<ProductResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

}
