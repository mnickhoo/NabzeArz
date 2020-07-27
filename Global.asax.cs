using IdentitySample.Models;
using NabzeArz;
using NabzeArz.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IdentitySample
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register); //for webAPI

            //register global filter
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //register route
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //register boundle here
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //
            Database.SetInitializer<ApplicationDbContext>(null);



            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                               .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                               .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            ////Run send message task schedule
            //ISchedule taskSend = new SchaduleSendMessage();
            //Task.Run(async () => await taskSend.Run());

            //////Run update message task schedule
            //ISchedule taskUpdate = new SchaduleUpdateMessage();
            //Task.Run(async () => await taskUpdate.Run());

            ////////send chart in channel
            //ISchedule taskChart = new schaduleChart();

            //Task.Run(async () => await taskChart.Run());

            ////update Gold and fiat currency in database
            //ISchedule taskNerkh = new schaduleNerkh();
            //Task.Run(async () => await taskNerkh.Run() );

            //Start SqlDependency with application initialization
            //SqlDependency.Start(connString);
        }
     
        protected void Application_Error()
        {
            logError();
            Server.ClearError();
            //Stop SqlDependency
            SqlDependency.Stop(connString);
        }
        public void logError()
        {
            Exception error = Server.GetLastError();
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/log/"); ;
            string fullPathAndFileName = path + timeStamp + ".txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (StreamWriter stream = new StreamWriter(fullPathAndFileName, true))
            {
                stream.WriteLine(error);
            }
        }
    }

}

