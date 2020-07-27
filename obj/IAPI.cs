using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;

namespace CointPrice.Models
{
    public interface IAPI
    {
        HttpClient Client();
        //Url GetApiUrl();
        //Task<HttpResponseMessage> GetResponse(); 

    }
}