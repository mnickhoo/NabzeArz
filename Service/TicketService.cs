using IdentitySample.Models;
using NabzeArz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot.Types;

namespace NabzeArz.Service
{
    public class TicketService
    {
        //get All ticket 
        public static IEnumerable<ticketModel> GetTickets()
        {
            try
            {
                ApplicationDbContext dbo = new ApplicationDbContext();

                var tickets = dbo.tickets.ToList();
                return tickets;
            }
            catch (Exception e)
            {
                throw new Exception("Error from TicketService/GetTickets : " + e.Message, e.InnerException);
            }
        }

        //get ticket by user_id 
        public static IEnumerable<ticketModel> GetTicketsByUserID(int userId)
        {
            try
            {
                ApplicationDbContext dbo = new ApplicationDbContext();
                var tickets = dbo.tickets.Where(t => t.user_id == userId).ToList().OrderBy(t => t.date);
                return tickets;
            }
            catch (Exception e)
            {
                throw new Exception("Error from TicketService/GetTicketsByUserID : " + e.Message, e.InnerException);
            }
        }

        //get ticket filter by type
        public static IEnumerable<ticketModel> GetTicketsByType(string type)
        {
            try
            {
                ApplicationDbContext dbo = new ApplicationDbContext();
                var tickets = dbo.tickets.Where(t => t.type == type).ToList().OrderBy(t => t.date);
                return tickets;
            }
            catch (Exception e)
            {
                throw new Exception("Error from TicketService/GetTicketsByType : " + e.Message, e.InnerException);
            }
        }
        public async static Task AddDogeCoinTicket(CallbackQuery callback)
        {
            try
            {
                ApplicationDbContext dbo = new ApplicationDbContext();
                //get user by ChatId 
                var user = UserService.GetUser((long)callback.From.Id); 
                if (user != null) //check user is exist
                {
                    if (!string.IsNullOrEmpty(user.email) && !string.IsNullOrEmpty(user.walletDogcoin)) //check user is registered phone and dogeCoin wallet
                    {
                        //dogeCoin
                        dbo.tickets.Add(new ticketModel { date = DateTime.Now, description = "دریافت 100 دوج کوین", title = "دریافت دوج کوین", type = "GetDogeCoin", status = "0", user_id = user.Id });
                        dbo.SaveChanges(); //save to database
                        await TelegramService.AnswerCallBackQuery(callback.Id, "درخواست دوج کوین ثبت شد", showAlert: true);
                    }
                    else
                    {
                        await TelegramService.AnswerCallBackQuery(callback.Id, "شما شماره موبایل یا آدرس کیف پول خود را ثبت نکرده اید", showAlert: true);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error from TicketService/GetTicketsByType : " + e.Message, e.InnerException);
            }
        }
        public static bool UpdateStatus(int ticketId , string status)
        {
            try
            {
                ApplicationDbContext dbo = new ApplicationDbContext();

                var ticket = dbo.tickets.Where(t => t.id == ticketId).FirstOrDefault();
                ticket.status = status;
                dbo.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error From TicketService/UpdateStatus :" + e.Message , e.InnerException);
            }
        }

    }
}