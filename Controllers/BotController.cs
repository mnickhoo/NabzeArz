using NabzeArz.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NabzeArz.Controllers
{
    public class BotController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Index()
        {
            #region get request section and deserialize to update message
            var responsString = new StreamReader(Request.InputStream).ReadToEnd(); //read request 
            var update = JsonConvert.DeserializeObject<Update>(responsString); // deserialize to update model
            #endregion
            switch (update.Type)
            {
                case UpdateType.MessageUpdate:
                    AssetService.ProccessMessage(update.Message); //process request
                    break;

                case UpdateType.InlineQueryUpdate:
                    await TelegramService.AnswerQueery(update);
                    break;

                case UpdateType.CallbackQueryUpdate:
                    AssetService.ProcessCallBack(update.CallbackQuery);
                    break;

                case UpdateType.ChosenInlineResultUpdate:
                    break;

                case UpdateType.EditedMessage:
                    break;
            }
            return Json(data: "successfull");
        }

        public ActionResult nabzeArz()
        {
            return View();
        }
    }
}