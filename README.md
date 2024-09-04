# Use

```c#

//on top of Program.cs before your custom services

using TidalApi.Web.IoC; //import this directory

builder.Services.AddTidalWebApiServices();



//how to use it



//first create a custom service 

using TidalApi.Web.Interfaces;
using TidalApi.Web.Models;

public class MyTidalWebService()
{
    private readonly ITidalTokenBuilder _tokenBuilder;

    private readonly ITidalClient _tidalClient;

    public MyTidalWebService(ITidalClient client, ITidalTokenBuilder tidalTokenBuilder)
    {
        this._tokenBuilder = tidalTokenBuilder;
        this._tidalClient = client;
    }
    
    //get the access token
    public async Task<string?> GetToken(string myClientId, string myClientSecret)
    {
        var mytoken = await _tokenBuilder.Build(myClientId, myClientSecret);
        
        return mytoken ?? null;
    }
    
    //consume one service
    
   public async Task<TidalAlbum> GetOneAlbum(string albumId, string myToken, string market) //market is optional. Default value is US
   {
       var album = await _tidalClient.Albums.GetOne(albumId, token, market);
       
       return album;
   }
    
} 


//for blazor you can inject the services on components

//on _Imports.razor
@using TidalApi.Web.Interfaces
@inject ITidalClient tidalClient
@inject ITidalTokenBuilder tokenBuilder  

```

