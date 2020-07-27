using IdentitySample.Models;
using NabzeArz.Helper;
using NabzeArz.Models;
using NabzeArz.Models.ContextModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace NabzeArz.Service
{
    public class CoinMarketAPISerivce : AbstractAPI
    {
        #region get by name
        //public async static Task<CurrencyModel> GetCurrencyByName(CurrencyName cryptoName)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await GetResponse(cryptoName); //get Response message
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
        #endregion
        #region old_comment
        //public static async Task<List<CoinMarketModel>> GetCurrencyList()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await GetResponse(url: "https://api.coinmarketcap.com/v1/ticker/"); //get response message

        //        if (response.IsSuccessStatusCode) //is successfull 
        //        {
        //            string result = await response.Content.ReadAsStringAsync(); //read to end of file                   
        //            var currencyList = JsonConvert.DeserializeObject<List<CoinMarketModel>>(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        //            return currencyList;
        //        }
        //        return null; //if not successfull return null
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception("Error from API Service/GetCryptoValueList" + e.Message);
        //    }
        //}
        #endregion

        public static List<CryptoRate> GetCurrencyList()
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var cryptoRates = db.CryptoRates.ToList();
                return cryptoRates;
                //var responseString = await FileService.OpenJsonFile(AppSetting.coinmarket_file); //open json file 
                //var currencyList = JsonConvert.DeserializeObject<List<CoinMarket>>(responseString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                //return currencyList;
            }
            catch (Exception e)
            {

                throw new Exception("Error from API CoinMarketAPIService/GetCurrencyList" + e.Message);
            }
        }

        public static IEnumerable<CryptoRate> GetTopCurrency(int number = 15)
        {
            try
            {
                var CurrencyList = GetCurrencyList(); //get all crypto currency

                if (CurrencyList != null)
                    return CurrencyList.Take(number);

                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error from : CoinMarketAPIService/GetTopCurrency", e.InnerException);
            }
        }

        public async static Task<IEnumerable<CoinMarket>> RequestAndSaveToFile()
        {
            var errorList = new List<Exception>(); //error list add exception error in each attemp
            int maxTimeOut = 3; //attemp max number

            do
            {
                --maxTimeOut;
                try
                {
                    return await FileService.RequestAndSaveTojson(AppSetting.coinmarket_api, AppSetting.coinmarket_file);
                }
                catch (Exception e)
                {
                    errorList.Add(e);
                }

            } while (maxTimeOut > 0);

            return null;
        }

        public async static Task<IEnumerable<CryptoRate>> RequestAndSaveToDB()
        {
            var errorList = new List<Exception>(); //error list add exception error in each attemp
            int maxTimeOut = 3; //attemp max number

            do
            {
                --maxTimeOut;
                try
                {
                    return await RequestAndSaveToDB(AppSetting.coinmarket_api);
                }
                catch (Exception e)
                {
                    errorList.Add(e);
                }

            } while (maxTimeOut > 0);

            return null;
        }




        public async static Task<IEnumerable<CryptoRate>> RequestAndSaveToDB(string url)
        {
            HttpResponseMessage response = await GetResponse(url); //get response

            if (response.IsSuccessStatusCode) //is successfull 
            {
                string result = await response.Content.ReadAsStringAsync(); //read to end of response    
                var currency = JsonConvert.DeserializeObject<IEnumerable<CoinMarket>>(result); //deserialize to coinMarket Model 
                var USD_price = CurrencyService.ToToman(Convert.ToDouble(NerkhAPIService.Create.GetCurrencyBySymbol(CurrencyType.USDHandyHavale).current));

                var finall = currency.Select(c => new CoinMarket
                { //add persian name and toman price 
                    available_supply = c.available_supply == null ? "" : c.available_supply,
                    id = c.id == null ? "" : c.id,
                    last_updated = c.last_updated == null ? "" : c.last_updated,
                    market_cap_usd = c.market_cap_usd == null ? "" : c.market_cap_usd,
                    max_supply = c.max_supply == null ? "" : c.max_supply,
                    name = c.name == null ? "" : c.name,
                    percent_change_1h = c.percent_change_1h == null ? "" : c.percent_change_1h,
                    percent_change_24h = c.percent_change_24h == null ? "" : c.percent_change_24h,
                    percent_change_7d = c.percent_change_7d == null ? "" : c.percent_change_7d,
                    persianName = PersianHelper.GetPersianName(c.name),
                    price_btc = c.price_btc == null ? "" : c.price_btc,
                    price_usd = c.price_usd == null ? "" : c.price_usd,
                    rank = c.rank == null ? "" : c.rank,
                    stockToman = USD_price.ToString() == null ? "" : USD_price.ToString(),
                    symbol = c.symbol == null ? "" : c.symbol,
                    total_supply = c.total_supply == null ? "" : c.total_supply,
                    _24h_volume_usd = c._24h_volume_usd == null ? "" : c._24h_volume_usd,
                    toman = CurrencyService.calculatorPrice(USD_price, double.Parse(c.price_usd)).ToString(),
                    persianDate = PersianHelper.GetPersianDatetime(DateTime.Now)
                });
                return saveToDatabase(finall);
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



        private static IEnumerable<CryptoRate> saveToDatabase(IEnumerable<CoinMarket> currencies)
        {
            try
            {
                var cryptoRates = currencies.Select(c => new CryptoRate
                {
                    en_name = c.name,
                    fa_name = c.persianName,
                    market_cap_usd = c.market_cap_usd,
                    percent_change_1h = c.percent_change_1h,
                    available_supply = c.available_supply,
                    max_supply = c.max_supply,
                    percent_change_24h = c.percent_change_24h,
                    percent_change_7d = c.percent_change_7d,
                    _24h_volume_usd = c._24h_volume_usd,
                    price_btc = c.price_btc,
                    rank = c.rank,
                    symbol = c.symbol,
                    total_supply = c.total_supply,
                    persianDate = c.persianDate,
                    price_toman = c.toman,
                    price_usd = c.price_usd
                }).ToList();
                ApplicationDbContext db = new ApplicationDbContext();
                //cast to cryptoRate 

                //add to database
                if (!db.CryptoRates.Any())
                {
                    cryptoRates.ForEach(c => db.CryptoRates.Add(c));  //add record into database 
                }
                else
                {
                    foreach (var item in cryptoRates) //cryptorates
                    {
                        var cryptoRate = db.CryptoRates.Where(c => c.symbol == item.symbol).FirstOrDefault(); //get row from db
                        if (cryptoRate != null)
                        {
                            cryptoRate.market_cap_usd = item.market_cap_usd;
                            cryptoRate.rank = item.rank;
                            cryptoRate.percent_change_1h = item.percent_change_1h;
                            cryptoRate.percent_change_7d = item.percent_change_7d;
                            cryptoRate._24h_volume_usd = item._24h_volume_usd;
                            cryptoRate.max_supply = item.max_supply;
                            cryptoRate.percent_change_24h = item.percent_change_24h;
                            cryptoRate.total_supply = item.total_supply;
                            cryptoRate.available_supply = item.available_supply;
                            cryptoRate.persianDate = item.persianDate;
                            cryptoRate.price_btc = item.price_btc; //update BTC
                            cryptoRate.price_usd = item.price_usd; //update USD
                            cryptoRate.price_toman = item.price_toman; //update toman
                            cryptoRate.price_rial = item.price_rial; //update rial
                        }
                        else
                        {
                            db.CryptoRates.Add(item); //add to database if has not added
                        }

                    }
                }
                db.SaveChanges(); //save to database 
                return cryptoRates;
            }
            catch (Exception e)
            {
                throw new Exception("Error from CoinMarketAPIService/saveToDatabase" + e.InnerException);
            }
        }
    }
}