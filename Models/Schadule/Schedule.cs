using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using Quartz.Impl;
using NabzeArz.Service;
using NabzeArz.Helper;

namespace NabzeArz.Models
{
    public class SendMessageJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)

        {
            #region old API from digiArz
            //var CurrencyList = await ApiService.GetCurrencyList(); //get currency list 
            //var text = CurrencyService.BuildStringForChannel(CurrencyList);
            //await TelegramService.SendMessage(text);
            #endregion
            var currencyRates =  NerkhAPIService.Create.GetCurrency(showInChannel : true); //get fiat currency         
            var cryptoCurrency = await CoinMarketAPISerivce.RequestAndSaveToDB(); //update json file
            var goldRates = NerkhAPIService.Create.GetGold();
            if (cryptoCurrency != null && currencyRates != null)
            {
                var text = CurrencyService.BuildStringForPublish(cryptoCurrency.Take(10) , currencyRates.Take(12) , goldRates); //send to telegram bot
                var resultFromChannel = await TelegramService.SendMessageToChanell(text.telegram, true , Telegram.Bot.Types.Enums.ParseMode.Default);

                if (resultFromChannel != null)
                {
                    UserService.ChangeMessageId(resultFromChannel);
                }
                twitterService.sendMesage(text.twitter); //send to twitter bot
            }

        }
    }

    public class SendChartJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ChartService.MakeChartImage("BTC"); //update chart image
            var image = await ChartService.SendChart(Convert.ToInt64(AppSetting.channel_chatId_test));
        }
    }

    public class NerkhJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var currencyRates = await NerkhAPIService.Create.RequestCurrencyAndSaveToDb(); //get fiat currency         
            var goldRates = await NerkhAPIService.Create.RequestGoldAndSaveToDb();
        }
    }

    public class UpdateMessageJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            #region old API from digiArz
            //var CurrencyList = await ApiService.GetCurrencyList(); //get currency list 
            //var text = CurrencyService.BuildStringForChannel(CurrencyList);
            //await TelegramService.SendMessage(text);
            #endregion
            //update json file 
            var result = await CoinMarketAPISerivce.RequestAndSaveToDB(); //update json file
            var currencyRates = NerkhAPIService.Create.GetCurrency(showInChannel : true); //get fiat currency         
            var goldRates =  NerkhAPIService.Create.GetGold(); //get fiat currency         

            if (result != null && currencyRates != null)
            {
                var text = CurrencyService.BuildStringForPublish(result.Take(10) , currencyRates.Take(12) , goldRates); //send to telegram bot
                var resultFromChannel = await TelegramService.UpdateMessageToChannel(text.telegram , parseMode : Telegram.Bot.Types.Enums.ParseMode.Default);
            }

        }
    }
    //public class UpdateUSDPriceJob : IJob
    //{
    //    public async Task Execute(IJobExecutionContext context)

    //    {
    //        await TgjuAPIService.RequestAndSaveToFile(); //update dollar price and save to json file
    //    }
    //}

    public class schaduleChart : ISchedule
    {
        public async Task Run()
        {
            //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 2);
            DateTimeOffset startTime = DateBuilder.FutureDate(30, IntervalUnit.Second); //start date

            //select job 
            IJobDetail job = JobBuilder.Create<SendChartJob>()
                                       .WithIdentity("sendChartImage")
                                       .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity("trigger3")
                                             .StartAt(startTime)
                                             .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever())
                                             .Build();



            ISchedulerFactory sf = new StdSchedulerFactory();
            var sc = await sf.GetScheduler();
            await sc.ScheduleJob(job, trigger); //giving job and trigger to schedule

            await sc.Start(); //schadule start
        }
    }

    public class schaduleNerkh : ISchedule
    {
        public async Task Run()
        {
            //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 2);
            DateTimeOffset startTime = DateBuilder.FutureDate(50, IntervalUnit.Second); //start date

            //select job 
            IJobDetail job = JobBuilder.Create<NerkhJob>()
                                       .WithIdentity("nerkhJob")
                                       .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity("triggerNerk")
                                             .StartAt(startTime)
                                             .WithSimpleSchedule(x => x.WithIntervalInMinutes(10).RepeatForever())
                                             .Build();



            ISchedulerFactory sf = new StdSchedulerFactory();
            var sc = await sf.GetScheduler();
            await sc.ScheduleJob(job, trigger); //giving job and trigger to schedule

            await sc.Start(); //schadule start
        }
    }

    public class SchaduleUpdateMessage : ISchedule
    {
        public async Task Run()
        {
            //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 2);
            DateTimeOffset startTime = DateBuilder.FutureDate(60, IntervalUnit.Second); //start date

            //select job 
            IJobDetail job = JobBuilder.Create<UpdateMessageJob>()
                                       .WithIdentity("UpdateMessageJob")
                                       .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity("trigger2")
                                             .StartAt(startTime)
                                             .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                                             .Build();



            ISchedulerFactory sf = new StdSchedulerFactory();
            var sc = await sf.GetScheduler();
            await sc.ScheduleJob(job, trigger); //giving job and trigger to schedule

            await sc.Start(); //schadule start
        }
    }


    public class SchaduleSendMessage : ISchedule
    {
        public async Task Run()
        {
            //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 2);
            DateTimeOffset startTime = DateBuilder.FutureDate(2, IntervalUnit.Second); //start date

            //select job 
            IJobDetail job = JobBuilder.Create<SendMessageJob>()
                                       .WithIdentity("SendMessageJob")
                                       .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity("trigger1")
                                             .StartAt(startTime)
                                             .WithSimpleSchedule(x => x.WithIntervalInMinutes(180).RepeatForever())
                                             .Build();



            ISchedulerFactory sf = new StdSchedulerFactory();
            var sc = await sf.GetScheduler();
            await sc.ScheduleJob(job, trigger); //giving job and trigger to schedule

            await sc.Start(); //schadule start
        }
    }

    //public class SchadulUpdateUSDPrice : ISchedule
    //{
    //    public async Task Run()
    //    {
    //        //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 2);
    //        DateTimeOffset startTime = DateBuilder.FutureDate(30, IntervalUnit.Second); //start date

    //        //select job 
    //        IJobDetail job = JobBuilder.Create<UpdateUSDPriceJob>()
    //                                   .WithIdentity("UpdateUSDPriceJob")
    //                                   .Build();

    //        ITrigger trigger = TriggerBuilder.Create()
    //                                         .WithIdentity("triggerUSD")
    //                                         .StartAt(startTime)
    //                                         .WithSimpleSchedule(x => x.WithIntervalInMinutes(10).RepeatForever())
    //                                         .Build();



    //        ISchedulerFactory sf = new StdSchedulerFactory();
    //        var sc = await sf.GetScheduler();
    //        await sc.ScheduleJob(job, trigger); //giving job and trigger to schedule

    //        await sc.Start(); //schadule start
    //    }
    //}
}