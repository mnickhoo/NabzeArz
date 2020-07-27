using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    [Table("Channel")]
    public class ChannelModel 
    {
        [Key]
        public int Id { get; set; }
        public int last_channel_message { get; set; }
        public string user_name { get; set; }
        public long chat_id { get; set; }
    }
}