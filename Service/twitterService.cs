using NabzeArz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TweetSharp;

namespace NabzeArz.Service
{
    public class twitterService
    {
        private static TwitterService GetTwiteerService()
        {
            //set consumer_key and consumer_Secret for application Setting
            TwitterService service = new TwitterService("S7HcNXsctZuS0Uk6l8CHoPqb3", "r9mHBv5g6o3ud8z2oCrcazf08dhb0O4xoMdqvzDyn7VVHmE4Yy");
            //set authenticate_token and authenticate_secret
            service.AuthenticateWith("953738332017655808-tyCWqja1J0jOC11ImC3UZOaWt9AQ5Fw", "bsnZUbGfV3FDnexWAHbOC8nWb0y8CxjYybaUpxTeUfAES");
            return service; 
        }
        public static TwitterStatus sendMesage(string message)
        {
            var service = GetTwiteerService();
            return service.SendTweet(new SendTweetOptions { Status =  message  });
        }
    }
}