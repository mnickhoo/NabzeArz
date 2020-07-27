using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CointPrice.Models
{
    public class CryptoCurrencey
    {
        public string time { get; set; }
        public BTC BTC { get; set; }
        public ETH ETH { get; set; }
        public ETC ETC { get; set; }
        public XMR XMR { get; set; }
        public XRP XRP { get; set; }
        public ZEC ZEC { get; set; }
        public FCT FCT { get; set; }
        public LTC LTC { get; set; }
        public DOGE DOGE { get; set; }
        public DASH DASH { get; set; }
    }

    public class BTC
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }

    public class Rates
    {
        public TMN TMN { get; set; }
        public USD USD { get; set; }
    }

    public class TMN
    {
        public string name { get; set; }
        public int rate { get; set; }
    }

    public class USD
    {
        public string name { get; set; }
        public string rate { get; set; }
    }

    public class ETH
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }


    public class ETC
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }




    public class XMR
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }

    public class XRP
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }

    public class ZEC
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }

    public class FCT
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }

    public class LTC
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }


    public class DOGE
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }

    public class DASH
    {
        public string name { get; set; }
        public Rates rates { get; set; }
    }
}
