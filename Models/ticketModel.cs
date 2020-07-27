using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class ticketModel 
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("user")]
        public int user_id { get; set; }

        public DateTime date { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public bool isDeleted { get; set; }
        public virtual UserModel user { get; set; }
    }
}