using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using System.Web.Mvc;
using Almohami.Core.Helper;
using System;
using System.Web;
using AlmohamiWeb.OTPServiceReference;
using Almohami.Core.Enums;
using AlmohamiWeb.Security;
using AlmohamiWeb.App_Start;
using Almohami.Data.AlmohamiModel;
using AlmohamiWeb.Models;
namespace AlmohamiWeb.Controllers
{
    public class AccountController : BaseController
    {

        private readonly IAccountLoginServices _accountLoginService;
        private readonly IUserServices _userService;
        private readonly IDashboardService _dashboardService;

        public string MailFrom = "", MailTo = "", MailCC = "", MailBCC = "", Subject = "", Body = "", Attachment = "";
        public string MailFrom1 = "", MailTo1 = "", MailCC1 = "", MailBCC1 = "", Subject1 = "", Body1 = "", Attachment1 = "";

        public bool IsBodyHtml = true;
        public AccountController()
        {
            _accountLoginService = new AccountLoginServices();
            _userService = new UserService();
            _dashboardService = new DashboardService();
        }

        #region Login
        //
        // GET: /Account/Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            LoginModel loginModel = new LoginModel();
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            if (Request.Cookies["Login"] != null)
            {
                loginModel.Email = Request.Cookies["Login"].Values["EmailID"];
                loginModel.Password = Request.Cookies["Login"].Values["Password"];
            }
            return View();
        }

