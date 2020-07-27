using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NabzeArz.Models
{
    [Table("UserBot")]
    [KnownTypeAttribute(typeof(UserModel))]
    public class UserModel : BaseModel
    {
        public string userName { get; set; }
        public long chatId { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string operation { get; set; }
        public string affiliate_id { get; set; }
        public string walletDogcoin { get; set; }
        public long refer_chat_id { get; set; }
        public int point { get; set; }
        public DateTime last_activity { get; set; } = DateTime.Now;
        public bool is_join_channel { get; set; }
        public int verification_code { get; set; }
        public bool isCompleteRegister { get; set; } = false;
        public virtual List<possensionModel> possensions { get; set; }
        //should be dataannotation for phone
        public string phone { get; set; }
        public bool unsubscriber { get; set; } = false; 
        //should be dataannotation for email
        //[EmailAddress(ErrorMessage = "آدرس ایمیل وارد شده صحیح نمی باشد")]
        public string email { get; set; }
        [InverseProperty("User")]
        public virtual List<PointModel> Points { get; set; }
        public virtual List<ticketModel> tickets { get; set; }
        public string language { get; set; }
        [NotMapped]
        public string emailData { get; set; }
        [NotMapped]
        public string phoneData { get; set; }
        [NotMapped]
        public string dogeCoinWalletData { get; set; }
    }
}