using Microsoft.Extensions.DependencyInjection;
using SVK.Services.Files;
using SVK.Services.Gebruikers;
using SVK.Services.Laadbonnen;
using SVK.Services.Producten;
using SVK.Services.TransportOpdrachten;
using SVK.Shared.Gebruikers;
using SVK.Shared.Laadbonnen;
using SVK.Shared.Producten;
using SVK.Shared.TransportOpdrachten;


namespace SVK.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IGebruikerService, GebruikerService>();
        services.AddScoped<ITransportOpdrachtService, TransportOpdrachtenService>();
        services.AddScoped<IStorageService, BlobStorageService>();
        services.AddScoped<ILaadbonService, LaadbonService>();

        return services; 
    }
}
