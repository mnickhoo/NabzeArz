using NabzeArz.Helper;
using NabzeArz.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace NabzeArz.Service
{
    public class FileService : AbstractAPI
    {
        public static bool SaveJsonFile(object json, string fileName)
        {
            try
            {
                string path = Path.Combine(HttpRuntime.AppDomainAppPath, "json/" + fileName);

                if (!File.Exists(path))
                    File.Create(path).Dispose(); //create file 

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(json); //and finalyy write in json file
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error in FileService/" + e.InnerException);
            }
        }

        public async static Task<string> OpenJsonFile(string fileName)
        {
            //make a path 
            string path = Path.Combine(HttpRuntime.AppDomainAppPath, "json/" + fileName);
            if (File.Exists(path)) //check file if is exist
            {
                using (StreamReader sr = new StreamReader((path))) //open file 
                {
                    var responseString = await sr.ReadToEndAsync(); //read to end
                    return responseString; //return string serialize object
                };
            }
            return null;
        }

        public static Stream GetFileStream(string fileName)
        {
            string path = Path.Combine(HttpRuntime.AppDomainAppPath, "Chart/BTC/" + fileName);

            if (File.Exists(path)) //check file if is exist
            {
                StreamReader sr = new StreamReader((path)); //open file 
                return sr.BaseStream;
            }
            return null;
        }

        public async static Task<IEnumerable<CoinMarket>> RequestAndSaveTojson(string url, string jsonFileName)
        {
            HttpResponseMessage response = await GetResponse(url); //response

            if (response.IsSuccessStatusCode) //is successfull 
            {
                string result = await response.Content.ReadAsStringAsync(); //read to end of file    
                var currency = JsonConvert.DeserializeObject<IEnumerable<CoinMarket>>(result); //deserialize to coinMarket Model 
                //var usd = await TgjuAPIService.GetUSDPrice(); //get price
                var usd = CurrencyService.ToToman(Convert.ToDouble(NerkhAPIService.Create.GetCurrencyBySymbol(CurrencyType.USD).current));

                var finall = currency.Select(c => new CoinMarket
                { //add persian name and toman price 
                    available_supply = c.available_supply,
                    id = c.id,
                    last_updated = c.last_updated,
                    market_cap_usd = c.market_cap_usd,
                    max_supply = c.max_supply,
                    name = c.name,
                    percent_change_1h = c.percent_change_1h,
                    percent_change_24h = c.percent_change_24h,
                    percent_change_7d = c.percent_change_7d,
                    persianName = PersianHelper.GetPersianName(c.name),
                    price_btc = c.price_btc,
                    price_usd = c.price_usd,
                    rank = c.rank,
                    stockToman = usd.ToString(),
                    symbol = c.symbol,
                    total_supply = c.total_supply,
                    _24h_volume_usd = c._24h_volume_usd,
                    toman = CurrencyService.calculatorPrice(usd, double.Parse(c.price_usd)).ToString(),
                    persianDate = PersianHelper.GetPersianDatetime(DateTime.Now)
                });

                if (SaveJsonFile(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(finall)), jsonFileName))//save to json file 
                    return finall;

                return null;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("there is problem with remote server that has a internal server error");
            }
            else
            {
                throw new Exception("there problem with connect to remote server");
            }
        }

        public async static Task<USDAPIModel> RequestTgjuAndSaveTojson(string url, string jsonFileName)
        {
            HttpResponseMessage response = await GetResponse(url); //response

            if (response.IsSuccessStatusCode) //is successfull 
            {
                string result = await response.Content.ReadAsStringAsync(); //read to end of file    
                var jsonObj = JObject.Parse(result).Children().Children().First()["transfer_usd"].ToString(); //parse to jobject and finally convert to string
                var USDprice = JsonConvert.DeserializeObject<USDAPIModel>(jsonObj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                if (SaveJsonFile(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(jsonObj)), jsonFileName))//save to json file 
                    return USDprice;

                return null;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("there is problem with remote server that has a internal server error");
            }
            else
            {
                throw new Exception("there problem with connect to remote server");
            }
        }


    }
}