using Almohami.Core.Enums;
using Almohami.Core.Helper;
using Almohami.Data.AlmohamiModel;
using Almohami.Data.SpModel;
using Almohami.Data.UnitOfWork;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Almohami.Services.Services
{
    public class CaseServices : ICaseServices
    {
        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CaseServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion


        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>

        public List<Case> GetAllCasaes(CaseEntityModel model)
        {
            return _unitOfWork.Repository<Case>().Table().Where(c => c.CaseStatus == model.CaseStatus).ToList();
        }
        public List<Case> GetCasesByUserIdandStatus(CaseEntityModel model)
        {
            return _unitOfWork.Repository<Case>().Table().Where(row => row.CaseUserID == model.CaseUserId && row.CaseStatus == model.CaseStatus).ToList();
        }
        public List<CaseAppealStatusMst> GetAllAppealStatus(CaseAppealStatusEntityModel model)
        {
            return _unitOfWork.Repository<CaseAppealStatusMst>().Table().ToList();
        }

        public List<CaseDiscriminationStatusMst> GetAllDiscriminationStatus(CaseDiscriminationStatusEntityModel model)
        {
            return _unitOfWork.Repository<CaseDiscriminationStatusMst>().Table().ToList();
        }

        public List<CasePrimaryStatusMst> GetAllPrimaryStatus(CasePrimaryStatusEntityModel model)
        {
            return _unitOfWork.Repository<CasePrimaryStatusMst>().Table().ToList();
        }

        public List<CaseType> GetAllCaseType(CaseTypeEntityModel model)
        {
            return _unitOfWork.Repository<CaseType>().Table().ToList();
        }

        public List<Court> GetAllCourt(CourtEntityModel model)
        {
            return _unitOfWork.Repository<Court>().Table().ToList();
        }

        public bool CheckUserHavingCase(Int64 userid)
        {
            return _unitOfWork.Repository<Case>().Table().Where(row => row.CaseDelete == false && row.CaseStatus == true && row.CaseUserID == userid).Any();
        }

        public string GetIncrementedCaseNo(CaseEntityModel model)
        {
            var lastno = _unitOfWork.Repository<Case>().Table().Where(p => p.CaseUserID == model.CaseUserId).OrderByDescending(p => p.CaseId).Select(p => p.CaseNo).First();
            return lastno;
        }
        public string GetIncrementedGlobalNo(CaseEntityModel model)
        {
            var globalno = _unitOfWork.Repository<Case>().Table().OrderByDescending(p => p.CaseId).Select(p => p.CaseGlobalNo).First();
            return globalno;
        }

        public CaseEntityModel GetCaseById(Int64 caseid)
        {

            return (from a in _unitOfWork.Repository<Case>().Table()

                    where a.CaseId == caseid && a.CaseDelete == false
                    select new CaseEntityModel
                    {
                        CaseId = a.CaseId,
                        CaseUserId = a.CaseUserID,
                        CaseNo = a.CaseNo,
                        CaseAssignedTo = a.CaseAssignedTo,
                        CaseClient = a.CaseClient,
                        CaseName = a.CaseName,
                        CaseNotes = a.CaseNotes,
                        CaseSubjectOfLawsuit = a.CaseSubjectOfLawsuit,
                        CaseTypeID = (long)a.CaseTypeID,
                        CasePrimaryStatusID = (long)a.CasePrimaryStatusID,
                        CaseAppealStatusID = (long)a.CaseAppealStatusID,
                        CaseDiscriminationStatusID = (long)a.CaseDiscriminationStatusID,
                        CaseCourtID = (long)a.CaseCourtID,
                        CaseGovNo = a.CaseGovNo,
                        CaseOneNo = a.CaseOneNo,
                        CaseAppealNo = a.CaseAppealNo,
                        CaseLastNo = a.CaseLastNo,
                        CaseFloorNo = a.CaseFloorNo,
                        CaseHallNo = a.CaseHallNo,
                        CaseStatus = a.CaseStatus,
                        Username = a.User.UserName,
                        CaseTypeName = a.CaseType.CaseTypeDescription,
                        PrimaryStatus = a.CasePrimaryStatusMst.CasePrimaryStausTitle,
                        AppealStatus = a.CaseAppealStatusMst.CaseAppealDescription,
                        DiscriminationStatus = a.CaseDiscriminationStatusMst.CaseDiscriminationStatusDescription,
                        CourtName = a.Court.CourtName,
                        ClientName = a.Client.ClientName,
                        //ClientName2=a.Client.ClientName2,
                        //ClientcivilId=a.Client.ClientCivilId,
                        //ClientCompanyName=a.Client.ClientCompany,
                        //ClientEmailId=a.Client.ClientEmailId,
                        //ClientMobileNo=a.Client.ClientMobileNo,
                        //ClientPhoneno=a.Client.ClientOfficeNo,                      
                        //UserMobileNo=a.User.UserMobileNo,
                        //UserEmail=a.User.UserEmailId,
                        //UserRole=a.User.SecurityRole.RoleName,
                    }).FirstOrDefault();
        }
        public void AddOrUpdateCase(CaseEntityModel caseentitymodel)
        {

            if (caseentitymodel.CaseId > 0)
            {
                // update
                var casedata = _unitOfWork.Repository<Case>().Table().FirstOrDefault(c => c.CaseId == caseentitymodel.CaseId);

                if (casedata == null)
                {
                    throw new Exception("Case not found");
                }
                casedata.CaseNo = caseentitymodel.CaseNo ?? casedata.CaseNo;
                casedata.CaseGlobalNo = caseentitymodel.CaseGlobalNo ?? casedata.CaseGlobalNo;
                casedata.CaseAssignedTo = caseentitymodel.CaseAssignedTo ?? casedata.CaseAssignedTo;
                casedata.CaseClient = caseentitymodel.CaseClient ?? casedata.CaseClient;
                casedata.CaseName = caseentitymodel.CaseName ?? casedata.CaseName;
                casedata.CaseNotes = caseentitymodel.CaseNotes ?? casedata.CaseNotes;
                casedata.CaseSubjectOfLawsuit = caseentitymodel.CaseSubjectOfLawsuit ?? casedata.CaseSubjectOfLawsuit;
                casedata.CaseTypeID = caseentitymodel.CaseTypeID ?? casedata.CaseTypeID;
                casedata.CasePrimaryStatusID = caseentitymodel.CasePrimaryStatusID ?? casedata.CasePrimaryStatusID;
                casedata.CaseAppealStatusID = caseentitymodel.CaseAppealStatusID ?? casedata.CaseAppealStatusID;
                casedata.CaseDiscriminationStatusID = caseentitymodel.CaseDiscriminationStatusID ?? casedata.CaseDiscriminationStatusID;
                casedata.CaseCourtID = caseentitymodel.CaseCourtID ?? casedata.CaseCourtID;
                casedata.CaseGovNo = caseentitymodel.CaseGovNo ?? casedata.CaseGovNo;
                casedata.CaseOneNo = caseentitymodel.CaseOneNo ?? casedata.CaseOneNo;
                casedata.CaseAppealNo = caseentitymodel.CaseAppealNo ?? casedata.CaseAppealNo;
                casedata.CaseLastNo = caseentitymodel.CaseLastNo ?? casedata.CaseLastNo;
                casedata.CaseFloorNo = caseentitymodel.CaseFloorNo ?? casedata.CaseFloorNo;
                casedata.CaseHallNo = caseentitymodel.CaseHallNo ?? casedata.CaseHallNo;
                casedata.CaseStatus = caseentitymodel.CaseStatus ?? casedata.CaseStatus;
                casedata.CaseActiveStatus = caseentitymodel.CaseActiveStatus.ToString() ?? casedata.CaseActiveStatus;
                casedata.CaseDelete = caseentitymodel.CaseDelete ?? casedata.CaseDelete;
                casedata.CaseLastModifiedDate = DateTime.Now;
                casedata.CaseLastModifiedBy = 1;

                _unitOfWork.Repository<Case>().Update(casedata);
                _unitOfWork.Save();
            }
            else
            {
                //insert
                Mapper.CreateMap<CaseEntityModel, Case>();
                var CaseDetails = Mapper.Map<CaseEntityModel, Case>(caseentitymodel);

                CaseDetails.CaseNo = caseentitymodel.CaseNo;
                CaseDetails.CaseGlobalNo = caseentitymodel.CaseGlobalNo;
                CaseDetails.CaseAssignedTo = (long)caseentitymodel.CaseAssignedTo;
                CaseDetails.CaseClient = caseentitymodel.CaseClient;
                CaseDetails.CaseName = caseentitymodel.CaseName;
                CaseDetails.CaseNotes = caseentitymodel.CaseNotes;
                CaseDetails.CaseSubjectOfLawsuit = caseentitymodel.CaseSubjectOfLawsuit;
                CaseDetails.CaseTypeID = caseentitymodel.CaseTypeID;
                CaseDetails.CasePrimaryStatusID = caseentitymodel.CasePrimaryStatusID;
                CaseDetails.CaseAppealStatusID = caseentitymodel.CaseAppealStatusID;
                CaseDetails.CaseDiscriminationStatusID = caseentitymodel.CaseDiscriminationStatusID;
                CaseDetails.CaseCourtID = caseentitymodel.CaseCourtID;
                CaseDetails.CaseGovNo = caseentitymodel.CaseGovNo;
                CaseDetails.CaseOneNo = caseentitymodel.CaseOneNo;
                CaseDetails.CaseAppealNo = caseentitymodel.CaseAppealNo;
                CaseDetails.CaseLastNo = caseentitymodel.CaseLastNo;
                CaseDetails.CaseFloorNo = caseentitymodel.CaseFloorNo;
                CaseDetails.CaseHallNo = caseentitymodel.CaseHallNo;
                CaseDetails.CaseStatus = caseentitymodel.CaseStatus;
                CaseDetails.CaseDelete = caseentitymodel.CaseDelete;
                CaseDetails.CaseActiveStatus = caseentitymodel.CaseActiveStatus.ToString();
                CaseDetails.CaseCreatedBy = caseentitymodel.CaseCreatedBy;
                CaseDetails.CaseCreatedDate = DateTime.Now;
                _unitOfWork.Repository<Case>().Add(CaseDetails);
                _unitOfWork.Save();

            }

        }

        public void DeleteCase(CaseEntityModel caseentitymodel)
        {
            var casedata = _unitOfWork.Repository<Case>().Table().FirstOrDefault(c => c.CaseId == caseentitymodel.CaseId);


            if (casedata == null)
            {
                throw new Exception("Case not found");
            }

            casedata.CaseStatus = caseentitymodel.CaseStatus;
            casedata.CaseDelete = caseentitymodel.CaseDelete;
            casedata.CaseLastModifiedBy = caseentitymodel.CaseLastModifiedBy;
            casedata.CaseLastModifiedDate = DateTime.Now;
            _unitOfWork.Repository<Case>().Update(casedata);
            _unitOfWork.Save();
        }


        public bool AddOrUpdateCaseUpdates(CaseUpdatesEntityModel caseupdateentitymodel)
        {
            bool flag = false;

            if (caseupdateentitymodel.CaseUpdateId > 0)
            {
                //Update
            }
            else
            {
                //Insert
                Mapper.CreateMap<CaseUpdatesEntityModel, CaseUpdate>();
                var caseupdatedetails = Mapper.Map<CaseUpdatesEntityModel, CaseUpdate>(caseupdateentitymodel);
                caseupdatedetails.CaseUpdateCaseId = caseupdateentitymodel.CaseUpdateCaseId;
                caseupdatedetails.CaseUpdateTitle = caseupdateentitymodel.CaseUpdateTitle;
                caseupdatedetails.CaseUpdateDescription = caseupdateentitymodel.CaseUpdateDescription;
                caseupdatedetails.CaseUpdateCreatedDate = DateTime.Now;
                caseupdatedetails.CaseUpdatesCreatedBy = caseupdateentitymodel.CaseUpdatesCreatedBy;
                caseupdatedetails.CaseUpdatesDelete = false;
                caseupdatedetails.CaseUpdatesStatus = true;
                _unitOfWork.Repository<CaseUpdate>().Add(caseupdatedetails);
                _unitOfWork.Save();
                flag = true;
            }
            return flag;
        }

        public List<CaseUpdate> GetAllUpdatesByCaseId(CaseUpdatesEntityModel model)
        {
            return _unitOfWork.Repository<CaseUpdate>().Table().Where(m => m.CaseUpdateCaseId == model.CaseUpdateCaseId).ToList();

        }

        public List<AppointmentCategory> GetAllAppointmentCategory()
        {
            return _unitOfWork.Repository<AppointmentCategory>().Table().ToList();
        }

        public bool AddOrUpdateCaseAppointment(AppointmentEntityModel appointmententitymodel)
        {
            bool flag = false;

            if (appointmententitymodel.CaseAppointmentId > 0)
            {
                //Update
            }
            else
            {
                //Insert
                Mapper.CreateMap<AppointmentEntityModel, CaseAppointment>();
                var caseappointmentdetails = Mapper.Map<AppointmentEntityModel, CaseAppointment>(appointmententitymodel);
                caseappointmentdetails.CaseId = appointmententitymodel.CaseId;
                caseappointmentdetails.CaseAppointmentDate = appointmententitymodel.CaseAppointmentDate;
                caseappointmentdetails.CaseAppointmentTime = appointmententitymodel.CaseAppointmentTime.ToString();
                caseappointmentdetails.CaseAppointmentTitle = appointmententitymodel.CaseAppointmentTitle;
                caseappointmentdetails.CaseAppointmentDescription = appointmententitymodel.CaseAppointmentDescription;
                caseappointmentdetails.CaseAppointmentCategory = appointmententitymodel.CaseAppointmentCategory;
                caseappointmentdetails.CaseAppointmentProgress = appointmententitymodel.CaseAppointmentProgress.ToString();
                caseappointmentdetails.CaseAppointmentCreatedDate = DateTime.Now;
                caseappointmentdetails.CaseAppointmentStatus = true;
                caseappointmentdetails.CaseAppointmentCreatedBy = appointmententitymodel.CaseAppointmentCreatedBy;
                caseappointmentdetails.CaseAppointmentDelete = false;
                _unitOfWork.Repository<CaseAppointment>().Add(caseappointmentdetails);
                _unitOfWork.Save();
                flag = true;
            }
            return flag;
        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        public List<AppointmentEntityModel> LoadAppointmentSummaryInDateRange(double start, double end)
        {

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);

            var rslt = _unitOfWork.Repository<CaseAppointment>().Table().Where(s => s.CaseAppointmentDate >= fromDate && EntityFunctions.AddMinutes(s.CaseAppointmentDate, s.AppointmentLength) <= toDate)
                                                        .GroupBy(s => EntityFunctions.TruncateTime(s.CaseAppointmentDate))
                                                        .Select(x => new { DateTimeScheduled = x.Key, Count = x.Count() });

            List<AppointmentEntityModel> result = new List<AppointmentEntityModel>();
            int i = 0;
            foreach (var item in rslt)
            {
                AppointmentEntityModel rec = new AppointmentEntityModel();
                rec.ID = i; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                rec.EndDateString = StringDate + "T23:59:59";
                rec.Title = "Booked: " + item.Count.ToString();

                result.Add(rec);
                i++;
            }

            return result;
        }

        public List<AppointmentEntityModel> LoadAllAppointmentsInDateRange(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            List<AppointmentEntityModel> result = new List<AppointmentEntityModel>();
            if (_unitOfWork.Repository<CaseAppointment>().Table().Count() > 0)
            {

                //var rslt = _unitOfWork.Repository<CaseAppointment>().Table().Where(s => s.CaseAppointmentDate >= fromDate && EntityFunctions.AddMinutes(s.CaseAppointmentDate, s.AppointmentLength) <= toDate);
                var rslt = _unitOfWork.SQLQuery<sp_getCaseAppointment>("sp_getCaseAppointment").Where(s => s.CaseAppointmentDate >= fromDate && s.AppointmentDate <= toDate); 

                foreach (var item in rslt)
                {
                    AppointmentEntityModel rec = new AppointmentEntityModel();
                    rec.ID = Convert.ToInt32(item.CaseAppointmentId);
                    rec.StartDateString = item.CaseAppointmentDate.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    rec.EndDateString = item.CaseAppointmentDate.AddMinutes(item.AppointmentLength).ToString("s"); // field AppointmentLength is in minutes
                    rec.Title = item.CaseAppointmentTitle + " - " + item.AppointmentLength.ToString() + " mins";
                    rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.StatusENUM);
                    rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                    string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                    rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                    rec.StatusColor = ColorCode;
                    rec.CaseAppointmentTitle = item.CaseAppointmentTitle;
                    rec.CaseAppointmentTime = item.CaseAppointmentTime;
                    rec.AppointmentLength = item.AppointmentLength;
                    rec.CaseAppointmentCateTitle = item.AppointmentCategoryTitle;
                    result.Add(rec);
                }

            }

            return result;
        }

        public void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        {
            // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime

            //var rec = ent.AppointmentDiaries.FirstOrDefault(s => s.ID == id);
            // update
            var rec = _unitOfWork.Repository<CaseAppointment>().Table().FirstOrDefault(c => c.CaseAppointmentId == id);

            if (rec == null)
            {
                throw new Exception("Case not found");
            }
            DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
            rec.CaseAppointmentDate = DateTimeStart;
            if (!String.IsNullOrEmpty(NewEventEnd))
            {
                TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
                rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);
            }
            rec.CaseAppointmentLastModifiedDate = DateTime.Now;
            rec.CaseAppointmentLastModifiedBy = 1;

            _unitOfWork.Repository<CaseAppointment>().Update(rec);
            _unitOfWork.Save();

        }

        public bool CreateNewEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration, string CaseAppointmentProgress, string CaseAppointmentCategory, int CaseId)
        {
            try
            {

                CaseAppointment rec = new CaseAppointment();
                rec.CaseAppointmentTitle = Title;
                rec.CaseAppointmentDate = DateTime.ParseExact(NewEventDate + " " + NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                rec.AppointmentLength = Int32.Parse(NewEventDuration);
                rec.CaseId = CaseId;

                rec.CaseAppointmentTime = NewEventTime;
                rec.CaseAppointmentTitle = Title;
                rec.CaseAppointmentDescription = "test desc";
                rec.CaseAppointmentCategory = Convert.ToInt32(CaseAppointmentCategory);
                rec.CaseAppointmentProgress = "Complete";
                rec.CaseAppointmentCreatedDate = DateTime.Now;
                rec.CaseAppointmentStatus = true;
                rec.CaseAppointmentCreatedBy = 1;
                rec.CaseAppointmentDelete = false;
                rec.StatusENUM = Convert.ToInt32(CaseAppointmentProgress);
                _unitOfWork.Repository<CaseAppointment>().Add(rec);
                _unitOfWork.Save();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
