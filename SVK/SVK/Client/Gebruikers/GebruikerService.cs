using SVK.Shared.Gebruikers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using SVK.Client.Extensions;

namespace SVK.Client.Gebruikers;

public class GebruikerService : IGebruikerService
{
    private readonly HttpClient client;
    private const string endpoint = "api/gebruiker";

    public GebruikerService(HttpClient client)
    {
        this.client = client;
    }
    public Task<int> CreateAsync(GebruikerDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task<GebruikerDto.Detail> GetDetailAsync(int id)
    {
        var response = await client.GetFromJsonAsync<GebruikerDto.Detail>($"{endpoint}/{id}");
        return response; 
    }

    public async Task<GebruikerResult.Index> GetIndexAsync(GebruikerRequest.Index request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        var response = await client.GetFromJsonAsync<GebruikerResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }
}
