// See https://aka.ms/new-console-template for more information


using Tidal_Net.Authentication;
using Tidal_Net.Data;

var token = await TidalTokenBuilder.Build("clientId", "client_secret");
var client = new TidalClient(token);

var tracks = await client.Album.GetTracks("74778333");
var album = await client.Album.GetOne("74778333");
var artist = await client.Artist.GetOne("5416271");

Console.WriteLine(artist.Name);
Console.WriteLine($"Album: {album.Title}");
foreach (var track in tracks)
{
    Console.WriteLine($"Track: {track.Title}");
}

