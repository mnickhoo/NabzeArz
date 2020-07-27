using NabzeArz.Helper;
using NabzeArz.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.Nerkh
{
    public class CurrencyRate : INerkhRate
    {
        public int id { get; set; }
        [Display(Name = "نماد")]
        public string symbol { get; set; }
        [Display(Name = "کمترین نرح")]
        public string min { get; set; }
        [Display(Name = "بیشترین نرخ")]
        public string max { get; set; }
        [Display(Name = "نرخ حاضر")]
        public string current { get; set; }
        [Display(Name = "تغییر قیمت")]
        public string changePercent { get; set; }
        [Display(Name = "تغییر قیمت")]
        public int changePrice { get; set; }
        [Display(Name = "نمایش در کانال")]
        public bool showIntoChannel { get; set; }
        [Display(Name = "نام فارسی")]
        public string fa_name { get; set; }
        [Display(Name = "نام انگلیسی")]
        public string en_name { get; set; }
        [Display(Name = "مرتب سازی")]
        public int order_list { get; set; }
        [NotMapped]
        public string persianName { get { return PersianHelper.GetPersianName(this.symbol); } }
        [NotMapped]
        public string flag { get { return PersianHelper.flag(this.symbol); } }
        [NotMapped]
        public string flagUrl { get { return "somthing return as url path"; } }
        [NotMapped]
        public string current_Toman { get { return PersianHelper.ToDecimal(CurrencyService.ToToman(this.current)); } }
        [NotMapped]
        public string max_Toman { get { return PersianHelper.ToDecimal(CurrencyService.ToToman(this.max)); } }
        [NotMapped]
        public string min_Toman { get { return PersianHelper.ToDecimal(CurrencyService.ToToman(this.min)); } }

    }
}