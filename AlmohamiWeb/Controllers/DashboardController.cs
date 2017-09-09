using Almohami.Core.Enums;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using AlmohamiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AlmohamiWeb.Controllers
{
    public class DashboardController : AlmohamiController
    {

        private readonly IDashboardService _dashboardService;
        private readonly ISecurityRolePermissionService _securityRolePermissionService;
        private readonly IUserServices _userService;
        private readonly ICaseServices _CaseServices;

        public string MailFrom = "", MailTo = "", MailCC = "", MailBCC = "", Subject = "", Body = "", Attachment = "";
        public string MailFrom1 = "", MailTo1 = "", MailCC1 = "", MailBCC1 = "", Subject1 = "", Body1 = "", Attachment1 = "";

        public bool IsBodyHtml = true;
        public DashboardController()
        {
            _dashboardService = new DashboardService();
            _securityRolePermissionService = new SecurityRolePermissionService();
            _userService = new UserService();
            _CaseServices = new CaseServices();


        }

        // GET: Dashboard

        public ActionResult Index()
        {
            //DashboardModel dashboardModel = new DashboardModel();
            // TempData["Name"] = User.Name;

            AppointmentEntityModel dashboardModel = new AppointmentEntityModel();
            dashboardModel.AppointmentCategoryList = _CaseServices.GetAllAppointmentCategory();
            return View(dashboardModel);


        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            long logid = UserLogDetail();
            Session.Abandon();
            if (logid > 0 && logid.ToString() != "")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public long UserLogDetail()
        {
            UserLogEntityModel objlog = new UserLogEntityModel();
            objlog.UserStatus = false;
            objlog.UserLogID = User.Userlogid;
            //objlog.UserLogRole = User.RoleId;
            long logid = _userService.AddorUpdateUserLog(objlog);
            return logid;
        }

        #region Menubbind
        public ActionResult Menubind()
        {
            DashboardModel dashboardModel = new DashboardModel();
            dashboardModel.ModuleList = _dashboardService.GetAllModuleList();
            var ParentModuleList = _dashboardService.GetAllParentModuleList();
            dashboardModel.ParentModuleList = new List<Almohami.Data.AlmohamiModel.ApplicationParentModule>();

            dashboardModel.SecurityRoleId = User.RoleId;
            dashboardModel.LanguageId = Convert.ToInt32(Session["CurrentUICulture"]);

            if (dashboardModel.LanguageId == (int)Language.English)
            {
                if (User.RoleId == (int)Role.MasterAdmin)
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.MasterAdmin && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
                else if (User.RoleId == (int)Role.SystemAdmin)
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.SystemAdmin && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
                else if (User.RoleId == (int)Role.LawyerAdmin)
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.LawyerAdmin && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
                else
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.User && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
            }
            else
            {
                if (User.RoleId == (int)Role.MasterAdmin)
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.AraMasterAdmin && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
                else if (User.RoleId == (int)Role.SystemAdmin)
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.AraSystemAdmin && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
                else if (User.RoleId == (int)Role.LawyerAdmin)
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.AraLawyerAdmin && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }
                else
                {
                    dashboardModel.vRolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == (int)Role.AraUser && row.CanView == true && row.LanguageId == dashboardModel.LanguageId).OrderBy(row => row.SortOrder).ToList();
                }

            }


            int totalCount = ParentModuleList.Count();
            int?[] parentModuleId = new int?[totalCount];
            int i = 0;

            foreach (var item in dashboardModel.vRolePermissionList)
            {
                if (!(parentModuleId.Contains(item.ParentModuleId)))
                {
                    parentModuleId[i] = item.ParentModuleId;
                    dashboardModel.ParentModuleList.Add(ParentModuleList.Where(row => row.ParentModuleId == parentModuleId[i]).FirstOrDefault());
                    i++;
                }
            }

            return PartialView("~/Views/Dashboard/_Menubind.cshtml", dashboardModel);
        }
        #endregion


    }
}