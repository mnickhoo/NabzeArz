using IdentitySample.Models;
using NabzeArz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Service.state
{
    public class StateService
    {
        public static int GetUserRegisterdCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var count = db.UsersBot.ToList().Count();
            return count;
        }

        public static int GetTrackingTodayCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var count = db.Usertracking.Where(t => t.date >= DateTime.Today).ToList().Count; 
            return count;
        }

        public static int GetTrackingTotallCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var count = db.Usertracking.ToList().Count;
            return count;
        }

    }
}