using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public class StatusModel
    {
        public int registered { get; set; }
        public int points { get; set; }
        public int Users { get; set; }
        public List<TrackingModel> Tracking { get; set; }
        public bool status { get; set; } = true;
    }
}