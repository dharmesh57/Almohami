using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Contracts
{
   public interface ICaseServices
    {
        List<Case> GetAllCasaes(CaseEntityModel model);
        List<Case> GetCasesByUserIdandStatus(CaseEntityModel model);
        List<CaseAppealStatusMst> GetAllAppealStatus(CaseAppealStatusEntityModel model);
        List<CaseDiscriminationStatusMst> GetAllDiscriminationStatus(CaseDiscriminationStatusEntityModel model);

        List<CasePrimaryStatusMst> GetAllPrimaryStatus(CasePrimaryStatusEntityModel model);

        List<CaseType> GetAllCaseType(CaseTypeEntityModel model);

        List<Court> GetAllCourt(CourtEntityModel model);

        void AddOrUpdateCase(CaseEntityModel caseentitymodel);

        string GetIncrementedCaseNo(CaseEntityModel model);

        bool CheckUserHavingCase(Int64 userid);

        CaseEntityModel GetCaseById(Int64 caseid);

        void DeleteCase(CaseEntityModel caseentitymodel);

        bool AddOrUpdateCaseUpdates(CaseUpdatesEntityModel caseupdateentitymodel);

        List<CaseUpdate> GetAllUpdatesByCaseId(CaseUpdatesEntityModel model);

        string GetIncrementedGlobalNo(CaseEntityModel model);

        List<AppointmentCategory> GetAllAppointmentCategory();
        bool AddOrUpdateCaseAppointment(AppointmentEntityModel appointmententitymodel);
        List<AppointmentEntityModel> LoadAppointmentSummaryInDateRange(double start, double end);

        List<AppointmentEntityModel> LoadAllAppointmentsInDateRange(double start, double end);

        void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd);

        bool CreateNewEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration,string CaseAppointmentProgress, string CaseAppointmentCategory,int CaseId);
    }
}
