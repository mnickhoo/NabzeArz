using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class SanaModel
    {
        public SanaObject sana_buy_usd { get; set; }
        public SanaObject sana_buy_eur { get; set; }
        public SanaObject sana_buy_aed { get; set; }
        public SanaObject sana_sell_usd { get; set; }
        public SanaObject sana_sell_eur { get; set; }
        public SanaObject sana_sell_aed { get; set; }
    }

    public class SanaObject
    {
        public string title { get; set; }
        public string price { get; set; }
        public string jdate { get; set; }
        public string gdate { get; set; }
    }

}