using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NabzeArz.Service;
namespace NabzeArz.Controllers
{
    public class StatesController : Controller
    {
        // GET: States
        public ActionResult Index()
        {
            var states = TrackingService.forChart();
            ViewData["sum"] = TrackingService.sumRequest();         
            return View();
        }
    }
}