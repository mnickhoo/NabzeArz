using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NabzeArz.Models;
using NabzeArz.Service;

namespace NabzeArz.Hubs
{
    public class StatesHub : Hub
    {
        [HubMethodName("pushStates")]
        public static void pushStates(string message)
        {
            StatesHub.updateChart(); //trigger update chart methode for updae chart data 
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<StatesHub>();

            var count = TrackingService.sumRequest();
            Dictionary<string, int> states = new Dictionary<string, int>();
            states.Add("قیمت دلار", 180);
            states.Add("قیمت بیت کوین", 100);
            states.Add("دریافت دوج کوین", 50);

            context.Clients.All.getCount(new { count = count, message = message, states = states });
        }

        //just for send data to user trigger by pushStates 
        public static void updateChart(EnumOperation operation = EnumOperation.getStates) //
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<StatesHub>();
            IEnumerable<Report> reports = new List<Report>();
            //check this operation 
            switch (operation)
            {
                case EnumOperation.getStates:
                    reports = TrackingService.forChart();
                    break;
                default:
                    break;
            }
            //TrackingService
            //make a report

            context.Clients.All.getStates(reports);
        }


        [HubMethodName("sendUptodateInformation")]
        public static void SendUptodateInformation(string action)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<StatesHub>();

            // the updateStudentInformation method will update the connected client about any recent changes in the server data
            context.Clients.All.updateStudentInformation(action);
        }


    }
}