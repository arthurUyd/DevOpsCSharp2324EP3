using Microsoft.Extensions.DependencyInjection;
using Services.Files;
using Services.Gebruikers;
using Services.Laadbonnen;
using Services.Producten;
using Services.TransportOpdrachten;
using Shared.Gebruikers;
using Shared.Laadbonnen;
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
        services.AddScoped<ILaadbonService, LaadbonService>();
        services.AddScoped<IDocumentService, DocumentenService>();

        return services; 
    }
}
