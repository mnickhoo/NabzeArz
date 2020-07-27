using NabzeArz.Helper;
using NabzeArz.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.Nerkh
{
    public class GoldRate : INerkhRate
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public string current { get; set; }
        public DateTime lastUpdate { get; set; }
        public string changePercent { get; set; }
        public int changePrice { get; set; }
        public string fa_name { get; set; }
        public string en_name { get; set; }
        [NotMapped]
        public string persianName { get { return PersianHelper.GetPersianName(this.symbol); } }
        [NotMapped]
        public string flag { get { return PersianHelper.flag(this.symbol); } }
        [NotMapped]
        public string current_Toman { get { return PersianHelper.ToDecimal(CurrencyService.ToToman(this.current)); } }
        [NotMapped]
        public string max_Toman { get { return PersianHelper.ToDecimal(CurrencyService.ToToman(this.max)); } }
        [NotMapped]
        public string min_Toman { get { return PersianHelper.ToDecimal(CurrencyService.ToToman(this.min)); } }
    }
}