using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class AffiliateModel
    {
        public bool is_affiliate { get; set; }
        public bool is_refer_true { get; set; }
        public string invite_key { get; set; }
        public long affiliate_chat_id { get; set; }
    }
}