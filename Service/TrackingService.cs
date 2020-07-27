using IdentitySample.Models;
using NabzeArz.Hubs;
using NabzeArz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Service
{
    public class TrackingService
    {
        private static ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }
        public static bool Add(long chatId, string operation, requestType requestType, string message = null)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                TrackingModel UserTracking = new TrackingModel()
                {
                    chatId = chatId,
                    date = DateTime.Now,
                    operation = operation,
                    requestType = requestType.ToString()
                };
                db.Usertracking.Add(UserTracking);
                db.SaveChanges();
                StatesHub.pushStates(operation);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error from TrackingService/Add" + e.Message);
            }
        }

        public static int sumRequest()
        {
            ApplicationDbContext db = GetDb();
            int result = db.Usertracking.ToList().Count();
            return result;
        }
        public static int sumRequest(DateTime date)
        {
            try
            {
                ApplicationDbContext db = GetDb();
                var result = db.Usertracking.Where(u => u.date >= date).ToList().Count();
                return result; //result 
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static IEnumerable<Report> forChart()
        {
            var tracks = GetDb().Usertracking.AsEnumerable();

            var r = from track in tracks
                    group track by new { track.requestType, track.operation } into trackg
                    select new Report { key = trackg.Key.operation, count = trackg.Count(), requestType = trackg.Key.requestType };

            return r.ToList();
        }
        public static List<TrackingModel> GetTracking()
        {
            //
            var tracks = GetDb().Usertracking.Where(t => t.date == DateTime.Today).AsQueryable();
            var x = tracks.ToList();
            return x;
        }
        public static List<TrackingModel> GetTracking(int day)
        {
            //get tracking day ago to tommorow 
            var today = DateTime.Today;
            var StartDay = DateTime.Today.AddDays(-day);
            var tracks = GetDb().Usertracking.Where(t => t.date >= StartDay && t.date <= today ).AsQueryable();
            var x = tracks.ToList();
            return x;
        }
    }
    public enum EnumOperation
    {
        BTC_ChartRequest,
        ETH_chart,
        USD_chart,
        register,
        BTC_price,
        ETH_price,
        USD_priceRequest,
        helpRequest,
        show_pointsRequest,
        settingRequest,
        affiliate,
        showTopPriceRequest,
        donateRequest,
        newsRequest,
        AffiliateLinkRequest,
        addEmail,
        addPhone,
        addDogeCoin,
        sendContactRequest,
        NoResultRequest,
        settingAddEmailRequest,
        settingAddPhoneRequest,
        settingEmailPageRequest,
        settingDogecoinPageRequest,
        settingPhonePageRequest,
        settingEmailValidationErrorRequest,
        waitForEmailRequest,
        waitForDogecoinWalletRequest,
        settingMainMenueRequest,
        registerWithAffiliateIdRequest,
        registerNormal,
        getStates , 
        showPossensionRequest , 
        posession_add_ETH , 
        posession_add_BTC , 
        posession_add_XRP , 
        posession_add_BCH , 
        posession_add_TRX , 
        posession_add_LTC , 
        contactus , 
        mypossension , 
        services , 
        backToMainMenue , 
        fiatProcess , 
        showGoldPRice ,
        showFiatPRice , 
        showCryptoPrice , 
        searchInside

    }

    public enum requestType
    {
        message,
        inline,
        CallbackQueryUpdate
    }
}