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
    public class UserService
    {
        #region filed section 
        protected static ApplicationDbContext db = GetDb();
        private const int ponit = 50;

        #endregion

        /// <summary>
        /// get database context
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext GetDb()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// get user witch chat id 
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public static UserModel GetUser(long chatId)
        {
            try
            {
                var user = db.UsersBot.Where(t => t.chatId == chatId).FirstOrDefault();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static UserModel GetUser(int userId)
        {
            try
            {
                var user = db.UsersBot.Where(t => t.Id == userId).FirstOrDefault();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<UserModel> GetUsers()
        {
            try
            {
                var user = db.UsersBot.ToList();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool IsUserRegistered(long chatId)
        {
            if (GetUser(chatId) != null)
            {
                return true;
            }
            return false;
        }

        public static bool CreateUser(Message message, long refer_chat_id = 0)
        {
            try
            {
                var user = new UserModel()
                {
                    name = message.Chat.FirstName,
                    family = message.Chat.LastName,
                    chatId = message.Chat.Id,
                    userName = message.Chat.Username,
                    Date = DateTime.Now,
                    operation = null,
                    affiliate_id = "Invite_" + message.Chat.Id,
                    point = 0,
                    refer_chat_id = refer_chat_id,
                    last_activity = DateTime.Now
                };
                db.UsersBot.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error from : UserService/CreateUser", e.InnerException);
            }
        }
        public static bool IsReferTrue(long affiliate_chatId, string affiliate_inviteId)
        {
            var user = UserService.GetUser(affiliate_chatId);
            if (user != null && user.chatId == affiliate_chatId && user.affiliate_id == affiliate_inviteId)
                return true;

            return false;
        }
        public static int SumPoint(long chatId)
        {
            try
            {
                var user = GetUser(chatId);
                if (user != null)
                {
                    user.point += ponit;
                    db.SaveChanges();
                    return user.point;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception("Error form : UserService/SumPoint ", e.InnerException);
            }

        }

        public static void ChangeMessageId(Message message)
        {
            try
            {
                var channel = db.Chanel.FirstOrDefault(); //get chanel entity
                if (channel != null) //entity is exist
                {
                    channel.last_channel_message = message.MessageId; //update messageId
                    db.SaveChanges(); //save to database
                }
                else
                {
                    db.Chanel.Add(new ChannelModel
                    {
                        chat_id = message.Chat.Id,
                        last_channel_message = message.MessageId,
                        user_name = message.Chat.Username
                    });
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error from : UserService/ChangeMessageId", e.InnerException);
            }
        }


        public static int GetMessageId()
        {
            try
            {
                var channel = db.Chanel.FirstOrDefault();
                return channel.last_channel_message;
            }
            catch (Exception e)
            {
                throw new Exception("Error from : UserService/ChangeMessageId", e.InnerException);
            }
        }
        public async static Task<bool> ChangeMemberIsInChannel(long chatId)
        {
            try
            {
                var user = GetUser(chatId); //get user
                if (user != null)
                {
                    user.is_join_channel = await TelegramService.IsMemberInChannel(chatId); //is member in channel
                    db.SaveChanges(); //save in database
                    return user.is_join_channel;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception("Error form UserService/ChangeMemberIsInChannel", e.InnerException);
            }

        }
        public async static Task<Tuple<bool, bool>> HasRegisterAndIsMember(long chatId)
        {
            try
            {
                var user = GetUser(chatId); //get user
                if (user != null && user.is_join_channel) //user signup and joined in channel
                {
                    return new Tuple<bool, bool>(true, true);
                }
                else if (user != null && !user.is_join_channel) //user signup but hadn't join channel
                {
                    return new Tuple<bool, bool>(true, await ChangeMemberIsInChannel(chatId)); //check user if joined save in db and return true 
                }
                else
                {
                    return new Tuple<bool, bool>(false, false);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error form UserService/ChangeMemberIsInChannel", e.InnerException);
            }

        }

        public static UserModel userInformation(long chatId)
        {
            var user = GetUser(chatId);

            return new UserModel
            {
                email = (string.IsNullOrEmpty(user.email)) ? "ثبت نشده" : $"{user.email}",
                phone = (string.IsNullOrEmpty(user.phone)) ? "ثبت نشده" : $"{user.phone}",
                walletDogcoin = (string.IsNullOrEmpty(user.walletDogcoin)) ? "ثبت نشده" : $"{user.walletDogcoin}",
                emailData = user.email,
                phoneData = user.phone,
                dogeCoinWalletData = user.dogeCoinWalletData
            };
        }

        public static int AddOperaion(long chatId, EnumOperation operation)
        {
            try
            {
                var user = GetUser(chatId);
                user.operation = operation.ToString();
                var result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {

                throw new Exception("Error From UserService/AddOperaion" + e.Message);
            }
        }
        public static int AddOperaion(long chatId, string operation)
        {
            try
            {
                var user = GetUser(chatId);
                user.operation = operation;
                var result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {

                throw new Exception("Error From UserService/AddOperaion" + e.Message);
            }
        }
        public static Tuple<bool, EnumOperation> HasOperation(long chatId)
        {
            try
            {
                var user = GetUser(chatId); //get user
                bool isOperation = !string.IsNullOrEmpty(user.operation);
                EnumOperation operation;
                Enum.TryParse<EnumOperation>(user.operation, out operation);
                return new Tuple<bool, EnumOperation>(isOperation, operation);
            }
            catch (Exception e)
            {
                throw new Exception("Error From UserService/IsOperation" + e.Message);
            }
        }
        public static int AddEmail(long chatId, string email)
        {
            try
            {
                var user = GetUser(chatId);
                user.email = email;
                return db.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception("Error From UserService/AddEmail" + e.Message);
            }
        }
        public static int ClearOperation(long chatId)
        {
            try
            {
                var user = GetUser(chatId);
                user.operation = null;
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error From UserService/ClearOperation" + e.Message);
            }

        }
        public static int AddPhone(long chatId, string phoneNumber)
        {
            try
            {
                var user = GetUser(chatId);
                user.phone = phoneNumber;
                return db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error from : UserService/AddPhone" + e.Message);
            }
        }

        public static int AddDogeCoinAddress(Message message)
        {
            try
            {
                var user = GetUser(message.Chat.Id);
                user.walletDogcoin = message.Text;
                return db.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception("Error From : UserService/AddDogeCoinAddress" + e.Message);
            }
        }

        public async static Task<bool> CheckISCompleteRegister(long chatId)
        {
            var user = GetUser(chatId);
            if (!user.isCompleteRegister)
            {
                if (!string.IsNullOrEmpty(user.phone) && !string.IsNullOrEmpty(user.email))
                {
                    PointService.AddPoint(chatId, TypePoint.CompleteProfile);
                    user.isCompleteRegister = true;
                    db.SaveChanges();
                    await TelegramService.SendMessage(chatId, $"شما {(int)TypePoint.CompleteProfile} امتیاز بابت تکمیل ثبت نام کسب کردید");
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public async static Task SendMessageToAll(string text)
        {
            try
            {
                //get users call with ToList() methode
                var SendMessageTask = GetUsers().Select(async u =>
                {
                    await TelegramService.SendMessage(u.chatId, text, defaultKeyboard: true);
                }).ToList();

                //wait for all task are finished
                await Task.WhenAll(SendMessageTask); 
            }
            catch (Exception e)
            {
                throw new Exception("Error from SendMessageToAll" + e.Message, e.InnerException);
            }
        }
    }
}