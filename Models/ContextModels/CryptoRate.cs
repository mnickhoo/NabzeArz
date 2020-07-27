using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.ContextModels
{
    public class CryptoRate 
    {
        public int ID { get; set; }
        public string en_name { get; set; }
        public string fa_name { get; set; }
        public string symbol { get; set; }
        public string rank { get; set; }
        public string price_usd { get; set; }
        public string price_btc { get; set; }
        public string price_toman { get; set; }
        public string price_rial { get; set; }
        public string _24h_volume_usd { get; set; }
        public string market_cap_usd { get; set; }
        public string available_supply { get; set; }
        public string total_supply { get; set; }
        public string max_supply { get; set; }
        public string percent_change_1h { get; set; }
        public string percent_change_24h { get; set; }
        public string percent_change_7d { get; set; }
        public string persianDate { get; set; }
    }
}