using NabzeArz.Areas.Admin.Models;
using NabzeArz.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
namespace NabzeArz.Areas.Admin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        // GET: Admin/Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Send(MessageViewModel model)
        {
            //send Message to users
            await UserService.SendMessageToAll(model.text);
            return View();
        }
    }
}