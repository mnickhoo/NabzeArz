using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class Report
    {
        public string key { get; set; }
        public string requestType { get; set; }
        public int count { get; set; }
        public DateTime date { get; set; }
    }
}