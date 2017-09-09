using System;
using Almohami.Data.AlmohamiModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Almohami.Core.Enums;

namespace Almohami.Services.Entities
{
    public class CaseEntityModel : BaseViewModel
    {
        public CaseEntityModel()
        {
            CaseList = new List<Case>();
            UserList = new List<User>();
            ClientList = new List<Client>();
            CaseUpdateList = new List<CaseUpdate>();
            appointmentEntityModel = new AppointmentEntityModel();
            AppointmentCategoryList = new List<AppointmentCategory>();
        }
        [Key]
        public Int64 CaseId { get; set; }
        public AppointmentEntityModel appointmentEntityModel { get; set; }

        public List<AppointmentCategory> AppointmentCategoryList { get; set; }
        public Int64? CaseUserId { get; set; }

        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "CaseNo must be a natural number")]
        public string CaseNo { get; set; }

        public string CaseGlobalNo { get; set; }

        [Required]
        [Display(Name = "Case Assigned To")]
        [ForeignKey("User")]
        public Int64? CaseAssignedTo { get; set; }

        [Required]
        [Display(Name = "Client Name")]
        public Int64? CaseClient { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string CaseName { get; set; }

        [Display(Name = "Notes")]
        [AllowHtml]
        public string CaseNotes { get; set; }

        [Required]
        [Display(Name = "Subject Of the Lawsuit")]
        public string CaseSubjectOfLawsuit { get; set; }

        [Required]
        [Display(Name = "Type")]
        public Int64? CaseTypeID { get; set; }

        [Required]
        [Display(Name = "Primary Status")]
        public Int64? CasePrimaryStatusID { get; set; }

        [Required]
        [Display(Name = "Appeal Status")]
        public Int64? CaseAppealStatusID { get; set; }

        [Required]
        [Display(Name = "Discrimination Status")]
        public Int64? CaseDiscriminationStatusID { get; set; }

        [Required]
        [Display(Name = "Court Name")]
        public Int64? CaseCourtID { get; set; }

        [Required]
        [Display(Name = "GOV No")]
        public string CaseGovNo { get; set; }

        [Required]
        [Display(Name = "Case One No")]
        public string CaseOneNo { get; set; }

        [Required]
        [Display(Name = "AppealNo")]
        public string CaseAppealNo { get; set; }

        [Required]
        [Display(Name = "Last No")]
        public string CaseLastNo { get; set; }

        [Required]
        [Display(Name = "Floor No")]
        public string CaseFloorNo { get; set; }

        [Required]
        [Display(Name = "Hall No")]
        public string CaseHallNo { get; set; }

        public DateTime? CaseCreatedDate { get; set; }

        public long? CaseCreatedBy { get; set; }

        public DateTime? CaseLastModifiedDate { get; set; }

        public long? CaseLastModifiedBy { get; set; }

        [Required]
        [Display(Name = "Case Status")]
        public ActiveStatus CaseActiveStatus { get; set; }

        public bool? CaseStatus { get; set; }

        public bool? CaseDelete { get; set; }

        public List<CaseAppealStatusMst> CaseAppealList { get; set; }

        public List<CaseType> CaseTypeList { get; set; }

        public List<CasePrimaryStatusMst> CasePrimaryStatusList { get; set; }

        public List<CaseAppealStatusMst> CaseAppealStatusList { get; set; }

        public List<CaseDiscriminationStatusMst> CaseDiscriminationList { get; set; }

        public List<Court> CourtList { get; set; }

        public List<Case> CaseList { get; set; }

        public List<User> UserList { get; set; }

        public List<Client> ClientList { get; set; }

        public List<CaseUpdate> CaseUpdateList { get; set; }

        public virtual User User { get; set; }

        public string Username { get; set; }

        public string CaseTypeName { get; set; }

        public string PrimaryStatus { get; set; }

        public string AppealStatus { get; set; }

        public string DiscriminationStatus { get; set; }

        public string CourtName { get; set; }

        public string ClientName { get; set; }


        public string FileNo
        {
            get
            {
                return "FileNo - " + CaseNo;
            }
        }
    }
}
