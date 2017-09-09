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
using System.Web.UI.WebControls;

namespace AlmohamiWeb.Controllers
{
    public class CaseController : AlmohamiController
    {
        #region Private Variables
        private readonly ICaseServices _CaseServices;
        private readonly IUserServices _UserServices;
        private readonly IClientService _ClientServices;
        #endregion

        #region Controller
        public CaseController()
        {
            _CaseServices = new CaseServices();
            _UserServices = new UserService();
            _ClientServices = new ClientService();
        }
        #endregion

        // GET: User

        public ActionResult List()
        {
            CaseEntityModel caseentitymodel = new CaseEntityModel();
            caseentitymodel.CaseStatus = true;
            caseentitymodel.CaseUserId = User.UserId;
            caseentitymodel.CaseList = _CaseServices.GetCasesByUserIdandStatus(caseentitymodel);
            return View(caseentitymodel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CaseEntityModel caseentitymodel = new CaseEntityModel();
            Int64 Userid = User.UserId;
            bool flag = _CaseServices.CheckUserHavingCase(Userid);
            bool IncrementCaseNO = false;
            if (flag == true)
            {
                caseentitymodel.CaseUserId = Userid;
                string caseno = _CaseServices.GetIncrementedCaseNo(caseentitymodel);

                string NoOnly = caseno.Replace("FileNo-", "");
                Int64 NO = Convert.ToInt64(NoOnly);
                NO = NO + 1;
                caseentitymodel.CaseNo = NO.ToString();
                IncrementCaseNO = true;
            }
            string globalno = _CaseServices.GetIncrementedGlobalNo(caseentitymodel);
            globalno = globalno.Replace("GeneralNo-", "");
            Int64 GeneralNo = Convert.ToInt64(globalno);
            GeneralNo = GeneralNo + 1;
            caseentitymodel.CaseGlobalNo = GeneralNo.ToString();

            ViewBag.Flag = IncrementCaseNO;

            UserEntityModel userentitymodel = new UserEntityModel();
            caseentitymodel.UserList = _UserServices.GetAllActiveUsers(userentitymodel);

            //caseentitymodel.UserList.Insert(0, (new SelectListItem { Text = "All Individuals", Value = "0.0", Selected = true }));

            ClientEntityModel cliententitymodel = new ClientEntityModel();
            caseentitymodel.ClientList = _ClientServices.GetAllActiveClient(cliententitymodel);

            CaseAppealStatusEntityModel caseappealentitymodel = new CaseAppealStatusEntityModel();
            caseentitymodel.CaseAppealList = _CaseServices.GetAllAppealStatus(caseappealentitymodel);

            CaseDiscriminationStatusEntityModel casediscriminationentitymodel = new CaseDiscriminationStatusEntityModel();
            caseentitymodel.CaseDiscriminationList = _CaseServices.GetAllDiscriminationStatus(casediscriminationentitymodel);

            CasePrimaryStatusEntityModel caseprimaryentitymodel = new CasePrimaryStatusEntityModel();
            caseentitymodel.CasePrimaryStatusList = _CaseServices.GetAllPrimaryStatus(caseprimaryentitymodel);

            CaseTypeEntityModel casetypeentitymodel = new CaseTypeEntityModel();
            caseentitymodel.CaseTypeList = _CaseServices.GetAllCaseType(casetypeentitymodel);

            CourtEntityModel courtentitymodel = new CourtEntityModel();
            caseentitymodel.CourtList = _CaseServices.GetAllCourt(courtentitymodel);

            return View(caseentitymodel);
        }

        [HttpPost]
        public ActionResult Create(CaseEntityModel caseentitymodel)
        {
            if (ModelState.IsValid)
            {
                Int64 Userid = User.UserId;
                caseentitymodel.CaseUserId = Userid;
                caseentitymodel.CaseNo = "FileNo-" + caseentitymodel.CaseNo;
                caseentitymodel.CaseGlobalNo = "GeneralNo-" + caseentitymodel.CaseGlobalNo;
                caseentitymodel.CaseStatus = true;
                caseentitymodel.CaseDelete = false;
                caseentitymodel.CaseCreatedBy = User.UserId;
                _CaseServices.AddOrUpdateCase(caseentitymodel);
                return RedirectToAction("List", "Case");
            }
            return View(caseentitymodel);
        }

        [HttpPost]
        public ActionResult SaveAndNew(CaseEntityModel caseentitymodel)
        {
            if (ModelState.IsValid)
            {
                Int64 Userid = User.UserId;
                caseentitymodel.CaseUserId = Userid;
                caseentitymodel.CaseNo = "FileNo-" + caseentitymodel.CaseNo;
                caseentitymodel.CaseGlobalNo = "GeneralNo-" + caseentitymodel.CaseGlobalNo;
                caseentitymodel.CaseStatus = true;
                caseentitymodel.CaseDelete = false;
                caseentitymodel.CaseCreatedBy = User.UserId;
                _CaseServices.AddOrUpdateCase(caseentitymodel);
                return RedirectToAction("Create", "Case");
            }
            return View(caseentitymodel);
        }

        [HttpPost]
        public ActionResult UpdateAndNew(Int64 id, CaseEntityModel caseentitymodel)
        {
            if (ModelState.IsValid)
            {
                caseentitymodel.CaseId = id;
                Int64 Userid = User.UserId;
                caseentitymodel.CaseUserId = Userid;
                caseentitymodel.CaseNo = "FileNo-" + caseentitymodel.CaseNo;
                caseentitymodel.CaseLastModifiedBy = User.UserId;
                _CaseServices.AddOrUpdateCase(caseentitymodel);
                ModelState.Clear();
                return RedirectToAction("Create", "Case");
            }
            return View(caseentitymodel);
        }

        [HttpGet]
        public ActionResult Edit(string CaseId)
        {
            CaseEntityModel caseentitymodel = new CaseEntityModel();
            //Int64 id = Convert.ToInt64(Helpers.base64Decode(CaseId));
            caseentitymodel = _CaseServices.GetCaseById(12);
            if (caseentitymodel != null)
            {

                string NoOnly = caseentitymodel.CaseNo.Replace("FileNo-", "");
                caseentitymodel.CaseNo = NoOnly;

                UserEntityModel userentitymodel = new UserEntityModel();
                caseentitymodel.UserList = _UserServices.GetAllActiveUsers(userentitymodel);

                ClientEntityModel cliententitymodel = new ClientEntityModel();
                caseentitymodel.ClientList = _ClientServices.GetAllActiveClient(cliententitymodel);

                CaseAppealStatusEntityModel caseappealentitymodel = new CaseAppealStatusEntityModel();
                caseentitymodel.CaseAppealList = _CaseServices.GetAllAppealStatus(caseappealentitymodel);

                CaseDiscriminationStatusEntityModel casediscriminationentitymodel = new CaseDiscriminationStatusEntityModel();
                caseentitymodel.CaseDiscriminationList = _CaseServices.GetAllDiscriminationStatus(casediscriminationentitymodel);

                CasePrimaryStatusEntityModel caseprimaryentitymodel = new CasePrimaryStatusEntityModel();
                caseentitymodel.CasePrimaryStatusList = _CaseServices.GetAllPrimaryStatus(caseprimaryentitymodel);

                CaseTypeEntityModel casetypeentitymodel = new CaseTypeEntityModel();
                caseentitymodel.CaseTypeList = _CaseServices.GetAllCaseType(casetypeentitymodel);

                CourtEntityModel courtentitymodel = new CourtEntityModel();
                caseentitymodel.CourtList = _CaseServices.GetAllCourt(courtentitymodel);

                ViewBag.caseid = 12;
                return View(caseentitymodel);
            }
            return View(caseentitymodel);
        }

        [HttpPost]
        public ActionResult Edit(Int64 id, CaseEntityModel caseentitymodel)
        {

            if (ModelState.IsValid)
            {
                caseentitymodel.CaseId = id;
                Int64 Userid = User.UserId;
                caseentitymodel.CaseUserId = Userid;
                caseentitymodel.CaseNo = "FileNo-" + caseentitymodel.CaseNo;
                caseentitymodel.CaseLastModifiedBy = User.UserId;
                _CaseServices.AddOrUpdateCase(caseentitymodel);
                ModelState.Clear();
                return RedirectToAction("List", "Case");
            }
            return View(caseentitymodel);
        }

        public ActionResult Details(Int64 id, CaseEntityModel caseentitymodel)
        {
            caseentitymodel = _CaseServices.GetCaseById(id);
            caseentitymodel.appointmentEntityModel.AppointmentCategoryList = _CaseServices.GetAllAppointmentCategory();
            if (caseentitymodel != null)
            {
                CaseUpdatesEntityModel caseupdateentitymodel = new CaseUpdatesEntityModel();
                caseupdateentitymodel.CaseUpdateCaseId = id;
                caseentitymodel.CaseUpdateList = _CaseServices.GetAllUpdatesByCaseId(caseupdateentitymodel);

                string NoOnly = caseentitymodel.CaseNo.Replace("FileNo-", "");
                caseentitymodel.CaseNo = NoOnly;
                return View(caseentitymodel);
            }
            return View(caseentitymodel);
        }

        [HttpPost]
        public ActionResult Delete(int id, CaseEntityModel caseentitymodel)
        {
            try
            {
                caseentitymodel = new CaseEntityModel();
                caseentitymodel.CaseId = id;
                caseentitymodel.CaseDelete = true;
                caseentitymodel.CaseStatus = false;
                caseentitymodel.CaseLastModifiedBy = User.UserId;
                _CaseServices.DeleteCase(caseentitymodel);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return View();
            }
        }

        #region Appointment
        [HttpGet]
        public ActionResult AddAppointment(Int64 id)
        {
            AppointmentEntityModel Appointmentmodel = new AppointmentEntityModel();
            Appointmentmodel.AppointmentCategoryList = _CaseServices.GetAllAppointmentCategory();
            return PartialView("_AddAppointment", Appointmentmodel);
        }
        [HttpPost]
        //[ValidateAjax]
        public ActionResult AddAppointment(AppointmentEntityModel appointmententitymodel)
        {
            if (ModelState.IsValid)
            {
                appointmententitymodel.CaseAppointmentCreatedBy = User.UserId;
                if (_CaseServices.AddOrUpdateCaseAppointment(appointmententitymodel))
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Updates
        [HttpGet]
        public ActionResult AddCaseUpdates(Int64 id)
        {
            CaseUpdatesEntityModel caseupdatesentitymodel = new CaseUpdatesEntityModel();

            caseupdatesentitymodel.CaseUpdatesCreatedBy = User.UserId;
            caseupdatesentitymodel.CaseUpdateCaseId = id;

            return PartialView("_AddCaseUpdates", caseupdatesentitymodel);
        }

        [HttpPost]
        [ValidateAjax]
        public ActionResult AddCaseUpdates(CaseUpdatesEntityModel caseupdateentitymodel)
        {
            if (ModelState.IsValid)
            {
                caseupdateentitymodel.CaseUpdatesCreatedBy = User.UserId;
                if (_CaseServices.AddOrUpdateCaseUpdates(caseupdateentitymodel))
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListOfUpdates()
        {
            return View();
        }
        #endregion

        #region Calender Stuff

        public JsonResult GetDiarySummary(double start, double end)
        {
            var ApptListForDate = _CaseServices.LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = _CaseServices.LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                CaseAppointmentTitle=e.CaseAppointmentTitle,
                                CaseAppointmentTime=e.CaseAppointmentTime,
                                AppointmentLength=e.AppointmentLength,
                                CaseAppointmentCateTitle = e.CaseAppointmentCateTitle,
                                allDay = false

                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public void UpdateEvent(int id, string NewEventStart, string NewEventEnd)
        {
            _CaseServices.UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
        }

        public bool SaveEvent(string CaseAppointmentTitle, string CaseAppointmentDate, string CaseAppointmentTime, string AppointmentLength, string CaseAppointmentProgress, string CaseAppointmentCategory, int CaseId)
        {
            return _CaseServices.CreateNewEvent(CaseAppointmentTitle, CaseAppointmentDate, CaseAppointmentTime, AppointmentLength, CaseAppointmentProgress, CaseAppointmentCategory,CaseId);
        }


        #endregion
    }
}

