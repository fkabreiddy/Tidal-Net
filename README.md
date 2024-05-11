Hello

So basically i tried to do a Tidal Web Api library for .Net

# Project Structure:

Config.cs: Here you can find the base uri used to make the requests to the endpoint.

TidalApi.cs: Here you can find the uri builder to make the request, Providing 2 options, one for many objects (ex "get many albums) and other one for single objects (ex "get x item").
The difference between these two is the url builder change some characters to make the uri functional.

Authentication and Auth.cs file: On this file you can find an asynchronous function to generate a Access Token from your ClientId And Cient Secred provided by Tidal. You can call this method
without declaring a class instance and it returns a string.

Data: in this folder you can find the models on the Models folder and the Services on the Services folder. 
In Models you can find the TidalAlbum model used to extract the information from the tidal api. Some propeties like tags are not available by now because some albums wont come with that information
and can lead to futher exceptions. Otherwise, Services folder is for microservices. 


# Use

```c#
 var token = await Auth.GetAccessTokenAsync("your client id", "your client secret");
 var album = await AlbumService.GetAlbum("95094922", token);
 var albums = await AlbumService.GetManyALbums(new List<string>() { "95094922", "220809479" }, token);

      
 //always check nullability just for fun
 if(album is not null)
 {
     Console.WriteLine(album.Title  + " by: "+ album.Artists[0].Name);

 }

 if(albums is not null || albums.Count() >= 1)
 {
     foreach (var a in albums)
     {
         Console.WriteLine(a.Title + " by: " + a.Artists[0].Name);


     }
 }    
        
```

# Tips and Tricks

- Since im using dynamic objects to extract the json data it may have some issues with the tidal api that sometimes return json values as null or json props wont return at all. So i tried so just keep with the basics (name, id, artists, cover etc) to minimize this error gap.
- Also some albums with especial characters like ex: Motomami + arent able to be requested cuz the json deserializer is having some issues with this kind of chars


# so whats next?

Yeah, i might need help building the other services and making this a nuget package.
