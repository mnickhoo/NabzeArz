using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NabzeArz.Models
{
    public enum PriceOption
    {
        gold , 
        fiat , 
        cryptoCurrency , 
        full
    }
    public enum NerkhType
    {
        currency,
        gold
    }

    public enum CurrencyType
    {
        AMD,
        AZN,
        RUB,
        THB,
        MYR,
        HKD,
        SGD,
        PKR,
        INR,
        SYP,
        BHD,
        IQD,
        OMR,
        QAR,
        SAR,
        KWD,
        NOK,
        DKK,
        SEK,
        AFN,
        CHF,
        NZD,
        AUD,
        CAD,
        JPY,
        CNY,
        TRY,
        AED,
        GBP,
        EUR,
        USD ,
        USD_EX , 
        USDHandyHavale, 
        USDHandy
    }

    public enum GoldType
    {
        geram18,
        seke_bahar,
        gerami,
        mesghal,
        sekee_emami,
        rob,
        ons,
        geram24,
        nim
    }

    public enum CurrencyName
    {
        FindAll,
        Bitcoin,
        Ethereum,
        EthereumClassic,
        Monero,
        Ripple,
        ZCash,
        Factom,
        Litecoin,
        Dogecoin,
        DigitalCash
    }

    //enum for symbol 
    public enum Symbol
    {
        BTC,
        ETH,
        ETC,
        XMR,
        XRP,
        ZEC,
        FCT,
        LTC,
        DOGE,
        DASH
    }
    public enum DigitType
    {
        Toman,
        USD
    }
}