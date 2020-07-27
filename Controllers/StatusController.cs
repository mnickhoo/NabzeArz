using IdentitySample.Models;
using NabzeArz.Models;
using NabzeArz.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace NabzeArz.Controllers
{
    public class StatusController : ApiController
    {
        public IHttpActionResult GetStatus()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var status = new StatusModel()
            {
                registered = db.UsersBot.ToList().Count(),
            };
            return Ok(status);
        }
        public IHttpActionResult GetStatus(int day , string operation= null , string requestType = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //filter on operation and request Type 
            var Tracking = TrackingService.GetTracking(day);
            if (!string.IsNullOrEmpty(operation))
            {
                Tracking = Tracking.Where(t => t.operation == operation).ToList();
            }
            if (!string.IsNullOrEmpty(requestType))
            {
                Tracking = Tracking.Where(t => t.requestType == requestType).ToList();
            }
            var status = new StatusModel()
            {
                registered = db.Users.ToList().Count(),
                Tracking = Tracking
            };
            return Ok(status);
        }
        public IHttpActionResult GetMe()
        {
            return Ok(new { hey = "welcome to nabzeArz API" });
        }
    }
}
