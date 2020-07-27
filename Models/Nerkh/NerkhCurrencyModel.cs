using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.Nerkh
{
    public class NerkhCurrencyModel : NerkhResponseModel<currencySymbol>
    {
        public override Data<currencySymbol> data { get; set; }
    }

    public class currencyModel : NerkhAbstract { }

    public class currencySymbol
    {
        public currencyModel GEL { get; set; }
        public currencyModel AMD { get; set; }
        public currencyModel AZN { get; set; }
        public currencyModel RUB { get; set; }
        public currencyModel THB { get; set; }
        public currencyModel MYR { get; set; }
        public currencyModel HKD { get; set; }
        public currencyModel SGD { get; set; }
        public currencyModel PKR { get; set; }
        public currencyModel INR { get; set; }
        public currencyModel SYP { get; set; }
        public currencyModel BHD { get; set; }
        public currencyModel IQD { get; set; }
        public currencyModel OMR { get; set; }
        public currencyModel QAR { get; set; }
        public currencyModel SAR { get; set; }
        public currencyModel KWD { get; set; }
        public currencyModel NOK { get; set; }
        public currencyModel DKK { get; set; }
        public currencyModel SEK { get; set; }
        public currencyModel AFN { get; set; }
        public currencyModel CHF { get; set; }
        public currencyModel NZD { get; set; }
        public currencyModel AUD { get; set; }
        public currencyModel CAD { get; set; }
        public currencyModel JPY { get; set; }
        public currencyModel CNY { get; set; }
        public currencyModel TRY { get; set; }
        public currencyModel AED { get; set; }
        public currencyModel GBP { get; set; }
        public currencyModel EUR { get; set; }
        public currencyModel USD { get; set; }
        public currencyModel USD_EX { get; set; }
    }

}
