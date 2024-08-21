using SVK.Client.Extensions;
using SVK.Client.Infrastructure;
using SVK.Shared.Gebruikers;
using SVK.Shared.Producten;
using SVK.Shared.TransportOpdrachten;
using System.Net.Http.Json;

namespace SVK.Client.TransportOpdrachten;

public class TransportOpdrachtService : ITransportOpdrachtService
{

    private readonly HttpClient client;
    private readonly PublicClient publicClient;
    private const string endpoint = "api/transportopdracht";

    public TransportOpdrachtService(HttpClient client, PublicClient publicClient)
    {
        this.client = client;
        this.publicClient = publicClient;
    }

    public async Task<TransportOpdrachtResult.Create> CreateAsync(TransportOpdrachtDto.Mutate request)
    {
        var response = await client.PostAsJsonAsync(endpoint, request);
        return await response.Content.ReadFromJsonAsync<TransportOpdrachtResult.Create>();
    }

    public async Task EditAsync(int transportOpdrachtId, TransportOpdrachtDto.Mutate model)
    {
        var response = await client.PutAsJsonAsync($"{endpoint}/{transportOpdrachtId}", model);
    }

    public async Task<TransportOpdrachtDto.Detail> GetDetailAsync(int id)
    {
        var response = await publicClient.Client.GetFromJsonAsync<TransportOpdrachtDto.Detail>($"{endpoint}/{id}");
        return response;
    }

    public async Task<TransportOpdrachtResult.Index> GetIndexAsync(TransportOpdrachtRequest.Index request)
    {
        var response = await publicClient.Client.GetFromJsonAsync<TransportOpdrachtResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

    public Task CreateAndroidAsync(TransportOpdrachtDto.Mutate model)
    {
        throw new NotImplementedException();
    }
}
