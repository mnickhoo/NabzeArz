using NabzeArz.Models.API;
using NabzeArz.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NabzeArz.Controllers
{
    public class V1Controller : ApiController
    {
        public IHttpActionResult GetCrypto()
        {
            var currencies = CoinMarketAPISerivce.GetCurrencyList();
            if (currencies != null)
            {
                ResponseModel response = new ResponseModel(currencies);
                return Ok(response);
            }

            return Ok(currencies);
        }

        public IHttpActionResult GetCrypto(string symbol)
        {
            //return single crypto currency that filtered by symbol
            var currency = CoinMarketAPISerivce.GetCurrencyList().Where(c => c.symbol == symbol).FirstOrDefault();
            return Ok(currency);
        }

        public IHttpActionResult GetCurrency()
        {
            var currencies = NerkhAPIService.Create.GetCurrency();
            return Ok(currencies);
        }
        public IHttpActionResult GetGold()
        {
            var gold = NerkhAPIService.Create.GetGold();
            return Ok(gold);
        }
    }
}
