using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    [Table("Point")]
    public class PointModel : BaseModel
    {
        [Required]
        public string Type { get; set; }
        public int Amount { get; set; }
        public bool Dont_count { get; set; }
        [InverseProperty("Points")]
        public virtual UserModel User { get; set; }
    }
    public enum TypePoint
    {
        Affiliate = 50 , 
        CompleteProfile = 20 , 
        FirstRegister = 30 , 
        LeaveMember = -50 , 
        GetDogeCoin = -1000
    }
}