        [ValidateAjax]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel logon)
        {
            var getuser = _accountLoginService.getUser(logon.Email);
            if (getuser != null)
            {
                var hashcode = getuser.UserVCode;
                //var encodingpwdstring = Helpers.EncodePassword(logon.Password, hashcode);
                //logon.Password = encodingpwdstring;
                var userDetails = _accountLoginService.CheckLogin(logon);
                if (userDetails != null)
                {
                    if ((bool)userDetails.UserEmailConfirmed)
                    {
                        if ((bool)userDetails.UserPhoneConfirmed)
                        {
                            if (logon.RememberMe)
                            {
                                RememberMe(userDetails.UserEmailId, userDetails.UserPassword);

                            }
                            long logid = userlogDetail(userDetails);
                            FormAuthentication.StoreCurrentUserData(userDetails, logid, logon.RememberMe);
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Please Confirm Phone Number!");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Please Confirm Email Id!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserId or password in not correct!!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email Id");
            }
            return View();
        }
       
        public void RememberMe(string emailId, string password)
        {

            // cookie expire after 1 day

            HttpCookie cookie = new HttpCookie("Login");
            cookie.Values.Add("EmailID", emailId);
            cookie.Values.Add("Password", password);
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
        }
        public long userlogDetail(User userDetails)
        {
            UserLogEntityModel userlog = new UserLogEntityModel();
            userlog.UserLogUserID = userDetails.UserId;
            userlog.UserLogRole = Convert.ToInt64(userDetails.UserRoleId);
            userlog.UserStatus = userDetails.UserStatus;
            userlog.CreatedDate = DateTime.Now;
            userlog.CreatedBy = userDetails.UserId;
            long logid = _userService.AddorUpdateUserLog(userlog);
            return logid;
        }
        #endregion

        #region Registration

        //
        // GET: /Account/Registration
        [HttpGet]
        public ActionResult Registration()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }

        //
        // POST: /Account/Registration
        [HttpPost]
        [ValidateAjax]       
        public ActionResult Registration(RegisterModel model)
        {
            if (_accountLoginService.CheckEmailIdExist(model))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                model.HiddenOTP = CommonHelp.GetVerificationCode();
                model.MobileNo = "965" + model.MobileNo;
                var keyNew = Helpers.GeneratePassword(6);
                var pwd = Helpers.EncodePassword(model.Password, keyNew);
                model.Password = pwd;
                model.UserVCode = keyNew;
                model.UserEmailConfirmed = false;
                model.UserPhoneConfirmed = false;
                model.UserStatus = false;
                model.UserDelete = false;
                model = _accountLoginService.AddUser(model);

                MailFrom = "";
                MailTo = model.Email;
                Subject = "Please confirm your email address";
                Body = BindTemplate(model.Email, model.Name, model.UserID);
                CommonHelp.SendMail(MailFrom, MailTo, MailCC, MailBCC, Subject, Body, Attachment, IsBodyHtml);

                SendOTP(model.HiddenOTP, model.MobileNo);
                return PartialView("~/Views/Account/_SendOTP.cshtml", model);
            }

        }

        public SendingSMSResult SendOTP(string OTPMsg, string MobileNo)
        {

            MessagingSoapClient messagingClient = new MessagingSoapClient();

            SendingSMSResult result = null;
            DateTime? defDate = null;
            SoapUser user = new SoapUser()
            {
                Username = OTPConsts.OTPUserName.ToString(),
                Password = OTPConsts.OTPPassword.ToString(),
                CustomerId = Convert.ToInt32(OTPConsts.OTPCustomerId)

            };
            //result = messagingClient.AuthenticateUser(user);

            var sendMessageRequest = new SendingSMSRequest()
            {

                User = user,
                IsBlink = false,
                IsFlash = false,
                MessageBody = OTPConsts.OTPMsgBody +" " + OTPMsg,
                SenderText = OTPConsts.OTPSenderText,

                /*Testing no.*/
                // RecipientNumbers = OTPConsts.OTPTestRecipientNumbers,

                // Live no.
                RecipientNumbers = MobileNo,
                DefDate = defDate
            };

            result = messagingClient.SendSMS(sendMessageRequest);
            return result;
        }
        public ActionResult CheckOTP(string OTP, int userId)
        {
            if (_accountLoginService.CheckCurrentOTP(OTP, userId))
            {
                if (_accountLoginService.updateUserPhoneConfirm(userId))
                {
                    TempData["SuccessMessage"] = "Thank You Go to your inbox and confirm your mail.";
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResendOTP(int userId, string mobileNo)
        {

            RegisterModel registerModel = new RegisterModel();
            registerModel.HiddenOTP = CommonHelp.GetVerificationCode();
            SendOTP(registerModel.HiddenOTP, mobileNo);
            if (_accountLoginService.UpdateUserOTP(userId, registerModel.HiddenOTP))
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);


        }
    
        public ActionResult ConfirmEmail(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Int64 UserId = Convert.ToInt64(CommonHelp.Decryptdata(id));
                    if (UserId > 0)
                    {
                        LoginModel loginmodel = _accountLoginService.GetUserById(UserId);
                        if (loginmodel != null)
                        {
                            UserEntityModel userentitymodel = new UserEntityModel();
                            bool flag = _accountLoginService.ActiveUser(UserId);
                            if (flag == true)
                            {
                                MailFrom1 = "";
                                MailTo1 = loginmodel.Email;
                                Subject1 = "Welcome to Almohami!";
                                Body1 = BindWelcomeTemplate(loginmodel.Email, loginmodel.Name, loginmodel.UserID);
                                CommonHelp.SendMail(MailFrom1, MailTo1, MailCC1, MailBCC1, Subject1, Body1, Attachment1, IsBodyHtml);
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Error!!";
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "User Not Found!";
                        }
                        TempData["SuccessMessage"] = "Thank you for signing up with Almohami! We're excited to review your application. Once our team will review it and get back to you about next steps!";
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }

        #region BindTemplate
        protected string BindTemplate(string mailid, string Name, Int64 id)
        {
            string  encryptname, encryptid, str = "";
            try
            {
                //   Randomno = CommonHelp.GetRandomCode();

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
                    str = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><meta name='viewport' content='width=device-width, initial-scale=1.0' /><title></title><style type='text/css'>*{-webkit-font-smoothing:antialiased}body{Margin:0;padding:0;min-width:100%;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased;mso-line-height-rule:exactly}table{border-spacing:0;color:#333;font-family:Arial,sans-serif}img{border:0}.wrapper{width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}.webkit{max-width:600px}.outer{Margin:0 auto;width:100%;max-width:600px}.full-width-image img{width:100%;max-width:600px;height:auto}.inner{padding:10px}p{Margin:0;padding-bottom:10px}.h1{font-size:21px;font-weight:bold;Margin-top:15px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.h2{font-size:18px;font-weight:bold;Margin-top:10px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column .contents{text-align:left;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column p{font-size:13px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.two-column{text-align:center;font-size:0}.two-column .column{width:100%;max-width:300px;display:inline-block;vertical-align:top}.contents{width:100%}.two-column .contents{font-size:13px;text-align:left}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding-top:10px}.three-column{text-align:center;font-size:0;padding-top:10px;padding-bottom:10px}.three-column .column{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .contents{font-size:13px;text-align:center}.three-column img{width:100%;max-width:180px;height:auto}.three-column .text{padding-top:10px}.img-align-vertical img{display:inline-block;vertical-align:middle}@media only screen and (max-device-width: 480px){table[class=hide],img[class=hide],td[class=hide]{display:none !important}.contents1{width:100%}.contents1{width:100%}}</style></head><body style='Margin: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; min-width: 100%; background-color: #f3f2f0;'><center class='wrapper' style='width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; background-color: #f3f2f0;'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: #f3f2f0;' bgcolor='#f3f2f0;'><tr><td width='100%'><div class='webkit' style='max-width: 600px; Margin: 0 auto;'><table class='outer' align='center' cellpadding='0' cellspacing='0' border='1' style='border: 5px solid #0B3C5D; Margin: 0 auto; width: 100%; max-width: 600px;'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;'><table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><table style='width: 100%;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td align='center'><center><table border='0' align='center' width='100%' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td class='one-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;' bgcolor='#FFFFFF'><table cellpadding='0' cellspacing='0' border='0' width='100%' bgcolor='#42A0D6'><tr><td class='two-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='column' style='width: 100%; display: inline-block; vertical-align: top;'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 5px;' align='center'><a href='#' target='_blank'> <img src='https://app.almohami.com/Content/layout3/img/logo-default.png' alt='' width='100%' height='auto' style='border-width: 0; display: block' align='center' /></a></td></tr></table></div></td></tr></table></td></tr></tbody></table></center></td></tr></tbody></table></td></tr></table><table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-spacing: 0; border-left: 1px solid #e8e7e5; border-right: 1px solid #e8e7e5; border-bottom: 1px solid #e8e7e5; border-top: 1px solid #e8e7e5' bgcolor='#FFFFFF'><tr><td align='left' style='padding: 20px 30px 5px 30px'><p style='color: #0B3C5D; font-size: 24px; text-align: center; font-family: Open Sans,sans-serif'> <strong>Welcome to Almohami!</strong></p><p style='color: #262626; font-size: 13px; text-align: left; font-family: Open Sans,sans-serif'> <strong> Hi " + mailid.ToString() + ", </strong></p><p style='color: #000000; font-size: 13px; text-align: left; font-family: Open Sans, sans-serif; line-height: 18px'> Thank you for signing up with Almohami! We're excited to review your application. Once our team will review it and get back to you about next steps!<br/><br/>Thank you and best regards,<br/> Your Almohami Team</p></td></tr></table><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td height='5'> &nbsp;</td></tr><tr><td class='column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='' style='display: inline-block; vertical-align: top; margin: 10px 20px'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br /> This technical notification has been generated automatically. Please do not reply. You received this email notification because you signed up for a Almohami . Besides technical notification, we may also send you important product news and information about discounts and special offers from time to time (preferences for such non-technical communication can be managed separately). <br /></p></td></tr></table></div></td></tr><tr><td height='10'> &nbsp;</td></tr></table></td></tr></table></div></td></tr></table></center></body></html>";
                    //str += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><meta name='viewport' content='width=device-width, initial-scale=1.0' /><title></title><style type='text/css'>*{-webkit-font-smoothing:antialiased}body{Margin:0;padding:0;min-width:100%;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased;mso-line-height-rule:exactly}table{border-spacing:0;color:#333;font-family:Arial,sans-serif}img{border:0}.wrapper{width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}.webkit{max-width:600px}.outer{Margin:0 auto;width:100%;max-width:600px}.full-width-image img{width:100%;max-width:600px;height:auto}.inner{padding:10px}p{Margin:0;padding-bottom:10px}.h1{font-size:21px;font-weight:bold;Margin-top:15px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.h2{font-size:18px;font-weight:bold;Margin-top:10px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column .contents{text-align:left;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column p{font-size:14px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.two-column{text-align:center;font-size:0}.two-column .column{width:100%;max-width:300px;display:inline-block;vertical-align:top}.contents{width:100%}.two-column .contents{font-size:14px;text-align:left}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding-top:10px}.three-column{text-align:center;font-size:0;padding-top:10px;padding-bottom:10px}.three-column .column{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .contents{font-size:14px;text-align:center}.three-column img{width:100%;max-width:180px;height:auto}.three-column .text{padding-top:10px}.img-align-vertical img{display:inline-block;vertical-align:middle}@media only screen and (max-device-width: 480px){table[class=hide],img[class=hide],td[class=hide]{display:none !important}.contents1{width:100%}.contents1{width:100%}}</style></head><body style='Margin: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; min-width: 100%; background-color: #f3f2f0;'><center class='wrapper' style='width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; background-color: #f3f2f0;'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: #f3f2f0;' bgcolor='#f3f2f0;'><tr><td width='100%'><div class='webkit' style='max-width: 600px; Margin: 0 auto;'><table class='outer' align='center' cellpadding='0' cellspacing='0' border='1' style='border: 5px solid #0B3C5D; Margin: 0 auto; width: 100%; max-width: 600px;'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;'><table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><table style='width: 100%;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td align='center'><center><table border='0' align='center' width='100%' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td class='one-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;' bgcolor='#FFFFFF'><table cellpadding='0' cellspacing='0' border='0' width='100%' bgcolor='#42A0D6'><tr><td class='two-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='column' style='width: 100%; display: inline-block; vertical-align: top;'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 5px;' align='center'><a href='https://app.almohami.com/' target='_blank'> <img src='https://app.almohami.com/Content/layout3/img/logo-default.png' alt='' width='100%' height='auto' style='border-width: 0; display: block' align='center' /></a></td></tr></table></div></td></tr></table></td></tr></tbody></table></center></td></tr></tbody></table></td></tr></table><table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-spacing: 0; border-left: 1px solid #e8e7e5; border-right: 1px solid #e8e7e5; border-bottom: 1px solid #e8e7e5; border-top: 1px solid #e8e7e5' bgcolor='#FFFFFF'><tr><td align='left' style='padding: 20px 30px 5px 30px'><p style='color: #0B3C5D; font-size: 24px; text-align: center; font-family: Open Sans,sans-serif'> <strong>Welcome to Almohami!</strong></p><p style='color: #262626; font-size: 14px; text-align: left; font-family: Open Sans,sans-serif'> <strong> Hi " + Name.ToString() + ", </strong></p><p style='color: #000000; font-size: 14px; text-align: left; font-family: Open Sans,sans-serif; line-height: 18px'> Congratulations,on taking the first big step to managing your business. We're excited for you to see how easy it is to get started.</p><p style='color: #000000; font-size: 14px; text-align: left; font-family: Open Sans, sans-serif; line-height: 18px'> As an approved member of the Almohami, you have been granted full access to Almohami Central-the online resource area for Almohami.<br /> Your account details are:</p><table border='0' align='left' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td align='left'><table border='0' cellpadding='0' cellspacing='0' style='background-color: #D6EBFF;margin:10px;padding:10px;border:solid #b5b4b4 1px;color:#808080'><tr><td colspan='3' height='20'> Email: 	" + mailid.ToString() + "</td></tr><tr><td colspan='3' height='20'> Login: 	https://app.almohami.com</td></tr><tr><td colspan='3' height='20'> Questions: 	'http://www.almohami.com/support/' </td></tr></table></td></tr><tr><td> click the login button below. Work from any device, anytime, and anywhere in the world.<p></p></td></tr><tr><td><p style='font-family:Open Sans, sans-serif;font-size:13px;' align='left'> <a rel='nofollow' target='_blank' href='https://app.almohami.com/Account/Login' style='display:inline-block;margin:20px 0 5px 0;padding:6px 20px;font-size:18px;line-height:27px;color:#ffffff;font-weight:500;text-decoration:none;background-color:#28b779;border:1px solid #28b779;border-radius:3px;'>Login</a></p></td></tr></tbody></table></td></tr></table><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td height='5'> &nbsp;</td></tr><tr><td class='column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='' style='display: inline-block; vertical-align: top; margin: 10px 20px'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br /> This technical notification has been generated automatically. Please do not reply. You received this email notification because you either signed up for a Almohami or purchased a commercial subscription / license key. Besides technical notification, we may also send you important product news and information about discounts and special offers from time to time (preferences for such non-technical communication can be managed separately). <br /></p></td></tr></table></div></td></tr><tr><td height='10'> &nbsp;</td></tr></table></td></tr></table></div></td></tr></table></center></body></html>";
                }
            }
            catch (Exception ex)
            {

            }
            return str;

        }
        #endregion

        #endregion

        #region View Registration Requests
        public ActionResult ViewRegistrationRequest()
        {
            UserEntityModel userentitymodel = new UserEntityModel();
            userentitymodel.UsersList = _userService.GetAllInactiveUsers(userentitymodel);
            return View(userentitymodel);
        }
        public ActionResult ApproveUser(int id)
        {
            try
            {
                UserEntityModel userentitymodel = _userService.GetUserById(id);
                if (userentitymodel != null)
                {
                    userentitymodel.UserApprovedBy = 1;
                    userentitymodel.Status = true;
                    userentitymodel.IsDeleted = false;
                    _userService.AddorUpdateUser(userentitymodel);

                    MailFrom = "";
                    MailTo = userentitymodel.Email;
                    Subject = "Your Application Request has been approved";
                    Body = BindApprovalTemplate(userentitymodel.Email, userentitymodel.Name, userentitymodel.UserID);
                    CommonHelp.SendMail(MailFrom, MailTo, MailCC, MailBCC, Subject, Body, Attachment, IsBodyHtml);

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RejectUser(int id)
        {
            try
            {
                UserEntityModel userentitymodel = _userService.GetUserById(id);
                if (userentitymodel != null)
                {
                    userentitymodel.UserRejectedBy = 1;
                    userentitymodel.Status = false;
                    userentitymodel.IsDeleted = true;
                    _userService.AddorUpdateUser(userentitymodel);

                    MailFrom = "";
                    MailTo = userentitymodel.Email;
                    Subject = "Your Registration Request Rejected";
                    Body = BindRejectionTemplate(userentitymodel.Email, userentitymodel.Name, userentitymodel.UserID);
                    CommonHelp.SendMail(MailFrom, MailTo, MailCC, MailBCC, Subject, Body, Attachment, IsBodyHtml);

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #region BindApprovalTemplate
        protected string BindApprovalTemplate(string mailid, string Name, Int64 id)
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

        #region BindRejectionTemplate
        protected string BindRejectionTemplate(string mailid, string Name, Int64 id)
        {
            string str = "";
            try
            {
                if (mailid != "" && Name != "")
                {
                    str += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><meta name='viewport' content='width=device-width, initial-scale=1.0' /><title></title><style type='text/css'>*{-webkit-font-smoothing:antialiased}body{Margin:0;padding:0;min-width:100%;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased;mso-line-height-rule:exactly}table{border-spacing:0;color:#333;font-family:Arial,sans-serif}img{border:0}.wrapper{width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}.webkit{max-width:600px}.outer{Margin:0 auto;width:100%;max-width:600px}.full-width-image img{width:100%;max-width:600px;height:auto}.inner{padding:10px}p{Margin:0;padding-bottom:10px}.h1{font-size:21px;font-weight:bold;Margin-top:15px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.h2{font-size:18px;font-weight:bold;Margin-top:10px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column .contents{text-align:left;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column p{font-size:13px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.two-column{text-align:center;font-size:0}.two-column .column{width:100%;max-width:300px;display:inline-block;vertical-align:top}.contents{width:100%}.two-column .contents{font-size:13px;text-align:left}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding-top:10px}.three-column{text-align:center;font-size:0;padding-top:10px;padding-bottom:10px}.three-column .column{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .contents{font-size:13px;text-align:center}.three-column img{width:100%;max-width:180px;height:auto}.three-column .text{padding-top:10px}.img-align-vertical img{display:inline-block;vertical-align:middle}@media only screen and (max-device-width: 480px){table[class=hide],img[class=hide],td[class=hide]{display:none !important}.contents1{width:100%}.contents1{width:100%}}</style></head><body style='Margin: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; min-width: 100%; background-color: #f3f2f0;'><center class='wrapper' style='width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; background-color: #f3f2f0;'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: #f3f2f0;' bgcolor='#f3f2f0;'><tr><td width='100%'><div class='webkit' style='max-width: 600px; Margin: 0 auto;'><table class='outer' align='center' cellpadding='0' cellspacing='0' border='1' style='border: 5px solid #0B3C5D; Margin: 0 auto; width: 100%; max-width: 600px;'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;'><table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><table style='width: 100%;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td align='center'><center><table border='0' align='center' width='100%' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td class='one-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;' bgcolor='#FFFFFF'><table cellpadding='0' cellspacing='0' border='0' width='100%' bgcolor='#42A0D6'><tr><td class='two-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='column' style='width: 100%; display: inline-block; vertical-align: top;'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 5px;' align='center'><a href='#' target='_blank'> <img src='https://app.almohami.com/Content/layout3/img/logo-default.png' alt='' width='100%' height='auto' style='border-width: 0; display: block' align='center' /></a></td></tr></table></div></td></tr></table></td></tr></tbody></table></center></td></tr></tbody></table></td></tr></table><table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-spacing: 0; border-left: 1px solid #e8e7e5; border-right: 1px solid #e8e7e5; border-bottom: 1px solid #e8e7e5; border-top: 1px solid #e8e7e5' bgcolor='#FFFFFF'><tr><td align='left' style='padding: 20px 30px 5px 30px'><p style='color: #0B3C5D; font-size: 24px; text-align: center; font-family: Open Sans,sans-serif'> <strong>Almohami</strong></p><p style='color: #262626; font-size: 13px; text-align: left; font-family: Open Sans,sans-serif'> <strong>Hi, </strong></p><p style='color: #000000; font-size: 13px; text-align: left; font-family: Open Sans, sans-serif; line-height: 18px'> Thank you for your interest in Almohami. We genuinely appreciate your efforts and commitment. Unfortunately, We have decided not to move forward with your application at this time as it does not meet our current terms. We wish you the best in your future endeavors. <br /><br />Thank you and best regards, <br />Your Almohami Team</p></td></tr></table><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td height='5'> &nbsp;</td></tr><tr><td class='column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='' style='display: inline-block; vertical-align: top; margin: 10px 20px'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br /> This technical notification has been generated automatically. Please do not reply. You received this email notification because you signed up for a Almohami . Besides technical notification, we may also send you important product news and information about discounts and special offers from time to time (preferences for such non-technical communication can be managed separately). <br /></p></td></tr></table></div></td></tr><tr><td height='10'> &nbsp;</td></tr></table></td></tr></table></div></td></tr></table></center></body></html>";
                }
            }
            catch (Exception ex)
            {

            }
            return str;

        }
        #endregion

        #endregion

        #region Forgot Password
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordEntityModel forgotpwdmodel = new ForgotPasswordEntityModel();
            return View(forgotpwdmodel);
        }
        [HttpPost]
        [ValidateAjax]
        public ActionResult ForgotPassword(ForgotPasswordEntityModel forgotpasswordmodel)
        {
            if (ModelState.IsValid)
            {
                var getuser = _accountLoginService.getUser(forgotpasswordmodel.Email);
                if (getuser != null)
                {
                    var token = CommonHelp.GetRandomCode(8);
                    string identity = Helpers.base64Encode(getuser.UserId.ToString());
                    //forgotpasswordmodel.PasswordVerificationToken = token;
                    //forgotpasswordmodel.PasswordVerificationTokenExpirationDate = DateTime.Now;
                    DateTime expirydate = DateTime.Now.AddHours(24);
                    if (_accountLoginService.UpdatePasswordToken(getuser.UserId, token, expirydate))
                    {
                        var resetlink = "<a href='" + Url.Action("ResetPassword", "Account", new { User = identity, ResetToken = token, IsResetTokenInvalid = false }, "https://app.almohami.com") + "'  style='width: 250; display: block; text-decoration: none; border: 0; text-align: center; font-weight: bold; font-size: 18px; font-family: Arial, sans-serif; color: #ffffff' class='button_link'>Reset Password</a>";
                        MailFrom = "";
                        MailTo = getuser.UserEmailId;
                        Subject = "Password Reset Request";
                        Body = BindForgotPasswordTemplate(getuser.UserEmailId, getuser.UserName, getuser.UserId, resetlink);
                        CommonHelp.SendMail(MailFrom, MailTo, MailCC, MailBCC, Subject, Body, Attachment, IsBodyHtml);
                        return RedirectToAction("ForgotPasswordConfirmation");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email Id not found! You may have signed up with a different email address.");
                }
            }
            return View();
        }
        public string BindForgotPasswordTemplate(string mailid, string Name, Int64 id, string resetlink)
        {
            string str = "";
            try
            {
                if (mailid != "" && Name != "")
                {
                    str = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><meta name='viewport' content='width=device-width, initial-scale=1.0' /><title></title><style type='text/css'>*{-webkit-font-smoothing:antialiased}body{Margin:0;padding:0;min-width:100%;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased;mso-line-height-rule:exactly}table{border-spacing:0;color:#333;font-family:Arial,sans-serif}img{border:0}.wrapper{width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}.webkit{max-width:600px}.outer{Margin:0 auto;width:100%;max-width:600px}.full-width-image img{width:100%;max-width:600px;height:auto}.inner{padding:10px}p{Margin:0;padding-bottom:10px}.h1{font-size:21px;font-weight:bold;Margin-top:15px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.h2{font-size:18px;font-weight:bold;Margin-top:10px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column .contents{text-align:left;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.one-column p{font-size:13px;Margin-bottom:5px;font-family:Arial,sans-serif;-webkit-font-smoothing:antialiased}.two-column{text-align:center;font-size:0}.two-column .column{width:100%;max-width:300px;display:inline-block;vertical-align:top}.contents{width:100%}.two-column .contents{font-size:13px;text-align:left}.two-column img{width:100%;max-width:280px;height:auto}.two-column .text{padding-top:10px}.three-column{text-align:center;font-size:0;padding-top:10px;padding-bottom:10px}.three-column .column{width:100%;max-width:200px;display:inline-block;vertical-align:top}.three-column .contents{font-size:13px;text-align:center}.three-column img{width:100%;max-width:180px;height:auto}.three-column .text{padding-top:10px}.img-align-vertical img{display:inline-block;vertical-align:middle}@media only screen and (max-device-width: 480px){table[class=hide],img[class=hide],td[class=hide]{display:none !important}.contents1{width:100%}.contents1{width:100%}}</style></head><body style='Margin: 0; padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; min-width: 100%; background-color: #f3f2f0;'><center class='wrapper' style='width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; background-color: #f3f2f0;'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: #f3f2f0;' bgcolor='#f3f2f0;'><tr><td width='100%'><div class='webkit' style='max-width: 600px; Margin: 0 auto;'><table class='outer' align='center' cellpadding='0' cellspacing='0' border='1' style='border: 5px solid #0B3C5D; Margin: 0 auto; width: 100%; max-width: 600px;'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;'><table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><table style='width: 100%;' cellpadding='0' cellspacing='0' border='0'><tbody><tr><td align='center'><center><table border='0' align='center' width='100%' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tbody><tr><td class='one-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0;' bgcolor='#FFFFFF'><table cellpadding='0' cellspacing='0' border='0' width='100%' bgcolor='#42A0D6'><tr><td class='two-column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='column' style='width: 100%; display: inline-block; vertical-align: top;'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 5px;' align='center'> <a href='#' target='_blank'> <img src='https://app.almohami.com/Content/layout3/img/logo-default.png' alt='' width='100%' height='auto' style='border-width: 0; display: block' align='center' /></a></td></tr></table></div></td></tr></table></td></tr></tbody></table></center></td></tr></tbody></table></td></tr></table><table class='one-column' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-spacing: 0; border-left: 1px solid #e8e7e5; border-right: 1px solid #e8e7e5; border-bottom: 1px solid #e8e7e5; border-top: 1px solid #e8e7e5' bgcolor='#FFFFFF'><tr><td align='left' style='padding: 20px 30px 5px 30px'><p style='color: #0B3C5D; font-size: 24px; text-align: center; font-family: Open Sans,sans-serif'> <strong>Reset your password</strong></p><p style='color: #262626; font-size: 13px; text-align: left; font-family: Open Sans,sans-serif'> <strong>Hi " + Name.ToString() + ", </strong></p><p style='color: #000000; font-size: 13px; text-align: left; font-family: Open Sans, sans-serif; line-height: 18px'> We received a request to reset your password. If you did not submit this request, simply ignore this email.</p></td></tr><tr><td align='center'><table border='0' cellpadding='0' cellspacing='0' style='Margin: 0 auto;'><tr><td><div style='padding: 10px; margin-bottom:10px; background-color: #28B779; text-align: center'> " + resetlink + "</div></td></tr></table></td></tr></table><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td height='5'> &nbsp;</td></tr><tr><td class='column' style='padding-top: 0; padding-bottom: 0; padding-right: 0; padding-left: 0; text-align: center; font-size: 0;'><div class='' style='display: inline-block; vertical-align: top; margin: 10px 20px'><table class='contents' style='border-spacing: 0; width: 100%'><tr><td width='100%' align='left' valign='middle' style='padding-top: 0; padding-bottom: 0; padding-right: 20px; padding-left: 0;'><p style='color: #b5b4b4; font-size: 12px; text-align: left; font-family: Open Sans, sans-serif'> <br /> This technical notification has been generated automatically. Please do not reply. You received this email notification because you signed up for a Almohami . Besides technical notification, we may also send you important product news and information about discounts and special offers from time to time (preferences for such non-technical communication can be managed separately). <br /></p></td></tr></table></div></td></tr><tr><td height='10'> &nbsp;</td></tr></table></td></tr></table></div></td></tr></table></center></body></html>";
                }
            }
            catch (Exception ex)
            {

            }
            return str;
        }

        public ActionResult ForgotPasswordConfirmation()
        {
            return View("ForgotPasswordConfirmation");
        }
        #endregion

        #region Reset Password
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string User, string ResetToken, string IsResetTokenInvalid)
        {
            //long userid = Convert.ToInt64(Helpers.base64Decode(User));
            //var userdata = _accountLoginService.CheckTokenAndId(userid, ResetToken);
            //if (userdata != null)
            //{
            //    if (userdata.PasswordVerificationTokenExpirationDate > DateTime.Now)
            //    {
            //        ResetPasswordEntityModel resetpwdentitymodel = new ResetPasswordEntityModel();
            //        resetpwdentitymodel.Name = userdata.UserName;
            //        resetpwdentitymodel.Email = userdata.UserEmailId;
            //        resetpwdentitymodel.Id = userid;
            //        resetpwdentitymodel.IsResetTokenInvalid = true;

            //        return View(resetpwdentitymodel);
            //    }
            //    else
            //    {
            //        TempData["ErrorMessage"] = "Invalid";
            //        var resetlink = "<a href='" + Url.Action("ResetPassword", "Account", new { User = userdata.UserId, ResetToken = ResetToken, IsResetTokenInvalid = false }, "https://app.almohami.com") + "'  style='width: 250; display: block; text-decoration: none; border: 0; text-align: center; font-weight: bold; font-size: 18px; font-family: Arial, sans-serif; color: #ffffff' class='button_link'>Reset Password</a>";
            //        MailFrom = "";
            //        MailTo = userdata.UserEmailId;
            //        Subject = "Password Reset Request";
            //        Body = BindForgotPasswordTemplate(userdata.UserEmailId, userdata.UserName, userdata.UserId, resetlink);
            //        CommonHelp.SendMail(MailFrom, MailTo, MailCC, MailBCC, Subject, Body, Attachment, IsBodyHtml);
            //        return View("ResetPasswordConfirmation");
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "User Not Found!");
            //    return View();
            //}
            return View();
        }
        [HttpPost]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordEntityModel resetpwdentitymodel)
        {
            if (ModelState.IsValid)
            {
                var keyNew = Helpers.GeneratePassword(6);
                var pwd = Helpers.EncodePassword(resetpwdentitymodel.Password, keyNew);
                resetpwdentitymodel.Password = pwd;
                resetpwdentitymodel.UserVCode = keyNew;

                if (_accountLoginService.UpdatePassword(resetpwdentitymodel.Id, resetpwdentitymodel.Password, resetpwdentitymodel.UserVCode))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View(resetpwdentitymodel);
                }
            }
            return View(resetpwdentitymodel);

        }
        #endregion

        public ActionResult ChangeCurrentCulture(int culture)
        {
            // Change the current culture for this user.
            //
            //SiteSession.CurrentUICulture = culture;
            //
            // Cache the new current culture into the user HTTP session. 
            //
            Session["CurrentUICulture"] = culture;
            //
            // Redirect to the same page from where the request was made! 
            //
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}