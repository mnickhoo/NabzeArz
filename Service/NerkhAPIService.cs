using NabzeArz.Models.Nerkh;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using NabzeArz.Models;
using System.Reflection;
using IdentitySample.Models;

namespace NabzeArz.Service
{
    public class NerkhAPIService
    {
        private static NerkhAPIService _instance;
        protected string _apiKey;

        private NerkhAPIService()
        {
            _apiKey = "e41a7ba23a567320db0fad8a6d4b34e0";
        }

        public static NerkhAPIService Create
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NerkhAPIService();
                }
                return _instance;
            }
        }

        protected string apiKey { get { return _apiKey; } }

        //make uri 
        private Uri GetUrl(NerkhType type)
        {
            string url = "";
            switch (type)
            {
                case NerkhType.currency:
                    url += $"http://nerkh-api.ir/api/{this.apiKey}/currency/?showchange=true";
                    return new Uri(url);
                case NerkhType.gold:
                    url += $"http://nerkh-api.ir/api/{this.apiKey}/gold/?showchange=true";
                    return new Uri(url);
                default:
                    return new Uri("");
            }
        }
        //get all currency 
        private async Task<IEnumerable<NerkhObject>> GetCurrencyRequest()
        {
            //make a request to nerkhAPI 
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler, false)) //set handler for client
                {
                    var responseString = await client.GetAsync(GetUrl(NerkhType.currency)).Result.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NerkhCurrencyModel>(responseString); //deserialize object
                    PropertyInfo[] properties = result.data.Prices.GetType().GetProperties(); //get properties of NerkhModel
                    var final = properties.Select(p => new NerkhObject
                    {
                        symbol = p.Name,
                        rates = p.GetValue(result.data.Prices, null)
                    }); //get value
                    return final;
                }
            }
        }
        private async Task<IEnumerable<NerkhObject>> GetGoldRequest()
        {
            //make a request to nerkhAPI 
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler, false)) //set handler for client
                {
                    var responseString = await client.GetAsync(GetUrl(NerkhType.gold)).Result.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NerkhGoldModel>(responseString); //deserialize object
                    PropertyInfo[] properties = result.data.Prices.GetType().GetProperties(); //get properties of NerkhModel
                    var final = properties.Select(p => new NerkhObject
                    {
                        symbol = p.Name,
                        rates = p.GetValue(result.data.Prices, null)
                    }); //get value
                    return final;
                }
            }
        }

        public IEnumerable<CurrencyRate> GetCurrency(bool showInChannel = false)
        {
            List<CurrencyRate> currenceis = new List<CurrencyRate>();

            using (ApplicationDbContext db = new ApplicationDbContext()) //make connection to database
            {
                if (showInChannel)
                {
                    currenceis = db.CurrencyRates.Where(c => c.showIntoChannel).ToList(); //get currencyRates from databasse
                }
                else
                {
                    currenceis = db.CurrencyRates.ToList(); //get currencyRates from databasse
                }
                return currenceis;
            }
        }

        public IEnumerable<GoldRate> GetGold()
        {
            using (ApplicationDbContext db = new ApplicationDbContext()) //make connection to database
            {
                var golds = db.GoldRates.ToList(); //get GoldRates from databasse
                return golds;
            }
        }

        public static bool ChangeOrderList(string[] items)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var currencies = db.CurrencyRates.ToList(); //get currencies 
                for (int i = 0; i < items.Length; i++)
                {
                    currencies.Where(c => c.symbol == items[i]).FirstOrDefault().order_list = i; 
                }
                db.SaveChanges();
            }
            return true;
        }
        /// <summary>
        /// request to remote and save into database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GoldRate>> RequestGoldAndSaveToDb()
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())//get_db_context 
                {
                    var gold = await GetGoldRequest(); //get gold rates  from { NerkhAPi }
                    var goldRates = CastCurrencyToGoldRates(gold).Reverse().ToList(); //cast to currencyRate

                    if (!db.GoldRates.Any()) //check in databse record has been inserted
                    {
                        goldRates.ForEach(c => db.GoldRates.Add(c));  //add record into database 
                    }
                    else
                    {
                        foreach (var item in goldRates)
                        {
                            var singGold = db.GoldRates.Where(c => c.symbol == item.symbol).FirstOrDefault(); //get from db
                            if (singGold != null)
                            {
                                singGold.max = item.max; //update max
                                singGold.min = item.min; //update min
                                singGold.current = item.current; //update current
                                singGold.changePercent = item.changePercent; //update change percent
                                singGold.changePrice = item.changePrice; //update change price
                            }
                            else
                            {
                                db.GoldRates.Add(item); //if no record has been yet then add it
                            }
                        }
                        //update price in table
                    }
                    db.SaveChanges(); //save into database
                    return goldRates; //return currency Rate
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// request to remote and save into database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurrencyRate>> RequestCurrencyAndSaveToDb()
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())//get_db_context 
                {
                    var currencies = await GetCurrencyRequest(); //get currencies  from { NerkhAPi }
                    var currenceyRates = CastCurrencyToCurrencyRates(currencies).Reverse().ToList(); //cast to currencyRate

                    if (!db.CurrencyRates.Any()) //check in databse record has been inserted
                    {
                        currenceyRates.ForEach(c => db.CurrencyRates.Add(c));  //add record into database 
                    }
                    else
                    {
                        foreach (var item in currenceyRates)
                        {
                            var currency = db.CurrencyRates.Where(c => c.symbol == item.symbol).FirstOrDefault(); //get from db
                            if (currency != null)
                            {
                                currency.max = item.max; //update max
                                currency.min = item.min; //update min
                                currency.current = item.current; //update current
                                currency.changePercent = item.changePercent; //update change percent
                                currency.changePrice = item.changePrice; //update change price
                            }
                            else
                            {
                                db.CurrencyRates.Add(item); //if no record has been yet then add it
                            }
                        }
                        //update price in table
                    }
                    db.SaveChanges(); //save into database
                    return currenceyRates; //return currency Rate
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private IEnumerable<CurrencyRate> CastCurrencyToCurrencyRates(IEnumerable<NerkhObject> nerkhObjects)
        {
            var currenceyRates = nerkhObjects.Select(c => new CurrencyRate
            {
                symbol = c.symbol,
                min = ((currencyModel)c.rates).min,
                current = ((currencyModel)c.rates).current,
                max = ((currencyModel)c.rates).max,
                changePercent = ((currencyModel)c.rates).changePercent,
                changePrice = ((currencyModel)c.rates).changePrice
            });
            return currenceyRates;
        }

        private IEnumerable<GoldRate> CastCurrencyToGoldRates(IEnumerable<NerkhObject> nerkhObjects)
        {
            var currenceyRates = nerkhObjects.Select(c => new GoldRate
            {
                symbol = c.symbol,
                min = ((currencyModel)c.rates).min,
                current = ((currencyModel)c.rates).current,
                max = ((currencyModel)c.rates).max,
                lastUpdate = DateTime.Now,
                changePercent = ((currencyModel)c.rates).changePercent,
                changePrice = ((currencyModel)c.rates).changePrice
            });
            return currenceyRates;
        }

        /// <summary>
        /// Get currency by symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>CurrencyRate</returns>
        public CurrencyRate GetCurrencyBySymbol(CurrencyType symbol)
        {
            var currencies = GetCurrency(); //get currency
            var result = currencies.Where(c => c.symbol == symbol.ToString()).FirstOrDefault(); //filter currencies
            return result;
        }
   
        /// <summary>
        /// Get Gold by symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>GoldRate</returns>
        public GoldRate GetGoldBySymbol(GoldType symbol)
        {
            var golds = GetGold(); //get gold rates
            var result = golds.Where(c => c.symbol == symbol.ToString()).FirstOrDefault(); //filter by symbols
            return result;
        }
    }
}