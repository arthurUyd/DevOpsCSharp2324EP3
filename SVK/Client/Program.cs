using Client;
using Client.Gebruikers;
using Client.Infrastructure;
using Client.TransportOpdrachten;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Gebruikers;
using Shared.TransportOpdrachten;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddTransient<CleanErrorHandler>();

builder.Services.AddHttpClient("SvkServerAPI", client => client.BaseAddress = new Uri("https://localhost:7136"))
    .AddHttpMessageHandler<CleanErrorHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SvkServerAPI"));

builder.Services.AddScoped<IGebruikerService, GebruikerService>();
builder.Services.AddScoped<ITransportOpdrachtService, TransportOpdrachtService>();

await builder.Build().RunAsync();
