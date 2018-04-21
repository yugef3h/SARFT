using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Upload_Photo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new { controller = "Home", action = "Login" }
           ); routes.MapRoute(
                 name: "AlterUsersInfo",
                 url: "AlterUsersInfo/{id}",
                 defaults: new { controller = "Users", action = "UpdateUser", id = UrlParameter.Optional }
             ); routes.MapRoute(
                 name: "AlterReport",
                 url: "AlterReport/{id}",
                 defaults: new { controller = "Report", action = "UpdateReport", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "Upload_Map",
                url: "Upload_Map/{id}",
                defaults: new { controller = "Map", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
      name: "ErrorPage",
      url: "ErrorPage/{ErroCode}",
      defaults: new { controller = "Admin", action = "ErrorPage", ErroCode = UrlParameter.Optional }
  );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
