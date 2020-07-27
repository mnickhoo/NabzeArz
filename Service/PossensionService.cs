using NabzeArz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NabzeArz.Helper;
using IdentitySample.Models;

namespace NabzeArz.Service
{
    public class PossensionService
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        //add possension 
        public static int Add(possensionModel possension)
        {
            try
            {
                //get recored with user_Id and currency_id 
                var record = db.possensions.Where(p => p.user_id == possension.user_id && p.currency_id == possension.currency_id).FirstOrDefault();
                if (record != null)
                {
                    record.amount = possension.amount;
                    return db.SaveChanges();
                }
                db.possensions.Add(possension);
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("error From PossensionService/Add" + e.Message, e.InnerException);
            }
        }
        //remove possension
        public static bool Remove(long chatId, int currency_id)
        {
            var user = db.possensions.Where(p => p.user.chatId == chatId && p.currency.id == currency_id);
            db.possensions.RemoveRange(user);
            db.SaveChanges();
            return true;
        }
        //get my possension
        public static List<possensionModel> GetMyPossension(long chatId)
        {
            var possension = db.possensions.Where(p => p.user.chatId == chatId).ToList();
            return possension;
        }
        public static Tuple<bool, string> TemplateForMessage(ICollection<possensionModel> possensions)
        {

            var USDRate = NerkhAPIService.Create.GetCurrencyBySymbol(CurrencyType.USD).current; //Get USDPrice
            string Message = ""; //message
            if (possensions.Count != 0)
            {
                Message += "دارایی شما به شرح زیر می باشد : \n";
                foreach (var p in possensions)
                {
                    var USD =  CurrencyService.ConvertToUSD(p.amount, CurrencyService.EnumParse(p.currency.symbol));
                    Message += $"{p.currency.persianName} : {p.amount} \n" +
                               $"دلار : {USD} \n" +
                               $"تومان : {CurrencyService.ConvertToToman(Convert.ToDouble(USDRate), USD)} \n";
                }
                return new Tuple<bool, string>(true, Message);
            }
            Message += "شما هیچ دارایی ثبت نکردید";
            return new Tuple<bool, string>(false, Message);
        }
    }
}