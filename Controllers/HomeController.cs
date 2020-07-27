using NabzeArz.Helper;
using NabzeArz.Models;
using NabzeArz.Models.Nerkh;
using NabzeArz.Service;
using NabzeArz.Service.state;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NabzeArz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            return View();
        }
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
                    await AssetService.ProccessMessage(update.Message); //process request
                    break;

                case UpdateType.InlineQueryUpdate:
                    await TelegramService.AnswerQueery(update);
                    break;

                case UpdateType.CallbackQueryUpdate:
                    await AssetService.ProcessCallBack(update.CallbackQuery);
                    break;

                case UpdateType.ChosenInlineResultUpdate:
                    break;

                case UpdateType.EditedMessage:
                    break;
            }

            #region comment
            //if (update.Type == UpdateType.InlineQueryUpdate)
            //{
            //    await TelegramService.AnswerQueery(update);
            //    return Json(data: "successfull");
            //}
            //else if (update.CallbackQuery != null) //check is from callback
            //{
            //    if (await UserService.ChangeMemberIsInChannel(update.CallbackQuery.From.Id))
            //    {
            //        //user has joined in channel 
            //        await AssetService.SendHelpMessage(update.CallbackQuery.From.Id);  //send help message
            //    }
            //    else
            //    {
            //        await TelegramService.AnswerCallBackQuert(update.CallbackQuery.Id, "متاسفانه هنوز عضو کانال ما نشده اید", showAlert: true);
            //    }
            //}
            //else if (update.Message != null)
            //{
            //    if (message.Text.StartsWith("/start")) //is message /start
            //    {
            //        if (TelegramService.hasRegisterd(chat.Id))  //if user had registered
            //        {
            //            //send message to user you had registered 
            //            await TelegramService.SendMessage(chat.Id, "⚠️ ثبت نام شده انجام شده و یا هنوز عضو کانال ما نشده اید \n\n عضویت در کانال نبض ارز 👇👇 \n  🏆 @nabzearz", new InlineKeyboardButton[][] { new InlineKeyboardButton[] { new InlineKeyboardButton { Text = "بررسی مجدد", CallbackData = "retry" } } });
            //        }
            //        else //user must be register in database
            //        {
            //            //process affiliate program 
            //            var affiliate_status = TelegramService.CheckAffiliateStatus(message.Text);
            //            if (affiliate_status.is_affiliate) //check is affiliate start
            //            {
            //                //save user in data base 
            //                var result = UserService.CreateUser(message, affiliate_status.affiliate_chat_id);

            //                //send welcome message to user
            //                if (result)
            //                {
            //                    await TelegramService.SendMessage(chat.Id, "خوش آمد گویی : \n ثبت نام شما با موفقیت انجام شد و از طرف خانواده ی بزرگ نبض ارز ورود شما را خوش آمد میگوییم");
            //                    if (await TelegramService.IsMemberInChannel(chat.Id)) //is member joined in channell except status "left" & "Kick"
            //                    {
            //                        await AssetService.SendHelpMessage(chat.Id);
            //                    }
            //                    else //user have not join in bot
            //                    {
            //                        //send message to user you must join in chanell
            //                        await AssetService.ShouldBejoinInChanell(chat.Id);
            //                    }
            //                    //process point of affiliate
            //                    var point = UserService.SumPoint(affiliate_status.affiliate_chat_id);
            //                    //send message to affiliate include pont and notification
            //                    await TelegramService.SendMessage(affiliate_status.affiliate_chat_id, $"کاربر {chat.FirstName} {chat.LastName} با لینک شما ثبت نام کرد \n\n 🔴 امتیاز شما :" + point);
            //                }
            //            }
            //            else
            //            {
            //                //save user in databasae 
            //                var result = UserService.CreateUser(message);

            //                if (result) //send welcome message to user
            //                    await AssetService.SendWelcomeMessage(chat.Id);

            //                if (await TelegramService.IsMemberInChannel(chat.Id)) //is member joined in channell except status "left" & "Kick"
            //                {
            //                    await AssetService.SendHelpMessage(chat.Id);
            //                }
            //                else //user have not join in bot
            //                {
            //                    //send message to user you must join in chanell
            //                    await AssetService.ShouldBejoinInChanell(chat.Id);
            //                }

            //            }
            //        }
            //    }
            //    else
            //    {
            //        await TelegramService.SendMessage(chat.Id, "doste man [nabzearz](http://nabzearz.ir) khobi ?", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            //        await AssetService.CheckCommand(message);

            //        ////means user had registered and check is user in channel 
            //        //if (TelegramService.hasRegisterd(chat.Id))
            //        //{
            //        //    if (await TelegramService.IsMemberInChannel(chat.Id)) //is member joined in channell except status "left" & "Kick"
            //        //    {
            //        //        //check operation 

            //        //        //check command 
            //        //        await AssetService.CheckCommand(message);
            //        //    }
            //        //    else //user have not join in bot
            //        //    {
            //        //        //send message to user you must join in chanell
            //        //        await AssetService.ShouldBejoinInChanell(chat.Id);
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    await TelegramService.SendMessage(chat.Id, "ثبت نام شما انجام نشده برای ثبت نام بر روی /start کلیک کنید");
            //        //}
            //    }
            //    return Json(data: "successfull");
            //}
            #endregion
            return Json(data: "successfull");
        }

        public ActionResult About()
        {
            return View();
        }
        public async Task<JsonResult> Start()
        {
            //Run send message task schedule
            ISchedule taskSend = new SchaduleSendMessage();
            await taskSend.Run();

            ////Run update message task schedule
            ISchedule taskUpdate = new SchaduleUpdateMessage();
            await taskUpdate.Run();

            ////Run update USD price 
            ISchedule taskChart = new schaduleChart();
            await taskChart.Run();

            //update Gold and fiat currency in database
            ISchedule taskNerkh = new schaduleNerkh();
            await taskNerkh.Run();

            return Json("Done!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<ActionResult> test()
        {
            var x = SearchService.search("دلار");
            await CoinMarketAPISerivce.RequestAndSaveToDB(); //request to remote and save in database
            var scheduler = new StdSchedulerFactory().GetScheduler();


            await NerkhAPIService.Create.RequestCurrencyAndSaveToDb();

            return Json("successfull", JsonRequestBehavior.AllowGet);
        }
        public JsonResult widgetAPI(string symbol = "USD") //here should be know hostName
        {
            CurrencyType type;
            Enum.TryParse(symbol, out type);
            var currency = NerkhAPIService.Create.GetCurrencyBySymbol(type);
            return Json(currency, JsonRequestBehavior.AllowGet);

        }
        public JavaScriptResult Widget(string symbol = "USD")
        {
            var js = @"
                document.getElementById('NabzeArz-data').innerHTML=`
                <style>
                #Nabz-Wrapper{
                  background-color : #f1f1f1 ; 
                  width : 250px ; 
                  box-shadow: 1px 3px 12px #5a5a5a;
                  padding : 10px ; 
                  position : inherit ; 
                  direction : rtl !important;
                }
                #header-container{
                  display : grid ; 
                  grid-template-columns : 50px 200px ;
                }
                .currency-icon{
                  position : relative ;
                  display : inline-block ;
                }
                .price {
                    position: relative;
                    display: inline-block;
                    text-align: center;
                    font-size: 32px;
                    font-family: tahoma;
                }
                #nabzearz-footer{
                  text-align:center;
                }
                #nabzearz-footer a{
                  color : #013243;
                  text-decoration : none;
                }
                .currency-icon img{
                  height : 50px
                }

                #nabzearz-footer{
                font-size:10px;
                }
                div#nabzearz-price:after {
                    content: ' تومان';
                    font-size: 20px;
                }
                </style>
                <div id='Nabz-Wrapper'>
                    <div id='header-container' >
                        <div class='currency-icon'>
                            <img src = 'https://bot.nabzearz.ir/content/icon/" + symbol + @".png' />
                        </div >
                <div id='nabzearz-price' class='price'></div>
                </div>
                <div id='nabzearz-footer' >
                   قدرت گرفته از<a href='https://nabzearz.ir'>نبض ارز  </a>
                </div>
        </div>`;

        function loadDoc()
        {
            var req = new XMLHttpRequest();
            if ('withCredentials' in req) {
                req.open('GET', 'https://bot.nabzearz.ir/home/widgetAPI?symbol=" + symbol + @"', true);
                req.onreadystatechange = function() {
                    if (req.readyState === 4)
                    {
                        if (req.status >= 200 && req.status < 400)
                        {
                            var myObj = JSON.parse(req.responseText);
                            var price = myObj.current_Toman;
                            var png =   myObj.Status;               
                            document.getElementById('nabzearz-price').innerHTML = price;
                        }
                    }
                };
                req.send(); //send
            } else { console.log('no'); }
            setTimeout(function(){ loadDoc() }, 500000);
        }
  loadDoc();
        document.getElementById('Nabz-Wrapper').style.cursor = 'pointer';
	document.getElementById('Nabz-Wrapper').onclick = function() { location.href = 'https://nabzearz.ir' }";

            return JavaScript(js);
        }
    }
}
