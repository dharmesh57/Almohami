using Almohami.Core.Helper;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using System;
using System.Web;
using System.Web.Mvc;
namespace AlmohamiWeb.Controllers
{
    public class UserController : AlmohamiController
    {
        #region Private Variables
        private readonly IUserServices _UserServices;
        private readonly ISecurityRolePermissionService _roleservices;
        public string MailFrom = "", MailTo = "", MailCC = "", MailBCC = "", Subject = "", Body = "", Attachment = "";
        public string MailFrom1 = "", MailTo1 = "", MailCC1 = "", MailBCC1 = "", Subject1 = "", Body1 = "", Attachment1 = "";
        public bool IsBodyHtml=true;
        #endregion

        #region Controller
        public UserController()
        {
            _UserServices = new UserService();
            _roleservices = new SecurityRolePermissionService();
        }
        #endregion

        // GET: User
       
        public ActionResult List()
        {
            UserEntityModel userentitymodel = new UserEntityModel();
            userentitymodel.UsersList = _UserServices.GetAllUsers(userentitymodel);
            return View(userentitymodel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            UserEntityModel userentitymodel = new UserEntityModel();
            userentitymodel.RoleList = _roleservices.GetAllRoleData(Convert.ToInt32(Session["CurrentUICulture"]));
            return View(userentitymodel);
        }

       
        [HttpPost]
        public ActionResult Create(UserEntityModel model)
        {
            if (ModelState.IsValid)
            {
                long Id = User.UserId;
                model.UserID = Id;
                model.IsDeleted = false;
                model.UserEmailConfirmed = false;
                model.UserPhoneConfirmed = false;
                model.Status = false;
                model.UserApprovedBy = Id;
                model.CreatedBy = User.UserId;
                model.CreatedDate = DateTime.Now;
                _UserServices.AddorUpdateUser(model);

                MailFrom = "";
                MailTo = model.Email;
                Subject = " Please confirm your email address";
                Body = BindTemplate(model.Email, model.Name, model.UserID);
                CommonHelp.SendMail(MailFrom, MailTo, MailCC, MailBCC, Subject, Body, Attachment, IsBodyHtml);

                TempData["SuccessMessage"] = "THANK YOU! Please go to your inbox and confirm your email";
                return RedirectToAction("Create", "User");
            }

            return View(model);
        }

        public ActionResult ConfirmEmail(string encryptid)
        {
            if (!string.IsNullOrEmpty(encryptid))
            {
                Int64 UserId = Convert.ToInt64(CommonHelp.Decryptdata(encryptid));
                if (UserId > 0)
                {
                    var Users = _UserServices.GetUserById(UserId);

                    UserEntityModel model = new UserEntityModel();
                    model.UserEmailConfirmed = true;
                    model.Status = false;
                   // model.RoleId = "Admin";
                    _UserServices.AddorUpdateUser(model);

                    MailFrom1 = "";
                    MailTo1 = Users.Email;
                    Subject1 = "Welcome to Almohami!";
                    Body1 = BindWelcomeTemplate(Users.Email, Users.Name, Users.UserID);
                    CommonHelp.SendMail(MailFrom1, MailTo1, MailCC1, MailBCC1, Subject1, Body1, Attachment1, IsBodyHtml);

                    return RedirectToAction("Details", "User");
                }
            }
            return View();
        }

        #region BindTemplate
        protected string BindTemplate(string mailid, string Name, Int64 id)
        {
            string Randomno, encryptname, encryptid, str = "";
            try
            {             

                if (mailid != "" && Name != "")
                {
                    encryptname = CommonHelp.Encryptdata(Name.ToString());
                    encryptid = CommonHelp.Encryptdata(id.ToString());
                    str += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><meta name='viewport' content='width=device-width, initial-scale=1.0' /><title></title><style type='text/css'>*{-webkit-font-smoothing:antialiased}body{Margin:0;padding:0;min-width:100%;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased;mso-line-height-rule:exactly}table{border-spacing:0;color:#333;font-family:Arial,sans-serif}img{border:0}.wrapper{width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}.webkit{max-width:600px}.outer{Margin:0 auto;width:100%;max-width:600px}.full-width-image img{width:100%;max-width:600px;height:auto}.inner{padding:10px}p{Margin:0;padding-bottom:10px}.h1{font-size:21px;font-weight:bold;Margin-top:15px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.h2{font-size:18px;font-weight:bold;Margin-top:10px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column .contents{text-align:left;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column p{font-size:14px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.two-column{text-align:center;font-size:0}.two-column .column{width:100%;max-width:300px;display:inline-block;vertical-align:top}.contents{width:100%}.two-column .contents{font-size:14px;text-align:left}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding-top:10px}.three-column{text-align:center;font-size:0;padding-top:10px;padding-bottom:10px}.three-column .column{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .contents{font-size:14px;text-align:center}.three-column img{width:100%;max-width:180px;height:auto}.three-column .text{padding-top:10px}.img-align-vertical img{display:inline-block;vertical-align:middle}@media only screen and (max-device-width: 480px){table[class=hide],img[class=hide],td[class=hide]{display:none !important}.contents1{width:100%}.contents1{width:100%}}</style></head><body style='Margin: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; min-width: 100%; background-color: #f3f2f0;'><center class='wrapper' style='width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; background-color: #f3f2f0;'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: #f3f2f0;' bgcolor='#f3f2f0;'><tr><td width='100%'><div class='webkit' style='max-width: 600px; Margin: 0 auto;'><table class='outer' align='center' cellpadding='0' cellspacing='0' border='1' style='border: 5px solid #0B3C5D; Margin: 0 auto; width: 100%; max-width: 600px;'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;'><table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><table style='width: 100%;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td align='center'><center><table border='0' align='center' width='100%' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td class='one-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;' bgcolor='#FFFFFF'><table cellpadding='0' cellspacing='0' border='0' width='100%' bgcolor='#42A0D6'><tr><td class='two-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='column' style='width: 100%; display: inline-block; vertical-align: top;'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 5px;' align='center'> <a href='https://app.almohami.com' target='_blank'> <img src='https://app.almohami.com/Content/layout3/img/logo-default.png' alt='' width='100%' height='auto' style='border-width: 0; display: block' align='center' /></a></td></tr></table></div></td></tr></table></td></tr></tbody></table></center></td></tr></tbody></table></td></tr></table><table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-spacing: 0; border-left: 1px solid #e8e7e5; border-right: 1px solid #e8e7e5; border-bottom: 1px solid #e8e7e5; border-top: 1px solid #e8e7e5' bgcolor='#FFFFFF'><tr><td align='left' style='padding: 20px 30px 5px 30px'><p style='color: #262626; font-size: 14px; text-align: left; font-family: Open Sans,sans-serif'> <strong>Dear " + Name.ToString() + ",</strong></p><p style='color: #000000; font-size: 14px; text-align: left; font-family: Open Sans,sans-serif; line-height: 18px'> We have received your request to add ' " + mailid.ToString() + "' to Almohami Account. <br /> Click the button below to confirm your email address.<br /></p><table border='0' align='left' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td align='center'><table border='0' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tr><td><div style='width: 200px; padding: 10px; margin-bottom:10px; background-color: #28B779; text-align: center'> <a href='https://app.almohami.com/Account/ConfirmEmail/" + encryptid + "' style='width: 250; display: block; text-decoration: none; border: 0; text-align: center; font-weight: bold; font-size: 18px; font-family: Arial, sans-serif; color: #ffffff' class='button_link'>Confirm Email Address</a></div></td></tr></table></td></tr></tbody></table></td></tr></table><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td height='5'>&nbsp;</td></tr><tr><td class='column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='' style='display: inline-block; vertical-align: top; margin: 10px 20px'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br /> This technical notification has been generated automatically. Please do not reply. You received this email notification because you either signed up for a Almohami or purchased a commercial subscription / license key. Besides technical notification, we may also send you important product news and information about discounts and special offers from time to time (preferences for such non-technical communication can be managed separately). <br /></p></td></tr><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br />For help, visit www.almohami.com/support/ or email help@almohami.com. <br /></p></td></tr></table></div></td></tr><tr><td height='10'>&nbsp;</td></tr></table></td></tr></table></div></td></tr></table></center></body></html>";

                }
            }
            catch (Exception ex)
            {

            }
            return str;

        }
        #endregion

        #region BindWelcomeTemplate
        protected string BindWelcomeTemplate(string mailid, string Name, Int64 id)
        {
            string str = "";
            try
            {
                if (mailid != "" && Name != "")
                {
                    str += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><meta name='viewport' content='width=device-width, initial-scale=1.0' /><title></title><style type='text/css'>*{-webkit-font-smoothing:antialiased}body{Margin:0;padding:0;min-width:100%;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased;mso-line-height-rule:exactly}table{border-spacing:0;color:#333;font-family:Arial,sans-serif}img{border:0}.wrapper{width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}.webkit{max-width:600px}.outer{Margin:0 auto;width:100%;max-width:600px}.full-width-image img{width:100%;max-width:600px;height:auto}.inner{padding:10px}p{Margin:0;padding-bottom:10px}.h1{font-size:21px;font-weight:bold;Margin-top:15px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.h2{font-size:18px;font-weight:bold;Margin-top:10px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column .contents{text-align:left;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column p{font-size:14px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.two-column{text-align:center;font-size:0}.two-column .column{width:100%;max-width:300px;display:inline-block;vertical-align:top}.contents{width:100%}.two-column .contents{font-size:14px;text-align:left}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding-top:10px}.three-column{text-align:center;font-size:0;padding-top:10px;padding-bottom:10px}.three-column .column{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .contents{font-size:14px;text-align:center}.three-column img{width:100%;max-width:180px;height:auto}.three-column .text{padding-top:10px}.img-align-vertical img{display:inline-block;vertical-align:middle}@media only screen and (max-device-width: 480px){table[class=hide],img[class=hide],td[class=hide]{display:none !important}.contents1{width:100%}.contents1{width:100%}}</style></head><body style='Margin: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; min-width: 100%; background-color: #f3f2f0;'><center class='wrapper' style='width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; background-color: #f3f2f0;'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: #f3f2f0;' bgcolor='#f3f2f0;'><tr><td width='100%'><div class='webkit' style='max-width: 600px; Margin: 0 auto;'><table class='outer' align='center' cellpadding='0' cellspacing='0' border='1' style='border: 5px solid #0B3C5D; Margin: 0 auto; width: 100%; max-width: 600px;'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;'><table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><table style='width: 100%;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td align='center'><center><table border='0' align='center' width='100%' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td class='one-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;' bgcolor='#FFFFFF'><table cellpadding='0' cellspacing='0' border='0' width='100%' bgcolor='#42A0D6'><tr><td class='two-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='column' style='width: 100%; display: inline-block; vertical-align: top;'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 5px;' align='center'><a href='https://app.almohami.com/' target='_blank'> <img src='https://app.almohami.com/Content/layout3/img/logo-default.png' alt='' width='100%' height='auto' style='border-width: 0; display: block' align='center' /></a></td></tr></table></div></td></tr></table></td></tr></tbody></table></center></td></tr></tbody></table></td></tr></table><table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-spacing: 0; border-left: 1px solid #e8e7e5; border-right: 1px solid #e8e7e5; border-bottom: 1px solid #e8e7e5; border-top: 1px solid #e8e7e5' bgcolor='#FFFFFF'><tr><td align='left' style='padding: 20px 30px 5px 30px'><p style='color: #0B3C5D; font-size: 24px; text-align: center; font-family: Open Sans,sans-serif'> <strong>Welcome to Almohami!</strong></p><p style='color: #262626; font-size: 14px; text-align: left; font-family: Open Sans,sans-serif'> <strong> Hi " + Name.ToString() + ", </strong></p><p style='color: #000000; font-size: 14px; text-align: left; font-family: Open Sans,sans-serif; line-height: 18px'> Congratulations,on taking the first big step to managing your business. We're excited for you to see how easy it is to get started.</p><p style='color: #000000; font-size: 14px; text-align: left; font-family: Open Sans, sans-serif; line-height: 18px'> As an approved member of the Almohami, you have been granted full access to Almohami Central-the online resource area for Almohami.<br /> Your account details are:</p><table border='0' align='left' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td align='left'><table border='0' cellpadding='0' cellspacing='0' style='background-color: #D6EBFF;margin:10px;padding:10px;border:solid #b5b4b4 1px;color:#808080'><tr><td colspan='3' height='20'> Email: 	" + mailid.ToString() + "</td></tr><tr><td colspan='3' height='20'> Login: 	https://app.almohami.com</td></tr><tr><td colspan='3' height='20'> Questions: 	'http://www.almohami.com/support/' </td></tr></table></td></tr><tr><td> click the login button below. Work from any device, anytime, and anywhere in the world.<p></p></td></tr><tr><td><p style='font-family:Open Sans, sans-serif;font-size:13px;' align='left'> <a rel='nofollow' target='_blank' href='https://app.almohami.com/Account/Login' style='display:inline-block;margin:20px 0 5px 0;padding:6px 20px;font-size:18px;line-height:27px;color:#ffffff;font-weight:500;text-decoration:none;background-color:#28b779;border:1px solid #28b779;border-radius:3px;'>Login</a></p></td></tr></tbody></table></td></tr></table><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td height='5'> &nbsp;</td></tr><tr><td class='column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='' style='display: inline-block; vertical-align: top; margin: 10px 20px'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br /> This technical notification has been generated automatically. Please do not reply. You received this email notification because you either signed up for a Almohami or purchased a commercial subscription / license key. Besides technical notification, we may also send you important product news and information about discounts and special offers from time to time (preferences for such non-technical communication can be managed separately). <br /></p></td></tr></table></div></td></tr><tr><td height='10'> &nbsp;</td></tr></table></td></tr></table></div></td></tr></table></center></body></html>";
                }
            }
            catch (Exception ex)
            {

            }
            return str;

        }
        #endregion

        public ActionResult Details(Int64 id)
        {
            UserEntityModel model = new UserEntityModel();
            model = _UserServices.GetUserById(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            UserEntityModel model = new UserEntityModel();
            model = _UserServices.GetUserById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Int64 id,UserEntityModel model)
        {
            if (ModelState.IsValid)
            {                
                model.UserID = id;
                model.IsDeleted = false;
                model.UserEmailConfirmed = false;
                model.UserPhoneConfirmed = false;
                model.Status = false;
                model.UserApprovedBy = id;
                model.CreatedBy = id;
                model.CreatedDate = DateTime.Now;
                _UserServices.AddorUpdateUser(model);               

                TempData["SuccessMessage"] = "";
                return RedirectToAction("Create", "User");
            }

            return View(model);
        }

        public ActionResult SaveandNew(Int64 id,UserEntityModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = id;
                model.IsDeleted = false;
                model.UserEmailConfirmed = false;
                model.UserPhoneConfirmed = false;
                model.Status = false;
                model.UserApprovedBy = id;
                model.CreatedBy = id;
                model.CreatedDate = DateTime.Now;
                _UserServices.AddorUpdateUser(model);

                TempData["SuccessMessage"] = "";
                return RedirectToAction("Create", "User");
            }

            return View(model);
        }
    }
}