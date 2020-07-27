using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NabzeArz.Models
{
    public abstract class AbstractAPI
    {
        protected static HttpClient Client() //make http client for request
        {
            return new HttpClient();
        }
        protected static async Task<HttpResponseMessage> GetResponse(string url) //get response 
        {
            HttpResponseMessage response = await Client().GetAsync(GetApiUrl(url));
            return response;  //request to API
        }
        protected static Uri GetApiUrl(string url)
        {
            return new Uri(url);
        }

        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff"); //generate time stamp
        }
        /// <summary>
        /// get specific timeStamp
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected static string GetTimeStamp(DateTime date)
        {
            return date.ToString("yyyyMMddHHmmssffff"); //generate time stamp
        }

    }
}