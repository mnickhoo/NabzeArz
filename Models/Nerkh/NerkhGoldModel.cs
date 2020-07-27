using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.Nerkh
{
    public class NerkhGoldModel : NerkhResponseModel<GoldSymbol>
    {
        public override Data<GoldSymbol> data { get; set; }
    }

    public class GoldSymbol
    {
        public currencyModel geram18 { get; set; }
        public currencyModel seke_bahar { get; set; }
        public currencyModel gerami { get; set; }
        public currencyModel mesghal { get; set; }
        public currencyModel sekee_emami { get; set; }
        public currencyModel rob { get; set; }
        public currencyModel ons { get; set; }
        public currencyModel geram24 { get; set; }
        public currencyModel nim { get; set; }
    }


}