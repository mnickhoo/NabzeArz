using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using NabzeArz.Models;

namespace NabzeArz.Helper
{
    public class PersianHelper
    {
        public static string GetPersianName(string currencyName)
        {
            switch (currencyName)
            {
                case "Bitcoin":
                    return Resources.Fa_IR.Bitcoin;
                case "Ethereum":
                    return Resources.Fa_IR.Ethereum;
                case "Ethereum classic":
                    return Resources.Fa_IR.EthereumClassic;
                case "Z-cash":
                    return Resources.Fa_IR.Zcash;
                case "Digital cash":
                    return Resources.Fa_IR.DigitalCash;
                case "Dogecoin":
                    return Resources.Fa_IR.Dogecoin;
                case "Litecoin":
                    return Resources.Fa_IR.Litecoin;
                case "Factom":
                    return Resources.Fa_IR.Factom;
                case "Ripple":
                    return Resources.Fa_IR.Ripple;
                case "Monero":
                    return Resources.Fa_IR.Monero;
                case "Bitcoin Cash":
                    return Resources.Fa_IR.BitcoinCash;
                case "Cardano":
                    return Resources.Fa_IR.Cardano;
                case "Stellar":
                    return Resources.Fa_IR.Stellar;
                case "NEM":
                    return Resources.Fa_IR.NEM;
                case "IOTA":
                    return Resources.Fa_IR.IOTA;
                case "Dash":
                    return Resources.Fa_IR.Dash;
                case "NEO":
                    return Resources.Fa_IR.NEO;
                case "TRON":
                    return Resources.Fa_IR.TRON;
                case "EOS":
                    return Resources.Fa_IR.EOS;
                case "Bitcoin Gold":
                    return Resources.Fa_IR.BitcoinGold;
                case "ICON":
                    return Resources.Fa_IR.ICON;
                case "Qtum":
                    return Resources.Fa_IR.QTUM;
                case "GameCredits":
                    return Resources.Fa_IR.gamecredits;
                case "TenX":
                    return Resources.Fa_IR.tenx;
                case "XPlay":
                    return Resources.Fa_IR.xplay;
                case "Bitcore":
                    return Resources.Fa_IR.bitcore;
                case "MediBloc":
                    return Resources.Fa_IR.medibloc;
                case "Quantstamp":
                    return Resources.Fa_IR.Quantstamp;
                case "Substratum":
                    return Resources.Fa_IR.substratum;
                case "Bancor":
                    return Resources.Fa_IR.bancor;
                case "Experience Points":
                    return Resources.Fa_IR.ExperiencePoints;
                case "Cryptonex":
                    return Resources.Fa_IR.cryptonex;
                case "Neblio":
                    return Resources.Fa_IR.neblio;
                case "ChainLink":
                    return Resources.Fa_IR.chainlink;
                case "Emercoin":
                    return Resources.Fa_IR.emercoin;
                case "ZCoin":
                    return Resources.Fa_IR.ZCoin;
                case "SIRIN LABS Token":
                    return Resources.Fa_IR.SIRINLABSToken;
                case "Enigma":
                    return Resources.Fa_IR.Enigma;
                case "Request Network":
                    return Resources.Fa_IR.RequestNetwork;
                case "Nexus":
                    return Resources.Fa_IR.nexus;
                case "gxshares":
                    return Resources.Fa_IR.gxshares;
                case "reddcoin":
                    return Resources.Fa_IR.reddcoin;
                case "ZClassic":
                    return Resources.Fa_IR.zclassic;
                case "MaidSafeCoin":
                    return Resources.Fa_IR.MaidSafeCoin;
                case "Nebulas":
                    return Resources.Fa_IR.Nebulas;
                case "Nxt":
                    return Resources.Fa_IR.nxt;
                case "syscoin":
                    return Resources.Fa_IR.syscoin;
                case "Kin":
                    return Resources.Fa_IR.kin;
                case "Bytom":
                    return Resources.Fa_IR.bytom;
                case "Aeternity":
                    return Resources.Fa_IR.aeternity;
                case "Power Ledge":
                    return Resources.Fa_IR.PowerLedger;
                case "DigixDAO":
                    return Resources.Fa_IR.digixdao;
                case "Dent":
                    return Resources.Fa_IR.dent;
                case "aelf":
                    return Resources.Fa_IR.aelf;
                case "MonaCoin":
                    return Resources.Fa_IR.monacoin;
                case "Aion":
                    return Resources.Fa_IR.aion;
                case "Cindicator":
                    return Resources.Fa_IR.cindicator;
                case "SmartCash":
                    return Resources.Fa_IR.smartcash;
                case "Byteball Bytes":
                    return Resources.Fa_IR.byteball;
                case "FunFair":
                    return Resources.Fa_IR.funfair;
                case "Ethos":
                    return Resources.Fa_IR.ethos;
                case "Gas":
                    return Resources.Fa_IR.gas;
                case "RChain":
                    return Resources.Fa_IR.rchain;
                case "Kyber Network":
                    return Resources.Fa_IR.KyberNetwork;
                case "WAX":
                    return Resources.Fa_IR.wax;
                case "Hshare":
                    return Resources.Fa_IR.hshare;
                case "PIVX":
                    return Resources.Fa_IR.pivx;
                case "Golem":
                    return Resources.Fa_IR.Golem;
                case "Basic Attention Token":
                    return Resources.Fa_IR.BasicAttentionToken;
                case "DigiByte":
                    return Resources.Fa_IR.digibyte;
                case "QASH":
                    return Resources.Fa_IR.qash;
                case "SALT":
                    return Resources.Fa_IR.salt;
                case "Ark":
                    return Resources.Fa_IR.ark;
                case "Loopring":
                    return Resources.Fa_IR.loopring;
                case "Dentacoin":
                    return Resources.Fa_IR.dentacoin;
                case "Dragonchain":
                    return Resources.Fa_IR.dragonchain;
                case "Walton":
                    return Resources.Fa_IR.walton;
                case "Decred":
                    return Resources.Fa_IR.decred;
                case "Komodo":
                    return Resources.Fa_IR.komodo;
                case "Electroneum":
                    return Resources.Fa_IR.electroneum;
                case "Veritaseum":
                    return Resources.Fa_IR.veritaseum;
                case "KuCoin Shares":
                    return Resources.Fa_IR.kucoinshares;
                case "Waves":
                    return Resources.Fa_IR.waves;
                case "0x":
                    return Resources.Fa_IR.ziroX;
                case "BitShares":
                    return Resources.Fa_IR.bitshares;
                case "Augur":
                    return Resources.Fa_IR.augur;
                case "Maker":
                    return Resources.Fa_IR.maker;
                case "Status":
                    return Resources.Fa_IR.status;
                case "Ardor":
                    return Resources.Fa_IR.ardor;
                case "Steem":
                    return Resources.Fa_IR.steem;
                case "Bytecoin":
                    return Resources.Fa_IR.Bytecoin;
                case "Stratis":
                    return Resources.Fa_IR.stratis;
                case "Siacoin":
                    return Resources.Fa_IR.siacoin;
                case "Binance Coin":
                    return Resources.Fa_IR.binanceCoin;
                case "Verge":
                    return Resources.Fa_IR.Verge;
                case "Zcash":
                    return Resources.Fa_IR.Zcash;
                case "Populous":
                    return Resources.Fa_IR.populous;
                case "OmiseGO":
                    return Resources.Fa_IR.omisego;
                case "Tether":
                    return Resources.Fa_IR.tether;
                case "RaiBlocks":
                    return Resources.Fa_IR.raiblocks;
                case "VeChain":
                    return Resources.Fa_IR.vechain;
                case "Lisk":
                    return Resources.Fa_IR.lisk;
                case "Ethereum Classic":
                    return Resources.Fa_IR.EthereumClassic;
                case "IOStoken":
                    return Resources.Fa_IR.IOStoken;
                case "AMD":
                    return Resources.Fa_IR.AMD;
                case "AZN":
                    return Resources.Fa_IR.AZN;
                case "RUB":
                    return Resources.Fa_IR.RUB;
                case "THB":
                    return Resources.Fa_IR.THB;
                case "MYR":
                    return Resources.Fa_IR.MYR;
                case "HKD":
                    return Resources.Fa_IR.HKD;
                case "SGD":
                    return Resources.Fa_IR.SGD;
                case "PKR":
                    return Resources.Fa_IR.PKR;
                case "INR":
                    return Resources.Fa_IR.INR;
                case "SYP":
                    return Resources.Fa_IR.SYP;
                case "BHD":
                    return Resources.Fa_IR.BHD;
                case "IQD":
                    return Resources.Fa_IR.IQD;
                case "OMR":
                    return Resources.Fa_IR.OMR;
                case "QAR":
                    return Resources.Fa_IR.QAR;
                case "SAR":
                    return Resources.Fa_IR.SAR;
                case "KWD":
                    return Resources.Fa_IR.KWD;
                case "NOK":
                    return Resources.Fa_IR.NOK;
                case "DKK":
                    return Resources.Fa_IR.DKK;
                case "SEK":
                    return Resources.Fa_IR.SEK;
                case "AFN":
                    return Resources.Fa_IR.AFN;
                case "CHF":
                    return Resources.Fa_IR.CHF;
                case "NZD":
                    return Resources.Fa_IR.NZD;
                case "AUD":
                    return Resources.Fa_IR.AUD;
                case "CAD":
                    return Resources.Fa_IR.CAD;
                case "JPY":
                    return Resources.Fa_IR.JPY;
                case "CNY":
                    return Resources.Fa_IR.CNY;
                case "TRY":
                    return Resources.Fa_IR.TRY;
                case "AED":
                    return Resources.Fa_IR.AED;
                case "GBP":
                    return Resources.Fa_IR.GBP;
                case "EUR":
                    return Resources.Fa_IR.EUR;
                case "USD":
                    return Resources.Fa_IR.USD;
                case "geram18":
                    return Resources.Fa_IR.geram18;
                case "seke_bahar":
                    return Resources.Fa_IR.sekebahar;
                case "gerami":
                    return Resources.Fa_IR.gerami;
                case "mesghal":
                    return Resources.Fa_IR.mesghal;
                case "sekee_emami":
                    return Resources.Fa_IR.sekeeemami;
                case "rob":
                    return Resources.Fa_IR.rob;
                case "ons":
                    return Resources.Fa_IR.ons;
                case "geram24":
                    return Resources.Fa_IR.geram24;
                case "nim":
                    return Resources.Fa_IR.nim;
                default:
                    return currencyName;
            }
        }

