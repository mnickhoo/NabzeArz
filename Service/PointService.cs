using IdentitySample.Models;
using NabzeArz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Service
{
    public class PointService
    {
        private static ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }
        public static ApplicationDbContext db { get { return GetDb(); } }
        //we need a method to add new point 

        public static bool AddPoint(long chatId, TypePoint type) //type should be enum
        {
            using (ApplicationDbContext dbacess = new ApplicationDbContext())
            {
                var user = dbacess.UsersBot.Where(u => u.chatId == chatId).FirstOrDefault(); //get user by ChatId
                if (user != null)
                {
                    var point = new PointModel()
                    {
                        Type = GetEnumName(type),
                        Date = DateTime.Now,
                        User = user,
                        Amount = (int)type
                    };
                    dbacess.Points.Add(point);
                    dbacess.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public static string GetEnumName(TypePoint type)
        {
            int value = (int)type; 
            string name = Enum.GetName(typeof(TypePoint), value);
            return name; 
        }

        public static IEnumerable<PointModel> GetUserPoints(long chatId)
        {
            //get point with chatId
            var points = db.Points.Where(p => p.User.chatId == chatId).ToList();
            return points;
        }
        public static int GetSumUserPoint(long chatId)
        {
            var points = GetUserPoints(chatId);
            var sumPoints = points.Select(p => p.Amount).Sum();
            return sumPoints; 
        }
    }
}