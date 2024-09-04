using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using TidalApi.Web.Core.Authentication;
using TidalApi.Web.Core.Data;
using TidalApi.Web.Core.Data.Interfaces;
using TidalApi.Web.Core.Data.Services;

namespace TidalApi.Web.Core.IoC;

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