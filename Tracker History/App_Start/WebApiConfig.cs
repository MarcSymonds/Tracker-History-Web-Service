using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Tracker_History {
   public static class WebApiConfig {
      public static void Register(HttpConfiguration config) {
         // Web API configuration and services

         // Web API routes
         config.MapHttpAttributeRoutes();

         config.Routes.MapHttpRoute(
            name: "Ping",
            routeTemplate: "api/Ping",
            defaults: new {
               controller = "Ping"
            });

         config.Routes.MapHttpRoute(
             name: "TrackerHistory",
             routeTemplate: "api/TrackerHistory",
             defaults: new {
                controller = "TrackerHistory",
                id = RouteParameter.Optional
             }
         );
      }
   }
}
