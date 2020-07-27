using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.API
{
    public interface ICryptoType
    {
        int id { get; set; }
        string en_name { get; set; }
        string fa_name { get; set; }
        string symbol { get; set; }
        int rank { get; set; }
        double price_usd { get; set; }
        double price_btc { get; set; }
        double price_toman { get; set; }
        double market_cap_usd { get; set; }
        double available_supply { get; set; }
        double total_supply { get; set; }
        double max_supply { get; set; }
        string percent_change_1h { get; set; }
        string percent_change_24h { get; set; }
        string percent_change_7d { get; set; }
    }
}