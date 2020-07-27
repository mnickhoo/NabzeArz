using IdentitySample.Models;
using NabzeArz.Helper;
using NabzeArz.Models.ContextModels;
using NabzeArz.Models.Nerkh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NabzeArz.Service
{
    public class SearchService
    {
        public static GoldRate searchForGold(string query)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var result = db.GoldRates.Where(g => g.symbol.Contains(query) ||
                                            g.fa_name.Contains(query) ||
                                            g.en_name.Contains(query)).FirstOrDefault();
            return result;
        }

        public static CurrencyRate searchForCurrency(string query)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var result = db.CurrencyRates.Where(g => g.symbol.Contains(query) ||
                                                g.fa_name.Contains(query) ||
                                                g.en_name.Contains(query)).FirstOrDefault();
            return result;
        }


        public static CryptoRate searchForCryptoCurrency(string query)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var result = db.CryptoRates.Where(g => g.symbol.Contains(query) ||
                                            g.fa_name.Contains(query) ||
                                            g.en_name.Contains(query)).FirstOrDefault();
            return result;
        }

        public static search search(string query)
        {
            var gold = searchForGold(query);
            if (gold != null)
            {
                return new search
                {
                    en_name = gold.en_name,
                    fa_name = gold.fa_name,
                    price_toman = gold.current_Toman,
                    symbol = gold.symbol
                };
            }
            var currecny = searchForCurrency(query);
            if (currecny != null)
            {
                return new search
                {
                    en_name = currecny.en_name,
                    fa_name = currecny.fa_name,
                    price_toman = currecny.current_Toman,
                    symbol = currecny.symbol
                };
            }
            var crypto = searchForCryptoCurrency(query);
            if (crypto != null)
            {
                return new search
                {
                    en_name = crypto.en_name,
                    fa_name = crypto.fa_name,
                    price_toman = PersianHelper.ToDecimal(crypto.price_toman),
                    symbol = crypto.symbol
                };
            }
            return null;
        }

        public static async Task<bool> SearchOperation(string query, long chatId)
        {
            var result = search(query);
            if (result != null)
            {
                //make a result message 
                string txt = $"{result.fa_name ?? result.en_name} : {result.price_toman} تومان است ";
                //send to user
                await TelegramService.SendMessage(chatId, txt);
                TrackingService.Add(chatId, query , requestType.message , message : query);
                return true;
            }
            return false;
        }
    }

    public class search
    {
        public string symbol { get; set; }
        public string fa_name { get; set; }
        public string en_name { get; set; }
        public string price_toman { get; set; }
    }
}