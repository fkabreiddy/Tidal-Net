# Use

```c#
var token = await TidalTokenBuilder.Build("your_clientId", "your client secret");
        
 //check the result

 if (token is  null)
     return false;

 //build the requester
 var requester = new TidalRequester(token);
 
 //make a request

 var response = await new TidalAlbumServices(requester).GetOne("74892041");

 var album = response.Data ?? new();
 Console.WriteLine($"Album: {album.Title} by {album.Artists.First().Name}");
 //output "Melodrama by Lorde"

```

