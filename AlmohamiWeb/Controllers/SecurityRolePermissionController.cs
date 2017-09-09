using Almohami.Core.Enums;
using Almohami.Core.Helper;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using AlmohamiWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmohamiWeb.Controllers
{
    public class SecurityRolePermissionController : AlmohamiController
    {
        #region Private Variables
        private readonly ISecurityRolePermissionService _securityRolePermissionService;

        #endregion

        #region Ctor
        public SecurityRolePermissionController()
        {
            _securityRolePermissionService = new SecurityRolePermissionService();
        }
        #endregion

        // GET: SecurityRolePermission

        #region Index
        public ActionResult Index()
        {
            SecurityRolePermissionEntityModel securityRolePermissionEntityModel = new SecurityRolePermissionEntityModel();
            securityRolePermissionEntityModel.LanguageId = Convert.ToInt32(Session["CurrentUICulture"]);
            securityRolePermissionEntityModel.SecurityRoleList = _securityRolePermissionService.GetAllRoleData(securityRolePermissionEntityModel.LanguageId);
            return View(securityRolePermissionEntityModel);
        }
        #endregion

        #region AddRole

        public ActionResult AddRole()
        {
            SecurityRolePermissionEntityModel securityRolePermissionEntityModel = new SecurityRolePermissionEntityModel();
            return View(securityRolePermissionEntityModel);
        }

        [ValidateAjax]
        [HttpPost]
        public ActionResult AddRole(SecurityRolePermissionEntityModel securityRolePermissionEntityModel)
        {
            if (ModelState.IsValid)
            {

                if (Convert.ToInt32(Session["CurrentUICulture"]) == (int)Language.English)
                {
                    securityRolePermissionEntityModel.LanguageId = (int)Language.English;
                }
                else
                {
                    securityRolePermissionEntityModel.LanguageId = (int)Language.Arabic;
                }

                _securityRolePermissionService.AddOrUpdateRole(securityRolePermissionEntityModel);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAndNew(SecurityRolePermissionEntityModel securityRolePermissionEntityModel)
        {
            if (ModelState.IsValid)
            {
                _securityRolePermissionService.AddOrUpdateRole(securityRolePermissionEntityModel);

                return RedirectToAction("AddRole", "SecurityRolePermission");
            }

            return Json(new { success = false, Errors = GetErrorsFromModelState() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EditRole
        [HttpGet]


        [Route("EditRole/{RoleId:int}")]
        public ActionResult EditRole(int RoleId)
        {
            //int id = Convert.ToInt32(Helpers.base64Decode(RoleId));
            SecurityRolePermissionEntityModel securityRolePermissionEntityModel = new SecurityRolePermissionEntityModel();
            var role = _securityRolePermissionService.GetRoleById(RoleId);
            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(int id, SecurityRolePermissionEntityModel securityRolePermissionEntityModel)
        {
            if (ModelState.IsValid)
            {
                _securityRolePermissionService.AddOrUpdateRole(securityRolePermissionEntityModel);

                return RedirectToAction("Index", "SecurityRolePermission");
            }
            return Json(new { success = false, Errors = GetErrorsFromModelState() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAndNew(int id, SecurityRolePermissionEntityModel securityRolePermissionEntityModel)
        {
            if (ModelState.IsValid)
            {
                _securityRolePermissionService.AddOrUpdateRole(securityRolePermissionEntityModel);

                return RedirectToAction("AddRole", "SecurityRolePermission");
            }
            return Json(new { success = false, Errors = GetErrorsFromModelState() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Role
        public ActionResult DeleteRole(int id, SecurityRolePermissionEntityModel serviceentitymodel)
        {
            try
            {
                serviceentitymodel = new SecurityRolePermissionEntityModel();
                _securityRolePermissionService.DeleteRole(id);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region ManageSecurityRolePermissions

        public ActionResult ManageSecurityRolePermissions(int id)
        {
            SecurityRolePermissionEntityModel securityRolePermissionEntityModel = new SecurityRolePermissionEntityModel();
            var role = _securityRolePermissionService.GetRoleById(id);
            securityRolePermissionEntityModel.RoleName = role.RoleName;
            securityRolePermissionEntityModel.RoleDescription = role.RoleDescription;
            securityRolePermissionEntityModel.LanguageId = Convert.ToInt32(Session["CurrentUICulture"]);

            securityRolePermissionEntityModel.RolePermissionList = _securityRolePermissionService.GetAllRoleWithPermissionData().Where(row => row.SecurityRoleId == id && row.LanguageId == securityRolePermissionEntityModel.LanguageId).ToList();
            return View(securityRolePermissionEntityModel);
        }

        [HttpPost]
        public ActionResult ManageSecurityRolePermissions(SecurityRolePermissionEntityModel AddsecurityRolePermissionEntityModel)
        {
            SecurityRolePermissionEntityModel securityRolePermissionEntityModel = new SecurityRolePermissionEntityModel();
            if (Convert.ToInt32(Session["CurrentUICulture"]) == (int)Language.English)
            {
                AddsecurityRolePermissionEntityModel.LanguageId = (int)Language.English;
            }
            else
            {
                AddsecurityRolePermissionEntityModel.LanguageId = (int)Language.Arabic;
            }
            _securityRolePermissionService.AddOrUpdatePermission(AddsecurityRolePermissionEntityModel);

            return View(securityRolePermissionEntityModel);
        }
        #endregion
    }
}