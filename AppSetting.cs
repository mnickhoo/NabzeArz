using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace NabzeArz
{
    public static class AppSetting
    {
        public static string tgju_file { get { return WebConfigurationManager.AppSettings["TgjuJson"]; } }
        public static string coinmarket_file { get { return WebConfigurationManager.AppSettings["CoinMarketJson"]; } }
        public static string coinmarket_api { get { return WebConfigurationManager.AppSettings["CoinMarketApiAddress"]; } }
        public static string tgju_api { get { return WebConfigurationManager.AppSettings["TgjuApiAddress"]; } }
        public static string telegram_token { get { return WebConfigurationManager.AppSettings["TelegramToken"]; } }
        public static string TelegramTokenTest { get { return "329340472:AAEkV-hC-_z3gcjHDaESwACMtWEymvBE93M"; } }
        public static string channel_chatId { get { return WebConfigurationManager.AppSettings["channelChatId"]; } }
        public static string channel_chatId_test { get { return WebConfigurationManager.AppSettings["channelChatIdTest"]; } }
    }
}