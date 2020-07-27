using CointPrice.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Security.Policy;

namespace CointPrice.Service
{
    public class ApiService : AbstractAPI
    {
        #region depricated methode


        //private static Uri GetApiUrl(CurrencyName CryptoName = CurrencyName.FindAll)
        //{
        //    string url = "https://digiarz.com/webservice/api/"; //url address by default 
        //    switch (CryptoName)
        //    {
        //        case CurrencyName.Bitcoin:
        //            url += "?c=" + Symbol.BTC.ToString();
        //            break;
        //        case CurrencyName.Ethereum:
        //            url += "?c=" + Symbol.ETH.ToString();
        //            break;
        //        case CurrencyName.EthereumClassic:
        //            url += "?c=" + Symbol.ETC.ToString();
        //            break;
        //        case CurrencyName.Monero:
        //            url += "?c=" + Symbol.XMR.ToString();
        //            break;
        //        case CurrencyName.Ripple:
        //            url += "?c=" + Symbol.XRP.ToString();
        //            break;
        //        case CurrencyName.ZCash:
        //            url += "?c=" + Symbol.ZEC.ToString();
        //            break;
        //        case CurrencyName.Factom:
        //            url += "?c=" + Symbol.FCT.ToString();
        //            break;
        //        case CurrencyName.Litecoin:
        //            url += "?c=" + Symbol.LTC.ToString();
        //            break;
        //        case CurrencyName.Dogecoin:
        //            url += "?c=" + Symbol.DOGE.ToString();
        //            break;
        //        case CurrencyName.DigitalCash:
        //            url += "?c=" + Symbol.DASH.ToString();
        //            break;
        //    }
        //    return new Uri(url);
        //}

        //private static async Task<HttpResponseMessage> GetResponse()
        //{
        //    HttpResponseMessage response = await Client().GetAsync(GetApiUrl());
        //    return response;  //request to API
        //}
        //private static async Task<HttpResponseMessage> GetResponse(CurrencyName cryptoName)
        //{
        //    HttpResponseMessage response = await Client().GetAsync(GetApiUrl(cryptoName)); //make request and dispose it
        //    return response;
        //}

        //public async static Task<CurrencyModel> GetCurrencyByName(CurrencyName cryptoName)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await GetResponse(cryptoName);//get Response message
        //        if (response.IsSuccessStatusCode) //is successfull 
        //        {
        //            string responseString = await response.Content.ReadAsStringAsync(); //read to end and syting
        //            var result = JObject.Parse(responseString)[CurrencyService.GetSymbol(cryptoName)]; //select crypto name record 
        //            var serializeObj = JsonConvert.SerializeObject(result); //serialize json to string
        //            var CryptoModel = JsonConvert.DeserializeObject<CurrencyModel>(serializeObj);//deserialze to crypto Model
        //            return CryptoModel;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception("Error from ApiService/GetCurrencyByName" + e.Message);
        //    }
        //}//get crypto by name


        //private static async Task<List<CurrencyModel>> GetCurrencies()
        //{
        //    using (HttpResponseMessage response = await GetResponse())
        //    {
        //        if (response.IsSuccessStatusCode) //is successfull 
        //        {
        //            string result = await response.Content.ReadAsStringAsync(); //read to end and syting
        //            var jsonObj = JObject.Parse(result).Children().Children().ToList(); //parse to jobject 
        //            jsonObj.RemoveAt(0); //remove first record was danger for deserialize
        //            var serialize = JsonConvert.SerializeObject(jsonObj);
        //            var CryptoList = JsonConvert.DeserializeObject<List<CurrencyModel>>(serialize, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        //            return CryptoList;
        //        }
        //        return null;
        //    }
        //}

        //public static async Task<List<CurrencyModel>> GetCurrencyList()
        //{
        //    try
        //    {
        //       return await GetCurrencies();
        //    }
        //    catch (WebException e)
        //    {
        //        if (e.Status == WebExceptionStatus.Timeout)
        //        {
        //            return await GetCurrencies();
        //        }
        //        throw new Exception("Error from API Service/GetCryptoValueList" + e.Message);
        //    }
        //}//GetCurrencyList 

        //public static async Task<CryptoCurrencey> GetPrice()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await GetResponse();
        //        if (response.IsSuccessStatusCode) //is successfull 
        //        {
        //            string result = await response.Content.ReadAsStringAsync(); //read to end and syting
        //            var jsonObj = JObject.Parse(result); //parse to jobject 
        //            var jsonObject = JsonConvert.SerializeObject(jsonObj); //serialize 
        //            var Crypto = JsonConvert.DeserializeObject<CryptoCurrencey>(jsonObject);
        //            return Crypto;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception("Error in API Service" + e.Message);
        //    }
        //}//get crypto 
        #endregion

    }
}