using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Tidal_Net.Authentication;
using Tidal_Net.Data;
using Tidal_Net.Data.Interfaces;
using Tidal_Net.Data.Services;

namespace Tidal_Net.IoC;

public static class TidalServicesCollectionExtensions
{
    public static IServiceCollection AddTidalWebApiServices(this IServiceCollection services)
    {
        
        // HttpClient for the TokenBuilder Only
        services.AddHttpClient("TidalAuthClient", client =>
        {
            client.BaseAddress = new Uri("https://auth.tidal.com/v1/oauth2/");
        });
        
        // Registrar HttpClient con IHttpClientFactory
        services.AddHttpClient("TidalApiClient", client =>
        {
            client.BaseAddress = new Uri("https://openapi.tidal.com/v2/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.tidal.v1+json"));
        });

        // Registrar servicios específicos que utilicen el HttpClient
        services.AddScoped<ITidalTokenBuilder,TidalTokenBuilder>();
        services.AddScoped<ITidalRequester, TidalRequester>();
        services.AddScoped<ITidalAlbumServices,TidalAlbumServices>();
        services.AddScoped<ITidalArtistServices,TidalArtistServices>();
        services.AddScoped<ITidalTrackServices,TidalTrackServices>();
        services.AddScoped<ITidalClient,TidalClient>();


        return services;
    }
    
}