# Use

```c#


using Tidal_Net.Authentication;
using Tidal_Net.Data;

var token = await TidalTokenBuilder.Build("clientId", "client_secret");
var client = new TidalClient(token);

var album = await client.Album.GetOne("74778333");
var artist = await client.Artist.GetOne("5416271");

Console.WriteLine(artist.Name);
Console.WriteLine($"Album: {album.Title}");
 

```

