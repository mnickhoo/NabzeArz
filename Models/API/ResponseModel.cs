using AutoMapper;
using NabzeArz.Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.API
{
    public class ResponseModel
    {
        public ResponseModel(IEnumerable<CryptoRate> cryptoRates)
        {
            #region old_version
            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<CryptoRate, CryptoType>(); }).CreateMapper();
            //Mapper.Initialize(cfg => { cfg.CreateMap<IEnumerable<CryptoRate>, IEnumerable<CryptoType>>(); });

            //var x = Mapper.Map<List<CryptoType>>(cryptoRates);
            #endregion

            this.data = cryptoRates.Select(c => new CryptoType
            {
                en_name = c.en_name,
                fa_name = c.fa_name,
                available_supply = Convert.ToDouble(c.available_supply),
                id = c.ID,
                market_cap_usd = Convert.ToDouble(c.market_cap_usd),
                max_supply = c.max_supply,
                percent_change_1h = c.percent_change_1h,
                percent_change_24h = c.percent_change_24h,
                percent_change_7d = c.percent_change_7d,
                price_btc = Convert.ToDouble(c.price_btc),
                price_toman = Convert.ToDouble(c.price_toman),
                price_usd = Convert.ToDouble(c.price_usd),
                rank = Convert.ToInt32(c.rank),
                symbol = c.symbol,
                total_supply = Convert.ToDouble(c.total_supply)
            }).ToList();

            if (this.data != null)
            {
                this.status = true;
                this.message = "Successfull";
            }
            else
            {
                this.status = false;
                this.message = "there is a problem!";
            }
            this.last_update = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
        public ResponseModel()
        {
            this.status = false;
            this.message = "there is a problem!";
        }

        public IEnumerable<CryptoType> data { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
        public string last_update { get; set; } //last update
    }
}