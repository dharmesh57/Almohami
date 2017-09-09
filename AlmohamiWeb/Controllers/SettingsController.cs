using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using AlmohamiWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmohamiWeb.Controllers
{
    public class SettingsController : AlmohamiController
    {
        #region Private Variables
        private readonly ISettingsService _settingsservice;

        #endregion

        #region Controller
        public SettingsController()
        {
            _settingsservice = new SettingsService();
        }
        #endregion

        // GET: Settings
        public ActionResult Index()
        {
            return View("SettingsLayout");
        }

        //GET:PersonalInfo
        [HttpGet]
        public ActionResult PersonalInfo()
        {
            UserEntityModel model = new UserEntityModel();
            long Id = User.UserId;
            UserEntityModel data = _settingsservice.GetUserById(Id);
            return PartialView("_PersonalInfo", data);
        }
    
        //Post:PersonalInfo
        [HttpPost]
        public ActionResult PersonalInfo(UserEntityModel model, HttpPostedFileBase file)
        {

            model.filesize = 10;
            string us = UploadUserFile(file, model);
            if (us != null)
            {
                //ViewBag.ResultMessage = cliententitymodel.getseterror;
                ModelState.AddModelError("", model.getseterror);
                return View();
            }

            string ImageName = System.IO.Path.GetFileName(file.FileName);
            string physicalPath = Server.MapPath("~/Content/Img/UserImg/" + ImageName);

            // save image in folder
            file.SaveAs(physicalPath);

            //save new record in database

            model.UserAvtar = ImageName;
            model.UserID = User.UserId;
            model.LastModifiedBy = User.UserId;
            _settingsservice.AddOrUpdateUser(model);

            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {

            ChangePasswordModel model = new ChangePasswordModel();
            return PartialView("_ChangePassword", model);
        }
        public string UploadUserFile(HttpPostedFileBase file, UserEntityModel userEntityModel)
        {
            try
            {
                // supported extensions
                // you can add any of extension,if you want pdf file validation then add .pdf in 
                // variable supportedTypes.

                var supportedTypes = new[] { "jpg", "jpeg", "png" };

                // following will fetch the extension of posted file.

                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                // Image datatype is included in System.Drawing librery.will get the image properties 
                //  like height, width.

                Image fp = System.Drawing.Image.FromStream(file.InputStream);

                //variable will get the ratio of image 
                // (600 x 400),ratio will be 1.5 

                //decimal fu = ((decimal)fp.Width / fp.Height);

                if (file.ContentLength > (userEntityModel.filesize * 1024))
                {
                    userEntityModel.getseterror = "filesize will be upto " + userEntityModel.filesize + "KB";
                    return userEntityModel.getseterror;
                }
                else if (!supportedTypes.Contains(fileExt))
                {
                    userEntityModel.getseterror = "file extension is not valid";
                    return userEntityModel.getseterror;
                }
                //else if (fu != clientEntityModel.ar)
                //{
                //    clientEntityModel.getseterror = "file should be in mentioned aspect ratio";
                //    return clientEntityModel.getseterror;
                //}
                else
                {
                    userEntityModel.getseterror = null;
                    return userEntityModel.getseterror;
                }
            }
            catch (Exception ex)
            {
                userEntityModel.getseterror = ex.Message;
                return userEntityModel.getseterror;
            }
        }

        [ValidateAjax]
        [HttpPost]

        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            model.UserID = User.UserId;
            var user = _settingsservice.CheckPassword(model);
            if (user != null)
            {
                _settingsservice.UpdatePassword(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult CompanyPreferences()
        {
            return PartialView("_CompanyPreferences");
        }
    }
}