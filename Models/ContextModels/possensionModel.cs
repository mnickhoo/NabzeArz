using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    [Table("possension")]
    public class possensionModel
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("user")]
        public int user_id { get; set; }
        [ForeignKey("currency")]
        public int currency_id { get; set; }
        public double amount { get; set; }
        public virtual UserModel user { get; set; }
        public virtual CurrencyModel currency { get; set; }
    }
}