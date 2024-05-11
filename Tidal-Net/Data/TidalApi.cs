using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tidal_Net.Data
{
    internal class TidalApi
    {
        public static async Task<string> Request(string endPoint, string accessToken)
        {
            try
            {
                var apiUrl = $"{Config.BaseUrl}{endPoint}?countryCode={Config.Market}";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                webRequest.Method = "GET";
                webRequest.Accept = "application/vnd.tidal.v1+json";
                webRequest.Headers.Add("Authorization: Bearer " + accessToken);
                webRequest.ContentType = "application/vnd.tidal.v1+json";

                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (Stream respStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(respStream, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                           
                           
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
        }
        public static async Task<string> RequestMany(string endPoint, string accessToken)
        {
            try
            {
                var apiUrl = $"{Config.BaseUrl}{endPoint}&countryCode={Config.Market}";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                webRequest.Method = "GET";
                webRequest.Accept = "application/vnd.tidal.v1+json";
                webRequest.Headers.Add("Authorization: Bearer " + accessToken);
                webRequest.ContentType = "application/vnd.tidal.v1+json";

                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (Stream respStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(respStream, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();


                        }
                    }
                }
            }
            catch (Exception e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
        }
    }
}
