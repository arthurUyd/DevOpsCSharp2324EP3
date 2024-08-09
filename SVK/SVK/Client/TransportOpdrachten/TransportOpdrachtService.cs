using SVK.Client.Extensions;
using SVK.Shared.Gebruikers;
using SVK.Shared.Producten;
using SVK.Shared.TransportOpdrachten;
using System.Net.Http.Json;

namespace SVK.Client.TransportOpdrachten;

public class TransportOpdrachtService : ITransportOpdrachtService
{

    private readonly HttpClient client;
    private const string endpoint = "api/transportopdracht";

    public TransportOpdrachtService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<TransportOpdrachtResult.Create> CreateAsync(TransportOpdrachtDto.Mutate request)
    {
        var response = await client.PostAsJsonAsync(endpoint, request);
        return await response.Content.ReadFromJsonAsync<TransportOpdrachtResult.Create>();
    }

    public Task EditAsync(int transportOpdrachtId, TransportOpdrachtDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task<TransportOpdrachtDto.Detail> GetDetailAsync(int id)
    {
        var response = await client.GetFromJsonAsync<TransportOpdrachtDto.Detail>($"{endpoint}/{id}");
        return response;
    }

    public async Task<TransportOpdrachtResult.Index> GetIndexAsync(TransportOpdrachtRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<TransportOpdrachtResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }
}
