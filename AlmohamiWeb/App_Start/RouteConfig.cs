using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlmohamiWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               "ResetpasswordLink",                                              // Route name
               "{controller}/{action}/{User}/{ResetToken}/{IsResetTokenInvalid}",                           // URL with parameters
               new { controller = "Account", action = "ResetPassword", User = "", ResetToken = "", IsResetTokenInvalid = "" }  // Parameter defaults
           );

            //routes.MapRoute(
            //name: "RoleEdit",
            //url: "{RoleId}",
            //defaults: new { controller = "SecurityRolePermission", action = "EditRole" },
            //constraints: new { RoleId = "\\d+" }
            //);
            
        }
    }
}
