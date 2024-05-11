using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidal_Net.Data
{
    public class Config
    {

        /// <summary>
        /// The base url string to the request endpoint
        /// </summary>

        public static readonly string BaseUrl = "https://openapi.tidal.com";

        /// <summary>
        /// This is the market of the request. The default value is US
        /// </summary>

        public static string Market = "US";
    }
}
