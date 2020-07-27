using NabzeArz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class USDAPIModel
    {
        private int _toman;
        
        public string p
        {
            get
            {
                return PersianHelper.ToDecimal(_toman.ToString());
            }
            set
            {
                _toman = (Convert.ToInt32(value.Replace(",",""))) / 10; 
            }
        }
        public string h { get; set; }
        public string l { get; set; }
        public string d { get; set; }
        public string dp { get; set; }
        public string dt { get; set; }
        public string t { get; set; }
        public string tg { get; set; }
    }
}