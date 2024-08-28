// See https://aka.ms/new-console-template for more information


using Tidal_Net.Authentication;
using Tidal_Net.Data.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {


        await Auth.GetAccessTokenAsync("9zOmg1PSCiUr8qLJ", "hmDVQVjUkDHWS1s0q1C9nGrj3Ohm2gWrxniVoR8CfaQ=");

        var albums = await TidalAlbumServices.GetManyAlbum(new List<string>(){"251380836", "214357541"});


        foreach (var album in albums.Data)
        {
            Console.WriteLine($"Album: {album.Title}");

        }

        var response = await TidalAlbumServices.GetOne("251380836");

        Console.WriteLine(response.Data?.Title);


    }
}