using Almohami.Core.Enums;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using AlmohamiWeb.Models;
using AlmohamiWeb.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlmohamiWeb.Controllers
{

    public class AlmohamiController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public AlmohamiController()
        {
            _dashboardService = new DashboardService();
        }
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }


       
        /// <summary>
        /// SuccessNotification
        /// </summary>
        /// <param name="message"></param>
        public void SuccessNotification(string message)
        {
            TempData["NotificationType"] = NotificationType.Success;
            TempData["Message"] = message;
        }

        /// <summary>
        /// ErrorNotification
        /// </summary>
        /// <param name="message"></param>
        public void ErrorNotification(string message)
        {
            TempData["NotificationType"] = NotificationType.Error;
            TempData["Message"] = message;
        }

        /// <summary>
        /// AccessDenied
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        public Dictionary<string, object> GetErrorsFromModelState()
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                // Only send the errors to the client.
                if (ModelState[key].Errors.Count > 0)
                {
                    errors[key] = ModelState[key].Errors;
                }
            }

            return errors;
        }

       

    }
}