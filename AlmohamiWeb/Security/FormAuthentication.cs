using Almohami.Core.Helper;
using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace AlmohamiWeb.Security
{
    public static class FormAuthentication
    {
        public static void StoreCurrentUserData(User userDetails, long logid, bool RememberMe)
        {

            //HttpCookie cookie = new HttpCookie("userlogin");
            //cookie["username"] = userDetails.UserName.ToString();
            //cookie["name"] = userDetails.UserName.ToString();
            //cookie["emailid"] = userDetails.UserEmailId.ToString();
            //cookie["adminid"] = userDetails.UserId.ToString();
            //cookie["roleId"] = userDetails.UserRoleId.ToString();

            //cookie.Expires = DateTime.Now.AddDays(1);
            //HttpContext.Current.Response.Cookies.Add(cookie);

            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.UserId = userDetails.UserId;
            serializeModel.Name = userDetails.UserName;
            serializeModel.RoleId = (long)userDetails.UserRoleId;
            serializeModel.Email = userDetails.UserEmailId;
            serializeModel.Userlogid = logid;
            string userData = JsonConvert.SerializeObject(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
            1,
            userDetails.UserEmailId,
            DateTime.Now,
            DateTime.Now.AddMinutes(RememberMe
            ? 525600 : 60),
            RememberMe,
            userData);

            int timeout = RememberMe ? 525600 : 60;

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = DateTime.Now.AddMinutes(timeout);
            authCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(authCookie);
            //FormAuth.authcookie();
        }

    }

}