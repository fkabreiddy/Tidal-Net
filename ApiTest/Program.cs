// See https://aka.ms/new-console-template for more information


using Tidal_Net.Authentication;
using Tidal_Net.Data;
using Tidal_Net.Data.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {


        ///we get build the Token and the api requester
        var token = await TidalTokenBuilder.Build("9zOmg1PSCiUr8qLJ", "hmDVQVjUkDHWS1s0q1C9nGrj3Ohm2gWrxniVoR8CfaQ=");
        
        if(token is null)
            return;
        
        var requester = new TidalRequester(token);
        
        //get one album + artist(s). Needed: a string with the album Id
        var albumResponse = await new TidalAlbumServices(requester).GetOne("74892041");
        
        if(albumResponse.Data is not null)
        {
            var album = albumResponse.Data;
            Console.WriteLine($"Album: {album.Title} by {album.Artists.First().Name}");

        }
        
        //get many albums + artists. Needed: a list of string with the albums ids
        var albumsResponse = await new TidalAlbumServices(requester).GetMany(new(){"74892041", "74778333"});

        if (albumsResponse.Data is not null)
        {
            var albums = albumsResponse.Data ?? new();
            foreach (var a in albums )
            {
                Console.WriteLine($"Album: {a.Title} by {a.Artists.First().Name}");

            }

            
        }
        
        //get the tracks of an album
        var tracks = await new TidalAlbumServices(requester).GetTracks("258373409");

        if (tracks.Data is not null && tracks.Data.Any())
        {
            int counter = 1;
            foreach (var track in tracks.Data)
            {
                
                Console.WriteLine($"Track {counter}: {track.Title}  {(track.Explicit ? " (explicit)" : "")}");
                counter++;
            }
        }



    }
}