using NabzeArz.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NabzeArz.Areas.Admin.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        // GET: Admin/Ticket show all tickets
        public ActionResult Index()
        {
            //show all tickets
            var tickets =  TicketService.GetTickets(); 
            return View(tickets);
        }

        public ActionResult DogeCoin()
        {
            //get post that type GetDogeCoin
            var tickets = TicketService.GetTicketsByType("GetDogeCoin"); 
            return View(tickets);
        }

        public async Task<JsonResult> DogeCoinRecieved(int userId , int ticketId)
        {
            var user = UserService.GetUser(userId); //get user by id
            if (user != null) //select user
            {
                //check if userPoint is greater than 1000
                if (PointService.GetSumUserPoint(user.chatId) > 1000 )
                {
                    //add point -1000 to user 
                    PointService.AddPoint(user.chatId, NabzeArz.Models.TypePoint.GetDogeCoin);
                    TicketService.UpdateStatus(ticketId, "1"); //update ticketId
                    //send message to user doge coin is received 
                    await TelegramService.SendMessage(user.chatId, "شما دوج کوین خود را دریافت کردید");
                    return Json(new {Error = false , Message = "با موفقیت اعمال شد" });
                }
                return Json(new { Error = true, Message = "امتیاز کاربر به حد مجاز نرسیده است" });
            }
            return Json(new { Error = true, Message = "مشکل در ثبت" });
        }
    }
}