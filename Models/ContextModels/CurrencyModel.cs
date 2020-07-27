using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    [Table("Currency")]
    public class CurrencyModel
    {
        [Key]
        public int id { get; set; }
        public string symbol { get; set; }
        public string Name { get; set; }
        public string persianName { get; set; }
        public virtual ICollection<possensionModel> possension { get; set; }
        //[ForeignKey("id")]
        //public possensionModel possension { get; set; }
    }

    public enum CurrencyEnum
    {
        BTC , 
        ETH , 
        ETC , 
        BCH , 
        XRP , 
        LTC
    }

}