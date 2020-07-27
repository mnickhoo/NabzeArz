using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.Nerkh
{
    public interface INerkhRate
    {
       int id { get; set; }
       string symbol { get; set; }
       string min { get; set; }
       string max { get; set; }
       string current { get; set; }
       string changePercent { get; set; }
       int changePrice { get; set; }
    }
}