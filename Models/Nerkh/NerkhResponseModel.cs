using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models.Nerkh
{
    public abstract class NerkhResponseModel<T>
    {
        public virtual Data<T> data { get; set; }
    }
    public class Data<T>
    {
        public string message { get; set; }
        public string status { get; set; }
        public virtual T Prices  { get; set; } //must be generate class
    }
    public abstract class NerkhAbstract
    {
        public string min { get; set; }
        public string max { get; set; }
        public string current { get; set; }
        public string changePercent { get; set; }
        public int changePrice { get; set; }
    }
}