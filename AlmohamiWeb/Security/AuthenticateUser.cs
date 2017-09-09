using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using Almohami.Services.Services;
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
    public static class AuthenticateUser
    {
        public static bool ValidateUser1(LoginModel logon, HttpResponseBase response)
        {
            bool result = false;
            if (Membership.ValidateUser(logon.Email, logon.Password))
            {
                // Create the authentication ticket with custom user data.
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(logon);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        logon.Email,
                        DateTime.Now,
                        DateTime.Now.AddDays(30),
                        true,
                        userData,
                        FormsAuthentication.FormsCookiePath);
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);
                // Create the cookie.
                response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                result = true;
            }
            return result;
        }


        public static User AuthenticateUserData(string username, string password)
        {

            AccountLoginServices accountLoginServices = new Almohami.Services.Services.AccountLoginServices();
            LoginModel loginModel = new Almohami.Services.Entities.LoginModel();
            loginModel.Email = username;
            loginModel.Password = password;
            User user = accountLoginServices.CheckLogin(loginModel);

            return user;
        }

        public static User User
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // The user is authenticated. Return the user from the forms auth ticket.
                    return ((MyPrincipal)(HttpContext.Current.User)).User
;
                }
                else if (HttpContext.Current.Items.Contains("User"))
                {
                    // The user is not authenticated, but has successfully logged in.
                    return (User)HttpContext.Current.Items["User"];
                }
                else
                {
                    return null;
                }
            }

        }

    }

}