        public static string GetPersianDatetime(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var day = pc.GetDayOfMonth(date); //day
            var month = pc.GetMonth(date); //month
            var year = pc.GetYear(date); //year
            var hour = pc.GetHour(date); //hour
            var minute = pc.GetMinute(date); //minute
            return $"{year}/{month}/{day} {hour}:{minute}";
        }
        public static string ToDecimal(string value, DigitType type = DigitType.Toman)
        {
            decimal price = decimal.Parse(value, NumberStyles.Currency);

            if (price >= 1)
            {
                if (type == DigitType.USD)
                {
                    return String.Format("{0:0.00}", price);
                }
                else
                {
                    return price.ToString("#,#");
                }
            }

            return String.Format("{0:0.00}", price);
        }

        public static string ConvertToPersianDigit(string value)
        {
            string name = "";
            foreach (var item in value)
            {
                switch (item)
                {
                    case '0':
                        name += "۰";
                        break;
                    case '1':
                        name += "۱";
                        break;
                    case '2':
                        name += "۲";
                        break;
                    case '3':
                        name += "۳";
                        break;
                    case '4':
                        name += "۴";
                        break;
                    case '5':
                        name += "۵";
                        break;
                    case '6':
                        name += "۶";
                        break;
                    case '7':
                        name += "۷";
                        break;
                    case '8':
                        name += "۸";
                        break;
                    case '9':
                        name += "۹";
                        break;
                    default:
                        name += item;
                        break;
                }
            }
            return name;
        }

