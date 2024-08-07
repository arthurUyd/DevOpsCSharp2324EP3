using Client.Extensions;
using Shared.Gebruikers;
using Shared.TransportOpdrachten;
using System.Net.Http.Json;

namespace Client.TransportOpdrachten;

public class TransportOpdrachtService : ITransportOpdrachtService
{

    private readonly HttpClient client;
    private const string endpoint = "api/transportopdracht";

    public TransportOpdrachtService(HttpClient client)
    {
        this.client = client;
    }

    public Task<TransportOpdrachtResult.Create> CreateAsync(TransportOpdrachtDto.Mutate model)
    {
        throw new NotImplementedException();
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
