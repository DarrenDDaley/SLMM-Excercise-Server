using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SLMM_ServerCode
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{x}/{y}",
                defaults: new { x = RouteParameter.Optional, y = RouteParameter.Optional }
            );
        }
    }
}
