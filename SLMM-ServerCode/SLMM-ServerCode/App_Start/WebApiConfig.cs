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

            // I configure the route template so that you can call a specfic action method from the controller 
            // while still having an appropriately named precuror api name which is the controller name itself
            // I also made sure that that x and y position can be added to the methods that need them 
            // but made them optional as that information isn't relevant to all the methods
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{x}/{y}",
                defaults: new { x = RouteParameter.Optional, y = RouteParameter.Optional }
            );
        }
    }
}
