using IdentitySample.Models;
using NabzeArz.Helper;
using NabzeArz.Models;
using NabzeArz.Models.ContextModels;
using NabzeArz.Models.Nerkh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NabzeArz.Service
{
    public class CurrencyService
    {
        public static Dictionary<string, string> GetCurrencyDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "Bitcoin" , "BTC" } ,
                { "Ethereum" , "ETH" } ,
                { "EthereumClassic" , "ETC" } ,
                { "Monero" , "XMR" } ,
                { "Ripple" , "XRP" } ,
                { "ZCash" , "ZEC" } ,
                { "Factom" , "FCT" } ,
                { "Litecoin" , "LTC" } ,
                { "Dogecoin" , "DOGE" } ,
                { "DigitalCash" , "DASH" } ,
                { "Ethereum classic" , "ETC" } ,
                { "Z-cash" , "ZEC" } ,
                { "Digital cash" , "DASH" }
            };
            return dictionary;
        }

        /// <summary>
        /// Get symbol name of cryptocurrency
        /// </summary>
        /// <param name="CryptoName">insert crypto name </param>
        /// <returns></returns>
        public static string GetSymbol(CurrencyName CryptoName)
        {
            var dictionary = GetCurrencyDictionary(); //Dictionay inclue name and symbol
            string symbolName;
            if (!dictionary.TryGetValue(CryptoName.ToString(), out symbolName)) //if hasn't been in dictionary return null
            {
                return null;
            }
            return symbolName;
        }



        public static string GetSymbol(string CryptoName)
        {
            var dictionary = GetCurrencyDictionary(); //Dictionay inclue name and symbol
            string symbolName;
            if (!dictionary.TryGetValue(CryptoName, out symbolName)) //if hasn't been in dictionary return null
            {
                return null;
            }
            return symbolName;
        }

        public static string BuildStringForChannel(IEnumerable<CryptoRate> currencyModel = null, IEnumerable<CurrencyRate> rates = null, IEnumerable<GoldRate> goldRates = null)
        {
            try
            {

                string txt = "💰  قیمت لحظه ای ارز ، سکه و طلا  \n\n تمام قیمت ها به ( تومان ) می باشد \n\n"; //currency
                if (rates != null)
                {
                    var rates_sorted = rates.OrderBy(r => r.order_list).ToList(); //sorted by rates
                    foreach (var rate in rates_sorted) //currency rate
                    {
                        txt += $"‏ {rate.flag} ({rate.symbol}) {rate.fa_name ?? rate.persianName} : {rate.current_Toman} " + "\n";
                    }
                }

                int counter = 0;
                if (currencyModel != null)
                {
                    txt += "\n ارزهای دیجیتال \n‏  -------------------------\n";
                    foreach (var item in currencyModel)
                    {
                        counter++;
                        if (counter <= 3)
                        {
                            txt += $"🔶{item.fa_name} {item.symbol} \n" +
                                   $"🔹{PersianHelper.ToDecimal(item.price_toman) } {Resources.Fa_IR.TMN} \n" +
                                   $"🔹{PersianHelper.ToDecimal(item.price_usd, DigitType.USD)} {Resources.Fa_IR.USD}\n" +
                                   $"{iconIncrease(item.percent_change_1h)} ساعت گذشته : %{item.percent_change_1h}" +
                                   $"\n{iconIncrease(item.percent_change_24h)} روز گذشته : %{item.percent_change_24h}" +
                                   $" \n{iconIncrease(item.percent_change_7d)} هفته گذشته : %{item.percent_change_7d} \n\n";
                        }
                        else
                        {
                            txt += $"🔶{item.fa_name} {item.symbol} \n" +
                                   $"🔹{PersianHelper.ToDecimal(item.price_toman)} {Resources.Fa_IR.TMN} \n" +
                                   $"🔹{PersianHelper.ToDecimal(item.price_usd, DigitType.USD)} {Resources.Fa_IR.USD}\n" +
                                   $"{iconIncrease(item.percent_change_7d)} هفته گذشته : %{item.percent_change_7d} \n\n";
                        }
                    }

                }
                if (goldRates != null)
                {
                    txt += "\n سکه و طلا \n‏  -------------------------\n";
                    foreach (var gold in goldRates)
                    {
                        txt += $"🔸 {gold.fa_name??gold.persianName} : {gold.current_Toman} \n";
                    }
                }

                txt += $"\n 🤖 ربات ما : @nabzearz_bot" + "\n\n";
                txt += "با نبض‌ارز لحظه ای دنبال کنید \n\n";
                txt += $"آخرین بروزرسانی : {PersianHelper.GetPersianDatetime(DateTime.Now)} \n\n";
                txt += $"🏆 @nabzearz";

                return txt;
            }
            catch (Exception e)
            {
                throw new Exception("Exception from CryptoCurrencyService/BuildStringForChannel" + e.Message);
            }
        }
        public static string BuildStringForTwitter(IEnumerable<CryptoRate> currencyModel)
        {
            try
            {
                string txt = $"قیمت دلار : {NerkhAPIService.Create.GetCurrencyBySymbol(CurrencyType.USD)} " + Resources.Fa_IR.TMN + "\n\n";

                int counter = 0;
                foreach (var item in currencyModel)
                {
                    counter++;
                    if (counter <= 3)
                    {
                        txt += $"{item.fa_name} {item.symbol} \n" +
                               $"{PersianHelper.ToDecimal(item.price_toman) } {Resources.Fa_IR.TMN} \n" +
                               $"{PersianHelper.ToDecimal(item.price_usd, DigitType.USD)} {Resources.Fa_IR.USD}\n\n";
                    }
                }
                txt += "#نبض #نبض_ارز #بیت_کوین #ریپل #اتریوم";
                var lenght = txt.Length;
                return txt;
            }

            catch (Exception e)
            {

                throw new Exception("Error from : CurrencyService/BuildStringForTwitter", e.InnerException);
            }
        }

        public static PublishString BuildStringForPublish(IEnumerable<CryptoRate> cryptoRate, IEnumerable<CurrencyRate> rates, IEnumerable<GoldRate> goldRates)
        {
            try
            {
                var result = new PublishString()
                {
                    telegram = BuildStringForChannel(cryptoRate, rates, goldRates),
                    twitter = BuildStringForTwitter(cryptoRate)
                };
                return result;
            }
            catch (Exception e)
            {

                throw new Exception("Error from : CurrencyService/BuildStringForPublish", e.InnerException);
            }
        }

        public static string iconIncrease(string value)
        {
            if (value.Contains("-"))
            {
                return "⬇️";
            }
            return "⬆️";
        }
        public static string hastagh(bool isPrice = true)
        {
            if (isPrice)
            {
                return "#نبض";
            }
            else
            {
                return "#رشد_هفتگی";
            }
        }

        public static double calculatorPrice(double USD, double currency, double fee = 8)
        {
            var pure = (currency * USD); //calculate multiple crypto currency to USD 
            var finall = pure + ((pure / 100) * fee); //pure price + fee
            return finall;
        }

        public static ICollection<CurrencyModel> GetCurrencies()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var currencies = db.Currencies.ToList();
            return currencies;
        }

        public static CurrencyModel GetCurrency(string symbol)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var currency = db.Currencies.Where(c => c.symbol == symbol).FirstOrDefault();
                return currency;
            }
            catch (Exception e)
            {
                throw new Exception("Error from CurrencyService/GetCurrency" + e.Message, e.InnerException);
            }

        }

        public static double ConvertToUSD(double UserRate, CurrencyEnum currency)
        {
            try
            {
                //each currency is a each dollar ? 
                var currencies = CoinMarketAPISerivce.GetCurrencyList();
                var currencyUSD = Convert.ToDouble(currencies.Where(c => c.symbol == currency.ToString()).FirstOrDefault().price_usd);
                //multiple crypto possension to USD 
                var result = UserRate * currencyUSD;
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error from Currencyservice/ConvertToUSD" + e.Message, e.InnerException);
            }
        }

        public static CurrencyEnum EnumParse(string symbol)
        {
            CurrencyEnum result;
            Enum.TryParse<CurrencyEnum>(symbol, out result);
            return result;
        }

        public static double ConvertToToman(double USDRate, double USDPrice)
        {
            var result = USDRate * USDPrice;
            return result;
        }

        public static double ToToman(double price)
        {
            return price / 10;
        }
        public static string ToToman(string price)
        {
            double priceDouble = Convert.ToDouble(price);
            return (priceDouble / 10).ToString();
        }
    }
}