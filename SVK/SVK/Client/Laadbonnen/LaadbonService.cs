using SVK.Shared.Laadbonnen;
using System.Net.Http.Json;

namespace SVK.Client.Laadbonnen;

public class LaadbonService : ILaadbonService
{

    private readonly HttpClient client;
    private const string endpoint = "api/laadbon";

    public LaadbonService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<LaadbonResult.Create> CreateAsync(int id, LaadbonDto.Mutate model)
    {
       var response = await client.PostAsJsonAsync($"{endpoint}/{id}", model);  
       return await response.Content.ReadFromJsonAsync<LaadbonResult.Create>();
    }

}
