using Almohami.Core.Helper;
using AlmohamiWeb.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace AlmohamiWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // Get Culture session

            int value = 2;

            HttpContext context = HttpContext.Current;

            if (context.Session == null || context.Session["CurrentUICulture"] == null)
            {
                value = 2;
            }
            else
            {
                value = (int)HttpContext.Current.Session["CurrentUICulture"];
            }


            if (value == 2)
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-KW");
            
            else
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // Set the thread's CurrentCulture the same as CurrentUICulture.
            //
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
        }
        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        //        CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

        //        if (serializeModel != null)
        //        {
        //            CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
        //            newUser.UserId = serializeModel.UserId;
        //            newUser.Name = serializeModel.Name;
        //            newUser.RoleId = serializeModel.RoleId;
        //            newUser.Email = serializeModel.Email;
        //            HttpContext.Current.User = newUser;
        //        }
        //    }
        //}

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {            
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

                if (serializeModel != null)
                {
                    
                    CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                    newUser.UserId = serializeModel.UserId;
                    newUser.Name = serializeModel.Name;
                    newUser.RoleId = serializeModel.RoleId;
                    newUser.Email = serializeModel.Email;
                    HttpContext.Current.User = newUser;
                }
            }

        }
    }
}
