using SVK.Client;
using SVK.Client.Files;
using SVK.Client.Gebruikers;
using SVK.Client.Infrastructure;
using SVK.Client.Shared;
using SVK.Client.TransportOpdrachten;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SVK.Shared.Gebruikers;
using SVK.Shared.TransportOpdrachten;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddTransient<CleanErrorHandler>();

builder.Services.AddHttpClient("SvkServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
           .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
           .AddHttpMessageHandler<CleanErrorHandler>();

builder.Services.AddHttpClient<IStorageService,
                               AzureBlobStorageService>();
builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SvkServerAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]); 
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();


builder.Services.AddScoped<IGebruikerService, GebruikerService>();
builder.Services.AddScoped<ITransportOpdrachtService, TransportOpdrachtService>();

await builder.Build().RunAsync();
