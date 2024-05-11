using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tidal_Net.Data.Models;

namespace Tidal_Net.Data.Services
{
    public class AlbumService
    {

        /// <summary>
        /// To search a single artist
        /// </summary>

        private static async Task<TidalAlbum> ExtractAlbumInfo(dynamic json)
        {
            try
            {
                TidalAlbum album = new TidalAlbum();

                album.Id = json.resource.id;
                album.BarcodeId = json.resource.barcodeId;
                album.Title = json.resource.title;

                // Asignar artistas
                List<TidalArtist> artists = new List<TidalArtist>();
                foreach (var artist in json.resource.artists)
                {
                    TidalArtist tidalArtist = new TidalArtist();
                    tidalArtist.Id = artist.id;
                    tidalArtist.Name = artist.name;

                    // Asignar imágenes del artista
                    List<Picture> pictures = new List<Picture>();
                    foreach (var picture in artist.picture)
                    {
                        Picture tidalPicture = new Picture();
                        tidalPicture.Url = picture.url;
                        tidalPicture.Width = picture.width;
                        tidalPicture.Height = picture.height;
                        pictures.Add(tidalPicture);
                    }
                    tidalArtist.Picture = pictures.ToList();

                    tidalArtist.Main = artist.main;
                    artists.Add(tidalArtist);
                }
                album.Artists = artists.ToList();

                album.Duration = json.resource.duration;
                album.ReleaseDate = json.resource.releaseDate;

                // Asignar portadas de imagen
                List<ImageCover> imageCovers = new List<ImageCover>();
                foreach (var imageCover in json.resource.imageCover)
                {
                    ImageCover tidalImageCover = new ImageCover();
                    tidalImageCover.Url = imageCover.url;
                    tidalImageCover.Width = imageCover.width;
                    tidalImageCover.Height = imageCover.height;
                    imageCovers.Add(tidalImageCover);
                }
                album.ImageCovers = imageCovers.ToList();

                // Asignar portadas de video
                List<ImageCover> videoCovers = new List<ImageCover>();
                foreach (var videoCover in json.resource.videoCover)
                {
                    ImageCover tidalVideoCover = new ImageCover();
                    tidalVideoCover.Url = videoCover.url;
                    tidalVideoCover.Width = videoCover.width;
                    tidalVideoCover.Height = videoCover.height;
                    videoCovers.Add(tidalVideoCover);
                }
                album.VideoCovers = videoCovers.ToList();

                album.NumberOfVolumes = json.resource.numberOfVolumes;
                album.NumberOfTracks = json.resource.numberOfTracks;
                album.NumberOfVideos = json.resource.numberOfVideos;
                album.Type = json.resource.type;
                album.Copyright = json.resource.copyright;


                album.TidalUrl = json.resource.tidalUrl;

                return album;

            }
            catch
            {
                return new();
            }
        }
        public  static async Task<TidalAlbum> GetAlbum(string albumid, string accessToken)
        {
            try
            {
                var result = await TidalApi.Request(endPoint: $"/albums/{albumid}", accessToken);
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);

                // Asignar valores del objeto dinámico al objeto TidalAlbum
                var album = await ExtractAlbumInfo(json);

                if (album is null)
                    return new();



                return album;
            }
            catch(Exception e)
            {
                return new();
            }
        }

        public static async Task<List<TidalAlbum>> GetManyALbums(List<string> albumsid, string accessToken)
        {
            try
            {
                List<TidalAlbum> albumes = new();
                if (albumsid.Count() <= 0)
                    return new();

                string idsQueryParam = "";
                if (albumsid.Count() == 1)
                {
                    idsQueryParam = $"ids={albumsid[0]}";
                }
                else
                {
                    var firstId = $"ids={albumsid[0]}";
                    idsQueryParam = $"ids={albumsid[0]}";
                    foreach (var id in albumsid.Skip(1))
                    {

                        idsQueryParam += $"&ids={id}";


                    }
                  


                }
                var result = await TidalApi.RequestMany(endPoint: $"/albums/byIds?{idsQueryParam}", accessToken);
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);

                if (json is null)
                    return new();
                foreach(var obj in json.data)
                {
                    var a = await ExtractAlbumInfo(obj);

                    if (a is not null)
                        albumes.Add(a);
                }

                return albumes;
            }
            catch
            {
                return new List<TidalAlbum>();  
            }
        }
    }
}
