using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.API
{
    public class CryptoType
    {
        public int id { get; set; }
        public string en_name { get; set; }
        public string fa_name { get; set; }
        public string symbol { get; set; }
        public int rank { get; set; }
        public double price_usd { get; set; }
        public double price_btc { get; set; }
        public double price_toman { get; set; }
        public double market_cap_usd { get; set; }
        public double available_supply { get; set; }
        public double total_supply { get; set; }
        public string max_supply { get; set; }
        public string percent_change_1h { get; set; }
        public string percent_change_24h { get; set; }
        public string percent_change_7d { get; set; }
    }
}