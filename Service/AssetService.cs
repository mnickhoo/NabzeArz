using NabzeArz.Helper;
using NabzeArz.Models;
using NabzeArz.Models.ContextModels;
using NabzeArz.Models.Nerkh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;

namespace NabzeArz.Service
{
    public class AssetService
    {
        public async static Task<Message> SendPrice(long chatId, PriceOption option)
        {
            IEnumerable<CryptoRate> cryptoList = null;
            IEnumerable<CurrencyRate> currencyRates = null;
            IEnumerable<GoldRate> goldRates = null;
            string text = "";
            switch (option)
            {
                case PriceOption.gold:
                    goldRates = NerkhAPIService.Create.GetGold(); //get gold rates  
                    text = CurrencyService.BuildStringForChannel(goldRates : goldRates);
                    break;

                case PriceOption.fiat:
                    currencyRates = NerkhAPIService.Create.GetCurrency(); //get fiat currency rates  
                    text = CurrencyService.BuildStringForChannel(rates : currencyRates);
                    break;

                case PriceOption.cryptoCurrency:
                    cryptoList =  CoinMarketAPISerivce.GetTopCurrency();
                    text = CurrencyService.BuildStringForChannel(currencyModel : cryptoList);

                    break;
                case PriceOption.full:
                    goldRates = NerkhAPIService.Create.GetGold(); //get gold rates    
                    currencyRates = NerkhAPIService.Create.GetCurrency(); //get fiat currency rates       
                    cryptoList =  CoinMarketAPISerivce.GetTopCurrency();
                    text = CurrencyService.BuildStringForChannel(cryptoList.Take(10), currencyRates.Take(12), goldRates);
                    break;
                default:
                    break;
            }
            return await TelegramService.SendMessage(chatId, text, ChatAction.Typing, parseMode: ParseMode.Default); //return a message type 
        }

