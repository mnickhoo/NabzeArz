using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class WidgetModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string hostName { get; set; }
        public string symbol { get; set; }
    }
}