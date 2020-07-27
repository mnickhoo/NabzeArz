using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace NabzeArz
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           // config.Routes.MapHttpRoute(
           //    name: "Api with action",
           //    routeTemplate: "api/{controller}/{action}/{id}",
           //    defaults: new { id = RouteParameter.Optional }
           //);

            ////config return json format
            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //    .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
