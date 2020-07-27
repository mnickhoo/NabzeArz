using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    [Table("UserTracking")]
    public class TrackingModel
    {
        public int id { get; set; }
        public long chatId { get; set; }
        public string operation { get; set; }
        public DateTime date { get; set; }
        public string requestType { get; set; }

    }
}