        public async static Task<Message> SendHelpMessage(long chatId, int replyToMessage = 0)
        {
            string text = Resources.Fa_IR.HelpMessage;
            return await TelegramService.SendMessage(chatId, text, replyToMessage, defaultKeyboard: true);
        }
        public async static Task<Message> NoTask(long chatId, int replyToMessage)
        {
            return await TelegramService.SendMessage(chatId, Resources.Fa_IR.deactiveMessage, replyToMessage);
        }
        public async static Task<Message> Fiat(long chatId, int replyToMessage)
        {
            //var usd = await TgjuAPIService.GetUSDPrice(); //get price
            var usd = NerkhAPIService.Create.GetCurrencyBySymbol(CurrencyType.USD_EX).current;
            var text = string.Format(Resources.Fa_IR.dollarMessage, usd);
            return await TelegramService.SendMessage(chatId, text, replyToMessage);
        }
        public async static Task<Message> Affiliate(long chatId)
        {
            var user = UserService.GetUser(chatId); //get user by chatId 
            if (user != null) //if user exist 
            {
                var text = Resources.Fa_IR.AffiliateMessage;
                text += "\n\n https://t.me/nabzearz_bot?start=" + user.affiliate_id;
                //forceUpdate
                return await TelegramService.SendMessage(chatId, text, inlineButtons: new InlineKeyboardButton[][] { new InlineKeyboardButton[] { InlineKeyboardButton.WithSwitchInlineQuery(Resources.Fa_IR.affiliateText, "affiliate") } });
            }
            return null;
        }
        public async static Task<Message> ShowUserPoint(long chatId, int replyToMessage)
        {
            var sumPoints = PointService.GetSumUserPoint(chatId);
            var process = CalculateProcess.makeProcess(sumPoints);
            if (process.Item2 >= 100)
            {
                //forceUpdate
                var ineline = new InlineKeyboardButton[][] { new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("دوج کوین خود را دریافت کنید", "reuestForGetDogeCoin") } };
                return await TelegramService.SendMessage(chatId, $"تبریک شما موفق به کسب دوج کوین های خود شده اید \n\n " + $"🔴 امتیاز شما : {sumPoints} از 1000 امتیاز \n {process.Item1} {process.Item2}% ", ineline);
            }
            else
            {
                return await TelegramService.SendMessage(chatId, $"🔴 امتیاز شما : {sumPoints} از 1000 امتیاز \n {process.Item1} {process.Item2}% ");
            }
        }
        public async static Task<Message> ShouldBejoinInChanell(long chatId)
        {
            //forceUpdate
            return await TelegramService.SendMessage(chatId, "برای استفاده از خدمات بات باید در کانال ما عضو شوید \n\n عضویت در کانال نبض ارز 👇👇 \n  🏆 @nabzearz ", new InlineKeyboardButton[][] { new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(Resources.Fa_IR.tryagain, "retry") } });
        }
        public async static Task<Message> SendWelcomeMessage(long chatId)
        {
            string message = "ثبت نام شما با موفقیت انجام شد و از طرف #نبض_ارز ورود شما را خوش آمد میگوییم \n\n" +
                 $"شما بابت ثبت نام {(int)TypePoint.FirstRegister} امتیاز کسب کردید \n\n 🔴 امتیاز شما :{PointService.GetSumUserPoint(chatId)}";

            return await TelegramService.SendMessage(chatId, message);

        }

        public async static Task<Message> SendDonatMessage(long chatId, int replyToMessage)
        {
            return await TelegramService.SendMessage(chatId, "ما را میتوانید از روش های زیر حمایت کنید ❤️  \n *ETH* \n ```0xbfa03bf4b4e1db7e6d5286265a3774e7bc33a95f``` \n *Doge* \n ```DBqqkDHanqE6FEgtf8jTvHBvxBRbVa19Y2``` ", replyToMessage, parseMode: ParseMode.Markdown);
        }

        public async static Task<Message> CheckCommand(Message message)
        {
            var messageId = message.MessageId; //get messageId
            var chatId = message.Chat.Id; //get ChatId
            switch (message.Text)
            {
                case "/help":
                case "راهنما":
                case "راهنما 💬":
                case "راهنما ❓":
                    message = await SendHelpMessage(chatId, messageId);
                    TrackingService.Add(chatId, EnumOperation.helpRequest.ToString(), requestType.message);
                    break;
                case "/list":
                case "قیمت لحظه‌ای":
                    KeyboardButton[][] keyboards = new KeyboardButton[][]
                    {
                        new KeyboardButton[]{ "ارز دیجیتال", "فیات" , "سکه و طلا"} ,
                        new KeyboardButton[]{ "بازگشت به منو اصلی" }
                    };
                    await TelegramService.SendMessage(chatId, "یکی از موارد منو را انتخاب کنید", keyboards);
                    break;
                case "/news":
                case "اخبار":
                    message = await NoTask(chatId, messageId);
                    TrackingService.Add(chatId, EnumOperation.newsRequest.ToString(), requestType.message);
                    break;
                case "/chartbtc":
                case "نمودار بیت کوین":
                    message = await ChartService.SendChart(chatId);
                    TrackingService.Add(chatId, EnumOperation.BTC_ChartRequest.ToString(), requestType.message);
                    break;
                case "/dogecoin":
                case "معرفی به دوستان":
                    message = await Affiliate(chatId);
                    TrackingService.Add(chatId, EnumOperation.AffiliateLinkRequest.ToString(), requestType.message);
                    break;
                case "/setting":
                case "تنظیمات":
                case "تنظیمات ⚙️":
                    message = await SettingOperation(chatId);
                    TrackingService.Add(chatId, EnumOperation.settingRequest.ToString(), requestType.message);
                    break;
                case "امتیاز":
                case "/point":
                case "امتیاز شما 🎖":
                    message = await ShowUserPoint(chatId, messageId);
                    TrackingService.Add(chatId, EnumOperation.show_pointsRequest.ToString(), requestType.message);
                    break;
                case "/donate":
                case "حمایت کنید 💖":
                    message = await SendDonatMessage(chatId, messageId);
                    TrackingService.Add(chatId, EnumOperation.donateRequest.ToString(), requestType.message);
                    break;
                case "/contact":
                case "تماس":
                    await ProcessContactUs(chatId);
                    TrackingService.Add(chatId, EnumOperation.contactus.ToString(), requestType.message);
                    break;
                case "دارایی من":
                    await ProcessMyPossension(message); //process my possension 
                    TrackingService.Add(chatId, EnumOperation.mypossension.ToString(), requestType.message);
                    break;
                case "خدمات 🔧":
                    await ProcessService(message);
                    TrackingService.Add(chatId, EnumOperation.services.ToString(), requestType.message);
                    break;
                case "بازگشت به منو اصلی":
                    //send message that have navigated to main menu
                    await TelegramService.SendMessage(chatId, "شما به منوی اصلی منتقل شدید", message.MessageId, defaultKeyboard: true);
                    //add to tracking list
                    TrackingService.Add(chatId, EnumOperation.backToMainMenue.ToString(), requestType.message);
                    break;
                case "ارزهای فیات":
                    //send fiat list
                    await ProcessFiatCurrency(message);
                    //add to tracking list
                    TrackingService.Add(chatId, EnumOperation.fiatProcess.ToString(), requestType.message);
                    break;
                case "سکه و طلا":
                    message = await SendPrice(chatId, PriceOption.gold);
                    TrackingService.Add(chatId, EnumOperation.showGoldPRice.ToString(), requestType.message);
                    break;
                case "فیات":
                    message = await SendPrice(chatId, PriceOption.fiat);
                    TrackingService.Add(chatId, EnumOperation.showFiatPRice.ToString(), requestType.message);
                    break;
                case "ارز دیجیتال":
                    message = await SendPrice(chatId, PriceOption.cryptoCurrency);
                    TrackingService.Add(chatId, EnumOperation.showCryptoPrice.ToString(), requestType.message);
                    break;
                default:
                    //searching in database if true mean user search a price else send
                    await TelegramService.SendChatAction(chatId, ChatAction.Typing);
                    var searchResult = await SearchService.SearchOperation(chatId: chatId, query: message.Text);
                    if (!searchResult)
                    {
                        message = await TelegramService.SendMessage(message.Chat.Id, "شرمنده چیزی پیدا نکردم !! \n میتونی به من اسم فارسی یا انگلیسی و یا حتی نماد ارز مورد نظر را  بفرستی تا من برات قیمت اش رو در بیارم \n برای راهنمایی میتوانید /help را بزنید", message.MessageId);
                        TrackingService.Add(chatId, EnumOperation.NoResultRequest.ToString(), requestType.message);
                    }
                    break;
            }
            return message;
        }
        public async static Task<Message> checkOperation(Message message, EnumOperation operation)
        {
            var chatId = message.Chat.Id; //getChatId
            var messageId = message.MessageId; //getMessageId
            switch (operation)
            {
                case EnumOperation.addEmail:
                    await AddEmailProcess(message); //add email process
                    break;
                case EnumOperation.addPhone:
                    await AddPhoneProcess(message);
                    break;
                case EnumOperation.addDogeCoin:
                    await AddDogeCoinProcess(message);
                    break;
                case EnumOperation.posession_add_ETH: //shall be automatic
                    //add ethreium value to database and send message to user
                    await addPosenssion(message, EnumOperation.posession_add_ETH);
                    break;
                case EnumOperation.posession_add_BTC: //shall be automatic
                    await addPosenssion(message, EnumOperation.posession_add_BTC);
                    break;
                default:
                    UserService.ClearOperation(chatId);
                    break;
            }
            return null;
        }
        public async static Task ProccessMessage(Message message)
        {
            if (UserService.IsUserRegistered(message.Chat.Id)) //check user is register or not
            {
                var hasOperation = UserService.HasOperation(message.Chat.Id); //check user has operation or not
                if (hasOperation.Item1)
                {
                    try
                    {
                        await checkOperation(message, hasOperation.Item2);
                    }
                    catch (Exception e)
                    {
                        UserService.ClearOperation(message.Chat.Id);
                        throw new Exception("", e.InnerException);
                    }
                }
                else
                {
                    await CheckCommand(message); //prepare command
                }
            }
            else
            {
                RegisterProcess(message); //register section 
            }
            #region comment
            //await sendAction(message.Chat.Id); //send action to user
            //var hasRegisterAndIsMember = await UserService.HasRegisterAndIsMember(message.Chat.Id);
            //if (hasRegisterAndIsMember.Item1 && hasRegisterAndIsMember.Item2) //if user has been registered and join in channel
            //{
            //    await CheckCommand(message); //prepare command
            //}
            //else if (hasRegisterAndIsMember.Item1 && !hasRegisterAndIsMember.Item2) //if user has been registered but leave the channel
            //{
            //    await ShouldBejoinInChanell(message.Chat.Id);
            //}
            //else //user has'nt registered
            //{
            //    RegisterProcess(message); //register section 
            //}
            #endregion
        }

        public static async Task sendAction(long chatId)
        {
            await TelegramService.SendChatAction(chatId, ChatAction.Typing);
        }

        public async static Task ProcessCallBack(CallbackQuery callback)
        {
            if (callback.Data.Contains("posession_add"))
            {
                //get which currency ? 
                var symbol = callback.Data.Split('_').Last();
                //send message that send value to us 
                await TelegramService.SendMessage(callback.From.Id, $"لطفا مقدار {symbol} خود را وارد کنید");
                //add operation 
                UserService.AddOperaion(callback.From.Id, callback.Data);
            }
            else if (callback.Data.Contains("Price_"))
            {
                //get symbol
                var symbol = callback.Data.Split('_').Last();
                //search in sysmbol

            }
            else if (callback.Data.Contains("fiat_"))
            {
                var symbol = callback.Data.Split('_').Skip(1).First();
                var pageNo = Convert.ToInt32(callback.Data.Split('_').Last());
                await ProcessUSDPrice(callback, symbol, pageNo: pageNo);
            }
            else if (callback.Data.Contains("currency_page"))
            {
                int pageNo = int.Parse(callback.Data.Split('_').Last()); //PageNo
                //var totall = currencies.Count;

                await ProcessFiatCurrency(callback, pageNo: pageNo);


            }
            else
            {
                switch (callback.Data)
                {
                    case "email":
                        await EmailProcess(callback);
                        break;
                    case "backToMain":
                        await mainSetting(callback);
                        break;
                    case "editEmail":
                        await SendEmailMessage(callback);
                        break;
                    case "phone":
                        await PhoneProcess(callback);
                        break;
                    case "editPhone":
                        await TelegramService.RequestContact(callback.From.Id);
                        UserService.AddOperaion(callback.From.Id, EnumOperation.addPhone);
                        //edit phone
                        break;
                    case "dogeCoinWallet":
                        await DogeCoinWallet(callback); //show dogeCoin page
                        break;
                    case "editDogeCoinWallet":
                        //send message to get dogeCoin 
                        await SendDogeCoinWalletMessage(callback);
                        //set addDogeCoinWallet to operation 
                        break;
                    case "digitalPossention":
                        //show possention list button
                        await ShowPossension(callback);
                        //edit possention
                        break;
                    case "reuestForGetDogeCoin":
                        //add ticket for GetDogeCoin
                        await TicketService.AddDogeCoinTicket(callback);
                        break;
                    case "HelpPossention":
                        await ProcessHelpCallback(callback, "دارایی من", "به صورت زیر می باشد دارایی من");
                        break;
                    case "contactUs":
                        await ProcessContactUs(callback.From.Id , true , callback.Id);
                        break;
                    default:
                        break;
                }
            }

            //await TelegramService.UpdateMessage(callback.Message.From.Id , callback.Message.MessageId , inline);
            //switch (callback.Data)
            //{
            //    case "retry":
            //        if (await UserService.ChangeMemberIsInChannel(chatId)) //user has joined in channel 
            //        {
            //            await SendHelpMessage(chatId);  //send help message
            //        }
            //        else
            //        {
            //            await TelegramService.AnswerCallBackQuert(callback.Id, Resources.Fa_IR.sorry, showAlert: true);
            //        }
            //        break;
            //}
        }
        public async static Task<bool> EmailProcess(CallbackQuery callback)
        {
            var user = UserService.userInformation(callback.From.Id);
            string buttonText = string.IsNullOrEmpty(user.emailData) ? "افزودن" : "ویرایش";
            var inline = new InlineKeyboardButton[][] {
                        new InlineKeyboardButton[] {
                        InlineKeyboardButton.WithCallbackData(buttonText , "editEmail") ,
                        InlineKeyboardButton.WithCallbackData("بازگشت ⤶" , "backToMain") ,
                    }
            };
            await TelegramService.SendWaiting(callback);
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, $"تنظیمات -> ایمیل \n {user.email}", inline);
            TrackingService.Add(callback.From.Id, EnumOperation.settingEmailPageRequest.ToString(), requestType.CallbackQueryUpdate);
            return true;
        }

        public async static Task<bool> DogeCoinWallet(CallbackQuery callback)
        {
            var user = UserService.userInformation(callback.From.Id); //get user information
            string buttonText = string.IsNullOrEmpty(user.dogeCoinWalletData) ? "افزودن" : "ویرایش"; //button text 
            var inline = new InlineKeyboardButton[][] {
                            new InlineKeyboardButton[] {
                            InlineKeyboardButton.WithCallbackData(buttonText , "editDogeCoinWallet") ,
                            InlineKeyboardButton.WithCallbackData("بازگشت ⤶" , "backToMain") ,
                    }
            };
            await TelegramService.SendWaiting(callback);
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, $"تنظیمات -> کیف پول دوج کوین \n {user.walletDogcoin}", inline);
            TrackingService.Add(callback.From.Id, EnumOperation.settingDogecoinPageRequest.ToString(), requestType.CallbackQueryUpdate);
            return true;
        }

        public async static Task<bool> PhoneProcess(CallbackQuery callback)
        {
            var user = UserService.userInformation(callback.From.Id);
            string buttonText = string.IsNullOrEmpty(user.phoneData) ? "افزودن" : "ویرایش";
            var inline = new InlineKeyboardButton[][] {
                        new InlineKeyboardButton[] {
                        InlineKeyboardButton.WithCallbackData(buttonText , "editPhone") ,
                        InlineKeyboardButton.WithCallbackData("بازگشت ⤶" , "backToMain") ,
                    }
            };
            await TelegramService.SendWaiting(callback);
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, $"تنظیمات -> موبایل \n {user.phone}", inline);
            TrackingService.Add(callback.From.Id, EnumOperation.settingPhonePageRequest.ToString(), requestType.CallbackQueryUpdate);
            return true;
        }

        public async static Task ShowPossension(CallbackQuery callback)
        {
            var currencies = CurrencyService.GetCurrencies();
            var inlineKeyboards = new List<InlineKeyboardButton>();
            foreach (var currency in currencies)
            {
                var inline = InlineKeyboardButton.WithCallbackData(currency.persianName, $"posession_add_{currency.symbol}");
                inlineKeyboards.Add(inline);
            }

            InlineKeyboardButton[][] inlines = new InlineKeyboardButton[][]
            {
                inlineKeyboards.ToArray()
            };

            await TelegramService.SendWaiting(callback); //waiting for send message
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, "یکی از ارز های زیر را انتخاب کنید", inlines);

            TrackingService.Add(callback.From.Id, EnumOperation.showPossensionRequest.ToString(), requestType.CallbackQueryUpdate);
        }

        public async static Task<bool> SendEmailMessage(CallbackQuery callback)
        {
            await TelegramService.SendWaiting(callback);
            //add email operation to database    

            UserService.AddOperaion(callback.From.Id, EnumOperation.addEmail); //add operation 

            await TelegramService.SendMessage(callback.From.Id, "لطفا ایمیل خود را وارد کنید");
            TrackingService.Add(callback.From.Id, EnumOperation.waitForEmailRequest.ToString(), requestType.CallbackQueryUpdate);
            return true;
        }

        public async static Task<bool> SendDogeCoinWalletMessage(CallbackQuery callback)
        {
            await TelegramService.SendWaiting(callback);
            //add email operation to database    

            UserService.AddOperaion(callback.From.Id, EnumOperation.addDogeCoin); //add operation 
            await TelegramService.SendMessage(callback.From.Id, "آدرس کیف پول دوج کوین خود را وارد وارد کنید");
            TrackingService.Add(callback.From.Id, EnumOperation.waitForDogecoinWalletRequest.ToString(), requestType.CallbackQueryUpdate);
            return true;
        }

        public async static Task<bool> mainSetting(CallbackQuery callback)
        {
            var mainMenue = AssetService.MainMenue(callback.From.Id);
            await TelegramService.AnswerCallBackQuery(callback.Id, "بازگشت به منوی اصلی");
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, mainMenue.Item1, mainMenue.Item2);
            TrackingService.Add(callback.From.Id, EnumOperation.settingMainMenueRequest.ToString(), requestType.CallbackQueryUpdate);
            return true;
        }

        public async static void RegisterProcess(Message message)
        {
            var chat = message.Chat; //get chat object

            var affiliate_status = TelegramService.CheckAffiliateStatus(message.Text); //check affiliate status 
            if (affiliate_status.is_affiliate && affiliate_status.is_refer_true)
            {
                var result = UserService.CreateUser(message, affiliate_status.affiliate_chat_id); //create user in data base 

                if (result) //send welcome message to user
                {
                    PointService.AddPoint(affiliate_status.affiliate_chat_id, TypePoint.Affiliate); //add point to affiliate
                    PointService.AddPoint(chat.Id, TypePoint.FirstRegister); //add register point to user
                                                                             //var point = UserService.SumPoint(affiliate_status.affiliate_chat_id);
                                                                             //send message to affiliate include pont and notification
                    await SendWelcomeMessage(chat.Id);
                    await SendHelpMessage(chat.Id);

                    //if (await UserService.ChangeMemberIsInChannel(chat.Id)) //is member joined in channell except status "left" & "Kick"
                    //{
                    //}
                    //else //user have not join in bot
                    //{
                    //    await ShouldBejoinInChanell(chat.Id); //send message to user you must join in chanell
                    //}
                    ////process point of affiliate


                    //need sum point of affiliate 
                    await TelegramService.SendMessage(affiliate_status.affiliate_chat_id, $"کاربر {chat.FirstName} {chat.LastName} با لینک شما ثبت نام کرد \n\n 🔴 امتیاز شما :{PointService.GetSumUserPoint(affiliate_status.affiliate_chat_id)}");
                    TrackingService.Add(chat.Id, EnumOperation.registerWithAffiliateIdRequest.ToString(), requestType.message);
                }
            }
            else
            {
                var result = UserService.CreateUser(message); //create user in databasae 
                if (result && message.Text.StartsWith("/start"))
                {
                    PointService.AddPoint(chat.Id, TypePoint.FirstRegister); //add point
                    await SendWelcomeMessage(chat.Id); //send welcome message
                    await SendHelpMessage(chat.Id); //send help message to new user
                    TrackingService.Add(chat.Id, EnumOperation.registerNormal.ToString(), requestType.message);

                    #region comment
                    //if (await TelegramService.IsMemberInChannel(chat.Id)) //is member joined in channell except status "left" & "Kick"
                    //{
                    //    await SendHelpMessage(chat.Id);
                    //    //await TelegramService.SendMessage(chat.Id, $"شما بابت ثبت نام 50 امتیاز کسب کردید \n\n 🔴 امتیاز شما :{PointService.GetSumUserPoint(chat.Id)}");
                    //}
                    //else //user have not join in bot
                    //{
                    //    //send message to user you must join in chanell
                    //    await ShouldBejoinInChanell(chat.Id);
                    //}
                    #endregion
                }

            }

        }

        public static async Task<Message> SettingOperation(long chatID)
        {
            var menue = MainMenue(chatID);// item1 == caption , item2 == inlineButton , item3 == userInformation 
            return await TelegramService.SendMessage(chatID, menue.Item1, menue.Item2, parseMode: ParseMode.Markdown);
        }

        /// <summary>
        /// this methode use for main menue data 
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns>item1 = caption , item2 = inlineButton , item3 = userinforMation</returns>
        public static Tuple<string, InlineKeyboardButton[][], UserModel> MainMenue(long chatID)
        {
            var inline = new InlineKeyboardButton[][] {
                new InlineKeyboardButton[] {
                    InlineKeyboardButton.WithCallbackData("ایمیل" , "email") ,
                    InlineKeyboardButton.WithCallbackData("موبایل" , "phone") ,
                    //InlineKeyboardButton.WithCallbackData("دارایی دیجیتال" , "digitalPossention") ,
                } ,
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("کیف پول دوج کوین" , "dogeCoinWallet")  ,
                    InlineKeyboardButton.WithCallbackData("تماس با ما", "contactUs")
                }
            };

            //request to database and check phone , email , wallet , possention is null or inserted 
            var userInformation = UserService.userInformation(chatID);

            string caption = $"*تنظیمات*  \n\n" +
                             $"*ایمیل* : {userInformation.email}  \n" +
                             $"*موبایل* : {userInformation.phone}  \n" +
                             $"*آدرس کیف پول* : ``` {userInformation.walletDogcoin} ```";

            return new Tuple<string, InlineKeyboardButton[][], UserModel>(caption, inline, userInformation);
        }

        public async static Task AddEmailProcess(Message message)
        {
            if (ValidationService.IsValidEmail(message.Text)) //check email is valid ?
            {
                UserService.AddEmail(message.Chat.Id, message.Text); //add email to database
                UserService.ClearOperation(message.Chat.Id); //clear operation
                await SettingOperation(message.Chat.Id); //send setting 
                await UserService.CheckISCompleteRegister(message.Chat.Id);
                //add tracking to database
                TrackingService.Add(message.Chat.Id, EnumOperation.settingAddEmailRequest.ToString(), requestType.message);
                return;
            }
            await TelegramService.SendMessage(message.From.Id, "ایمیل صحیح نمی باشد لطفا دوباره سعی کنید");
            TrackingService.Add(message.Chat.Id, EnumOperation.settingEmailValidationErrorRequest.ToString(), requestType.message); //add to tracking
        }

        public async static Task AddPhoneProcess(Message message)
        {
            if (message.Contact != null)
            {
                UserService.AddPhone(message.Chat.Id, message.Contact.PhoneNumber); //add contact to database
            }
            else
            {
                if (ValidationService.IsValidNumber(message.Text))
                {
                    UserService.AddPhone(message.Chat.Id, message.Text); //add contact to database
                }
                else
                {
                    await TelegramService.SendMessage(message.From.Id, "شماره موبایل معتبر نمی باشد");
                    return;
                }
            }
            UserService.ClearOperation(message.Chat.Id); //clear operation
            await TelegramService.SendMessage(message.Chat.Id, "شماره موبایل با موفقیت ذخیره شد", message.MessageId, defaultKeyboard: true);
            await SettingOperation(message.Chat.Id);
            await UserService.CheckISCompleteRegister(message.Chat.Id);
            TrackingService.Add(message.Chat.Id, EnumOperation.settingAddPhoneRequest.ToString(), requestType.message);
        }

        public async static Task AddDogeCoinProcess(Message message)
        {
            UserService.AddDogeCoinAddress(message); //add doge coin
            await TelegramService.SendMessage(message.Chat.Id, "آدرس دوج کوین با موفقیت ایجاد شد");
            await SettingOperation(message.Chat.Id);
            UserService.ClearOperation(message.Chat.Id); //clear opertation
            TrackingService.Add(message.Chat.Id, EnumOperation.settingDogecoinPageRequest.ToString(), requestType: requestType.message);
        }

        public async static Task addPosenssion(Message message, EnumOperation operation)
        {
            //get symbol
            string symbol = (operation.ToString()).Split('_').LastOrDefault();
            var currency = CurrencyService.GetCurrency(symbol);

            var user = UserService.GetUser(message.From.Id);

            var possension = new possensionModel
            {
                amount = Convert.ToDouble(message.Text),
                currency_id = currency.id,
                user_id = user.Id,
            };
            //add to database 
            PossensionService.Add(possension);
            //send message to user
            await TelegramService.SendMessage(message.From.Id, "با موفقیت ذخیره شد ");
            //clear operation
            UserService.ClearOperation(message.From.Id);
        }

        public async static Task ProcessMyPossension(Message message)
        {
            //get my possension 
            var posensions = PossensionService.GetMyPossension(message.Chat.Id);
            //generate report 
            var Message = PossensionService.TemplateForMessage(posensions);
            //send message to user 
            InlineKeyboardButton[][] inlines = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("❓" , "HelpPossention") , InlineKeyboardButton.WithCallbackData("افزودن / ویرایش", "digitalPossention")}
            };
            await TelegramService.SendMessage(message.Chat.Id, Message.Item2, inlines);
            //add log to database 
            TrackingService.Add(message.Chat.Id, EnumOperation.showPossensionRequest.ToString(), requestType: requestType.message);
        }

        public async static Task ProcessService(Message message)
        {
            var rkb = new KeyboardButton[][]
            {
                                    new KeyboardButton[]{ "ارزهای فیات" , "نمودار بیت کوین", "قیمت لحظه‌ای" } ,
                                    new KeyboardButton[]{ "بازگشت به منو اصلی" }
            };
            await TelegramService.SendMessage(message.Chat.Id, "یکی از خدمات زیر را انتخاب کنید", rkb);
        }

        public async static Task ProcessHelpCallback(CallbackQuery callback, string title, string description)
        {
            await TelegramService.AnswerCallBackQuery(callback.Id, "راهنمای 👇" + title);
            await TelegramService.SendMessage(callback.From.Id, description);
        }
        public async static Task ProcessContactUs(long chatId , bool isCallback = false , string callBackqueryId = null)
        {
            if (isCallback)
            {
                await TelegramService.AnswerCallBackQuery(callBackqueryId);
            }
            await TelegramService.SendContact(chatId);
            TrackingService.Add(chatId, EnumOperation.sendContactRequest.ToString(), requestType.message);
        }

        public async static Task ProcessFiatCurrency(Message message, int pageNo = 1, int itemPerPage = 9)
        {
            int offset = (pageNo - 1) * itemPerPage; //make an offset

            var currencies = NerkhAPIService.Create.GetCurrency().Select(c => c.symbol).ToList().Skip(offset).Take(itemPerPage); //get all symbol of currencies

            var inline = CreateFiatButtons(currencies.ToArray(), pageNo);
            await TelegramService.SendMessage(message.Chat.Id, "یکی از ارزهای زیر را انتخاب کنید", inline);
        }

        public async static Task ProcessFiatCurrency(CallbackQuery callback, int pageNo = 1, int itemPerPage = 9)
        {
            int offset = (pageNo - 1) * itemPerPage;

            var currencies = NerkhAPIService.Create.GetCurrency().Select(c => c.symbol).ToList().Skip(offset).Take(itemPerPage); //get all symbol of currencies

            var inline = CreateFiatButtons(currencies.ToArray(), pageNo);
            await TelegramService.SendWaiting(callback);
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, "یکی از ارزهای زیر را انتخاب کنید", inline);
        }
        public static InlineKeyboardButton[][] CurrencyButtons()
        {
            //English name , persian name , symbol , type
            //Erou , یورو , EURO  , currency
            //Bitcoin , بیت کوین , BTC , cryptoCurrency

            var inline = new InlineKeyboardButton[][]{
                new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("دلار" , "Price_USD") ,
                InlineKeyboardButton.WithCallbackData("یورو" , "Price_EURO")
                }
            };
            return inline;
        }
        public async static Task ProcessUSDPrice(CallbackQuery callback, string symbol, int pageNo = 1, int itemPerPage = 9)
        {
            int offset = (pageNo - 1) * itemPerPage;
            CurrencyType currencyType;
            Enum.TryParse(symbol, out currencyType);
            var price = NerkhAPIService.Create.GetCurrencyBySymbol(currencyType);
            //var persianName = PersianHelper.GetPersianName(symbol);
            var currencies = NerkhAPIService.Create.GetCurrency().Select(c => c.symbol).ToList().Skip(offset).Take(itemPerPage); //get all symbol of currencies

            var inline = CreateFiatButtons(currencies.ToArray(), pageNo);
            var message = $"قیمت هر {price.fa_name} {price.current_Toman}  تومان می باشد \n\n⬆️ بالاترین قیمت  {price.max_Toman}\n⬇️ پایین ترین قیمت  {price.min_Toman} \n ";
            await TelegramService.SendWaiting(callback);
            await TelegramService.updateMessageCaption(callback.From.Id, callback.Message.MessageId, message, inline);
        }

        public static InlineKeyboardButton[][] CreateFiatButtons(string[] values, int pageNo)
        {
            //var currencies = NerkhAPIService.Create.GetCurrency().Select(c => c.symbol).ToArray(); //get all symbol of currencies

            int columnCount = 3;
            int rowCounter = 0;
            int rowCount = (values.Count() / columnCount);
            int currencyCounter = 0;
            List<InlineKeyboardButton[]> inlineKeyboardButton = new List<InlineKeyboardButton[]>();
            List<InlineKeyboardButton[]> inlineButton = new List<InlineKeyboardButton[]>();
            List<InlineKeyboardButton> inline = new List<InlineKeyboardButton>();

            for (int i = rowCounter; i < rowCount; i++)//row counter
            {
                for (int j = 0; j < columnCount; j++)//column counter
                {
                    //make inlineKeyboard
                    var button = InlineKeyboardButton.WithCallbackData($"{PersianHelper.GetPersianName(values[currencyCounter])}", $"fiat_{values[currencyCounter]}_{pageNo}");
                    //add inlinekeyboard to list<inlineKeyboard> 
                    inline.Add(button);
                    currencyCounter++;
                }
                //add list<inlineKeyboard> to list<inlinekeybord[]>
                inlineButton.Add(inline.ToArray());
                inline.Clear();
                rowCounter++;
            }
            if (pageNo <= 1)
            {
                inline.Add(InlineKeyboardButton.WithCallbackData($"<< بعدی ", $"currency_page_{pageNo + 1}"));
            }
            else
            {
                inline.Add(InlineKeyboardButton.WithCallbackData($"قبلی >>", $"currency_page_{pageNo - 1}"));
                if (pageNo != 4)
                {
                    inline.Add(InlineKeyboardButton.WithCallbackData($"<< بعدی ", $"currency_page_{pageNo + 1}"));
                }
            }

            inlineButton.Add(inline.ToArray());
            inlineKeyboardButton.AddRange(inlineButton.ToArray());
            var final = inlineKeyboardButton.ToArray();
            return final;
        }
    }
}