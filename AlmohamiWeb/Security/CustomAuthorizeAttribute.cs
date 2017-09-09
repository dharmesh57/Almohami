using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlmohamiWeb.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string UsersConfigKey { get; set; }
        public string RolesConfigKey { get; set; }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
                if (!filterContext.HttpContext.Request.IsAuthenticated)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = 401;
                        filterContext.Result = new JsonResult { Data = "LogOut" };
                    }
                    else
                    {
                        string redirectUrl = string.Format(filterContext.HttpContext.Request.Url.PathAndQuery);
                        filterContext.Result = new RedirectToRouteResult(new
                               RouteValueDictionary(new { controller = "Account", action = "Login" }));
                    }
                }
        }
    }

    public class CustomPrincipalSerializeModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public long RoleId { get; set; }
        public string Email { get; set; }
        public long Userlogid { get; set; }

    }
    
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
            
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public string Email { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string Name { get; set; }
        public long Userlogid { get; set; }

    }
}

