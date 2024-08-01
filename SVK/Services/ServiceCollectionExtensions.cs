using Microsoft.Extensions.DependencyInjection;
using Services.Files;
using Services.Gebruikers;
using Services.Producten;
using Services.TransportOpdrachten;
using Shared.Gebruikers;
using Shared.Producten;
using Shared.TransportOpdrachten;


namespace Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IGebruikerService, GebruikerService>();
        services.AddScoped<ITransportOpdrachtService, TransportOpdrachtenService>();
        services.AddScoped<IStorageService, BlobStorageService>();

        return services; 
    }
}
