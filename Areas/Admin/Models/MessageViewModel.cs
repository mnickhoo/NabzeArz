using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Areas.Admin.Models
{
    public class MessageViewModel
    {
        public string text { get; set; }
        public bool toAll { get; set; }
        public int chat_id { get; set; }
    }
}