        public static string ConvertToToman(double USD, double USDprice)
        {
            var USDToman = USDprice / 10;
            /*
             * 1 - convert value USD to rial
             * 2 - fee
             * 3 - USD must expensive than now 
             * 
             */
            double result = Math.Round((USD * USDToman));
            return result.ToString();
        }
        public static string ConvertToPersianDay(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Saturday":
                    return "شنبه";
                case "Sunday":
                    return "یکشنبه";
                case "Monday":
                    return "دوشنبه";
                case "Tuesday":
                    return "سه شنبه";
                case "Wednesday":
                    return "چهارشنبه";
                case "Thursday":
                    return "پنج‌شنبه";
                case "Friday":
                    return "جمعه";
                default:
                    return dayOfWeek;
            }
        }

        public static List<string> GetSevenDaysEarly()
        {
            var now = DateTime.Now; //current datetime
            var past = now.AddDays(-7); //seven days earlier
            List<string> days = new List<string>();

            while (past < now)
            {
                days.Add(ConvertToPersianDay(past.DayOfWeek.ToString()));
                past = past.AddDays(1);
            }

            //while (now > past)
            //{
            //    days.Add(now.DayOfWeek.ToString());
            //    now = now.AddDays(-1);
            //}
            return days;
        }

        public static string flag(string symbol)
        {
            //define a dictionary
            var dic = new Dictionary<string, string>()
            {
                {"USD" , "🇺🇸" },
                {"EUR", "‏🇪🇺" },
                {"GBP", "‏‏🇬🇧" },
                {"JPY", "‏‏🇯🇵" },
                {"CHF", "‏‏🇨🇭" },
                {"AUD", "‏‏🇦🇺" },
                {"CAD", "‏‏🇨🇦" },
                {"NZD", "‏‏🇳🇿" },
                {"TRY", "‏‏🇹🇷" },
                {"SEK", "‏‏‏🇸🇪" },
                {"DKK", "‏‏‏🇩🇰" },
                {"NOK", "‏‏‏🇳🇴" },
                {"AED", "‏🇦🇪" },
                {"KWD", "‏‏‏🇰🇼" },
                {"SAR", "‏‏‏🇸🇦" },
                {"CNY", "‏‏‏🇨🇳" },
                {"RUB", "‏‏‏🇷🇺" },
                {"AMD", "‏‏‏🇦🇲" },
                {"BHD", "‏‏‏🇧🇭"},
                {"GEL", "‏‏‏🇬🇪" },
                {"IQD", "‏🇮🇶" },
                {"QAR", "‏‏🇶🇦" },
                {"PKR", "‏‏🇵🇰" },
                {"SYP", "‏‏🇸🇾" },
                {"MYR", "‏‏🇲🇾" },
                {"THB", "‏‏🇹🇭" },
                {"AZN", "‏🇦🇿" },
                {"AFN" , "🇦🇫" },
                {"OMR", "🇴🇲" },
                {"INR","🇮🇳" },
                {"SGD","🇸🇬" },
                {"HKD","🇨🇳" },
                {"USD_EX" , "🇺🇸" }
            };

            if (dic.ContainsKey(symbol))
            {
                var value = dic[symbol];
                return value;
            }
            return "-";
        }
    }
}