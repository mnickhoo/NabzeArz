using IdentitySample.Models;
using NabzeArz.Helper;
using NabzeArz.Models;
using NabzeArz.Models.ContextModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace NabzeArz.Service
{
    public class TelegramService
    {
        #region field section 
        protected static TelegramBotClient api { get { return getApi(); } }
        protected static ApplicationDbContext db = GetDb();
        #endregion


        /// <summary>
        /// get database context
        /// </summary>
        /// <returns></returns>
        private static ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// get user witch chat id 
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        private static UserModel GetUser(long chatId)
        {
            return db.UsersBot.Where(t => t.chatId == chatId).FirstOrDefault();
        }

        private static TelegramBotClient getApi()
        {
            //set Access Token from telegram
            return new TelegramBotClient(AppSetting.TelegramTokenTest);
        }


        public static async Task<Message> SendMessageToChanell(string text, bool disableNotification = true , ParseMode parseMode = ParseMode.Markdown)
        {
            //this section is hardcord and send to chanell
            //return await api.SendTextMessageAsync(AppSetting.channel_chatId, text, disableNotification: disableNotification);   //origin chanell id

            ////for test chanell id 
            return await api.SendTextMessageAsync(AppSetting.channel_chatId_test, text, disableNotification: disableNotification, parseMode: parseMode);
        }

        public static async Task<Message> UpdateMessageToChannel(string text, bool disableNotification = true , ParseMode parseMode = ParseMode.Default)
        {
            int messageId = UserService.GetMessageId(); //get chat_id
            //this section is hardcord and send to chanell
            return await UpdateMessage(Convert.ToInt64(AppSetting.channel_chatId_test), text, messageId, parseModel : parseMode);

            ////for test chanell id 
            //return await api.SendTextMessageAsync(-1001106842649, text , disableNotification : disableNotification);
        }

        //send message 
        public async static Task<Message> SendMessage(long chatId, string text, int replyToMessage = 0, bool defaultKeyboard = false, bool hideKeyboard = false, ParseMode parseMode = ParseMode.Default , KeyboardButton[][] replyKeyboard = null)
        {
            if (defaultKeyboard) //if default keyboard has true
            {
                var btn = new KeyboardButton[][]
              {
                    new KeyboardButton[] { "معرفی به دوستان",  "خدمات 🔧" } ,
                    new KeyboardButton[] { "تنظیمات ⚙️"  , "امتیاز شما 🎖" , "راهنما ❓" }
              };
                try
                {
                    ReplyKeyboardMarkup rkb = CreateKeyboardButton(btn);
                    //var result = await api.SendTextMessageAsync(chatId, text ,  false,  false, 0, rkb);
                    var result = await api.SendTextMessageAsync(chatId, text, replyMarkup: rkb);
                    return result;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                ReplyKeyboardMarkup rkb = CreateKeyboardButton(replyKeyboard); //create keyboard Button
                return await api.SendTextMessageAsync(chatId, text, parseMode: parseMode, replyToMessageId: replyToMessage , replyMarkup : rkb); //send Message
            }

        }

        public async static Task<Message> RequestContact(long chatId)
        {
            var phone = new KeyboardButton("شماره ام رو بفرست") { RequestContact = true };

            var reply = new KeyboardButton[][]
            {
                    new KeyboardButton[]{ phone }
            };
            //var result = await api.SendTextMessageAsync(chatId, text ,  false,  false, 0, rkb);
            var result = await api.SendTextMessageAsync(chatId, "لطفا شماره موبایل تان را بفرستید", replyMarkup: new ReplyKeyboardMarkup { Keyboard = reply });
            return result;
        }

        //send message without keyboard
        public async static Task<Message> SendMessage(long chatId, string text)
        {
            var reply = await api.SendTextMessageAsync(chatId, text);
            return reply;
        }

        //send message with inline keyboard button
        public async static Task<Message> SendMessage(long chatId, string text, InlineKeyboardButton[][] inlineButtons , ParseMode parseMode = ParseMode.Default)
        {
            return await api.SendTextMessageAsync(chatId, text, replyMarkup: new InlineKeyboardMarkup { InlineKeyboard = inlineButtons } , parseMode : parseMode);
        }

        public async static Task<Message> SendMessage(long chatId, string text, KeyboardButton[][] keyboardButton)
        {
            return await api.SendTextMessageAsync(chatId, text, replyMarkup: new ReplyKeyboardMarkup { Keyboard = keyboardButton }, parseMode: ParseMode.Markdown);
        }

        //send message with keyboard button
        //public async static Task<Message> SendMessage(long chatId, string text, KeyboardButton[][] menue)
        //{
        //    var replyMarkup = CreateKeyboardButton(menue);
        //    var result = await api.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup);
        //    return result;
        //}

        //send message with chatAction
        public async static Task<Message> SendMessage(long chatId, string text, ChatAction chatAction, ParseMode parseMode = ParseMode.Default)
        {
            await api.SendChatActionAsync(chatId, chatAction);
            return await api.SendTextMessageAsync(chatId, text, parseMode: parseMode);
        }
        public async static Task SendChatAction(long chatId, ChatAction chatAction)
        {
            await api.SendChatActionAsync(chatId, chatAction: chatAction);
        }

        public async static Task<Message> UpdateMessage(long chatId, string text, int messageId, ParseMode parseModel = ParseMode.Default)
        {
            return await api.EditMessageTextAsync(chatId, messageId, text, parseMode: parseModel);
        }

        public async static Task<Message> UpdateMessageReplyMarkup(long chatId, int messageId, InlineKeyboardButton[][] inlineKeyboard)
        {
            return await api.EditMessageReplyMarkupAsync(chatId, messageId, replyMarkup: new InlineKeyboardMarkup { InlineKeyboard = inlineKeyboard });
        }

        public async static Task<Message> updateMessageCaption(long chatID, int messageId, string caption, InlineKeyboardButton[][] inlineKeyboard = null)
        {
            return await api.EditMessageTextAsync(chatID, messageId, caption, replyMarkup: new InlineKeyboardMarkup { InlineKeyboard = inlineKeyboard }, parseMode: ParseMode.Markdown);
        }


        //public static bool hasOperationAndType(Message message)
        //{
        //    if (message.Text.StartsWith("/")) //mean user input a command
        //    {

        //    }
        //    var user = GetUser(message.Chat.Id); //get user 
        //    if (user != null) //null handle for user
        //    {
        //        if (user.operation != null )
        //        {
        //            user.operation = null;
        //            db.SaveChanges();
        //            return null;
        //        }
        //        return operation;
        //    }
        //    return null;
        //}

        public async static Task<Message> UploadImage(HttpPostedFileBase image, long chatId, string caption)
        {
            var fileName = image.FileName; // file Name of image
            var fts = new FileToSend(fileName, image.InputStream);

            // send image to chanel 
            return await api.SendPhotoAsync(chatId, fts, caption);
        }
        public async static Task<Message> UploadImage(Stream image, string fileName, long chatId, string caption)
        {
            var fts = new FileToSend(fileName, image);
            // send image to chanel 
            return await api.SendPhotoAsync(chatId, fts, caption);
        }
        public async static Task<Message> UploadImage(Stream image, long chatId, string caption)
        {
            var fts = new FileToSend("image", image);

            // send image to chanel 
            return await api.SendPhotoAsync(chatId, fts, caption);
        }

        public static ReplyKeyboardMarkup CreateKeyboardButton(KeyboardButton[][] keyboards)
        {
            //const int row = 2;
            //const int rowNumber = row; 
            //KeyboardButton[][] kb = new KeyboardButton[row][]
            //    {
            //        keyboards ,
            //        new KeyboardButton[] { new KeyboardButton {Text = "لغو ❌" } , new KeyboardButton { Text = "بازگشت" } } ,
            //    };
            ReplyKeyboardMarkup rkb = new ReplyKeyboardMarkup(keyboards);
            return rkb;
        }

        public static async Task<string> CalbackAnswer(string calbackId)
        {
            InlineQueryResult[] inlines = new InlineQueryResult[]
            {
                new InlineQueryResult { Title = "همینه" }
            };
            await api.AnswerCallbackQueryAsync(calbackId, "جوجه اردک زشت");
            return "success";
        }
        public static async Task<bool> IsMemberInChannel(long chatId)
        {
            try
            {
                var result = await api.GetChatMemberAsync(AppSetting.channel_chatId_test, Convert.ToInt32(chatId));
                if (result != null && result.Status != Telegram.Bot.Types.Enums.ChatMemberStatus.Left && result.Status != Telegram.Bot.Types.Enums.ChatMemberStatus.Kicked)
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static AffiliateModel CheckAffiliateStatus(string message)
        {
            var MessageArray = message.Split(' '); //split example "/start invite_167344742"
            if (MessageArray.Length == 2 && MessageArray.First() == "/start")
            {
                return new AffiliateModel()
                {
                    is_affiliate = true,
                    invite_key = MessageArray[1],
                    affiliate_chat_id = GetAffiliateChatId(MessageArray[1]),
                    is_refer_true = UserService.IsReferTrue(GetAffiliateChatId(MessageArray[1]), MessageArray[1])
                };
            }
            return new AffiliateModel() { is_affiliate = false };
        }

        public async static Task<bool> AnswerQueery(Update update)
        {
            #region history
            //InlineQueryResult[] inlines = new InlineQueryResult[2]
            //{
            //    new InlineQueryResultArticle {InputMessageContent = new InputTextMessageContent{MessageText = "482 تومان" } , Title = "ترون trx" , Id = update.InlineQuery.Id} ,
            //    new InlineQueryResultArticle {InputMessageContent = new InputTextMessageContent{MessageText = "7500 تومان" } , Title = "ریپل xrp" , Id = update.InlineQuery.Id}
            //    //new InlineQueryResultArticle {InputMessageContent = new InputTextMessageContent{MessageText = "3000 تومان" } , Title = "بیت کوین کش bch" , Id = update.InlineQuery.Id}
            //};
            #endregion
            string query = update.InlineQuery.Query; //get query 
            List<InlineQueryResult> inlinQuery = new List<InlineQueryResult>(); //inline query

            if (query == "affiliate")
            {
                var q = "https://t.me/nabzearz_bot?start=" + UserService.GetUser((long)update.InlineQuery.From.Id).affiliate_id;
                inlinQuery.Add(new InlineQueryResultArticle { InputMessageContent = new InputTextMessageContent { MessageText = "سلام دوست من از لینک من میتونی در ربات #نبض_ارز ثبت نام کنی \n\n" + q }, Title = $"ارسال لینک معرفی به دوستان ⬅️", Id = Guid.NewGuid().ToString() });
                TrackingService.Add(update.InlineQuery.From.Id, EnumOperation.affiliate.ToString(), requestType.inline);
            }
            else
            {
                //
                IEnumerable<CryptoRate> currencies = CoinMarketAPISerivce.GetCurrencyList(); //get currencies
                IEnumerable<CryptoRate> filter = currencies.Where(c =>
                                                                       c.fa_name.ToLower().Contains(query) ||
                                                                       c.en_name.ToLower().Contains(query) ||
                                                                       c.symbol.ToLower().Contains(query) ||
                                                                       query == "").Take(50); //maximum records are 50

                foreach (var item in filter)
                {
                    inlinQuery.Add(new InlineQueryResultArticle { InputMessageContent = new InputTextMessageContent { MessageText = TemplateForSingleCurrency(item) }, Title = item.fa_name, Id = Guid.NewGuid().ToString(), ThumbUrl = "https://bot.nabzearz.ir/Content/icon/" + item.symbol.ToLower() + ".png" });
                }
            }
            await api.AnswerInlineQueryAsync(update.InlineQuery.Id, results: inlinQuery.ToArray());
            
            return TrackingService.Add(update.InlineQuery.From.Id, query, requestType.inline);

        }

        private static long GetAffiliateChatId(string affiliateCode)
        {
            var MessageArray = affiliateCode.Split('_');
            return long.Parse(MessageArray[1]);
        }
        public async static Task<bool> AnswerCallBackQuery(string callBackQueryId, string text = null, bool showAlert = false)
        {
            return await api.AnswerCallbackQueryAsync(callBackQueryId, text: text, showAlert: showAlert);
        }
        public static string TemplateForSingleCurrency(CryptoRate currency)
        {
            var text = $"🔶نام ارز : {currency.fa_name} \n" +
                       $"🔶 نام به لاتین : {currency.en_name} \n" +
                       $"🔶 نماد : {currency.symbol} \n" +
                       $"🔹رتبه {currency.rank} \n" +
                       $"🔹{PersianHelper.ToDecimal(currency.price_toman) } {Resources.Fa_IR.TMN} \n" +
                       $"🔹{PersianHelper.ToDecimal(currency.price_usd, DigitType.USD)} {Resources.Fa_IR.USD}\n" +
                       $"🔹حجم بازار : {currency.market_cap_usd} دلار \n" +
                       $"🔹حداکثر توکن : {currency.max_supply} \n" +
                       $"🔹توکن فعال :{currency.available_supply ?? "نامشخص"}  \n" +
                       $"{CurrencyService.iconIncrease(currency.percent_change_1h)} ساعت گذشته : %{currency.percent_change_1h}\n" +
                       $"{CurrencyService.iconIncrease(currency.percent_change_24h)} روز گذشته : %{currency.percent_change_24h}\n" +
                       $"{CurrencyService.iconIncrease(currency.percent_change_7d)} هفته گذشته : %{currency.percent_change_7d} ";
            return text;
        }
        public async static Task<bool> SendWaiting(CallbackQuery callback)
        {
           return await TelegramService.AnswerCallBackQuery(callback.Id, "در حال انجام ...");
        }
        public async static Task<Message> SendContact(long chatId)
        {
            return await api.SendContactAsync(chatId, "+989203951758", "NabzeArz");
        }
    }
}