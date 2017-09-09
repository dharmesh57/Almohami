using Almohami.Core.Enums;
using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Almohami.Services.Entities
{
    public class AppointmentEntityModel : BaseViewModel
    {
        public AppointmentEntityModel()
        {
            AppointmentCategoryList = new List<AppointmentCategory>();
        }
        public long CaseAppointmentId { get; set; }

        public long? CaseId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime CaseAppointmentDate { get; set; }

        [Required]
        [Display(Name = "Time")]
        public string CaseAppointmentTime { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Subject")]
        public string CaseAppointmentTitle { get; set; }

        [Required]
        public string CaseAppointmentDescription { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Status")]
        public WorkStatus CaseAppointmentProgress { get; set; }

        [Required]
        [Display(Name="Appointment Type")]
        public int? CaseAppointmentCategory { get; set; }
        public string CaseAppointmentCateTitle { get; set; }
        public DateTime? CaseAppointmentCreatedDate { get; set; }

        public long? CaseAppointmentCreatedBy { get; set; }

        public DateTime? CaseAppointmentLastModifiedDate { get; set; }

        public long? CaseAppointmentLastModifiedBy { get; set; }

        public bool? CaseAppointmentStatus { get; set; }

        public List<AppointmentCategory> AppointmentCategoryList { get; set; }

        public bool? CaseAppointmentDelete { get; set; }


        public int ID { get; set; }
        public string Title { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string StatusString { get; set; }
        public string StatusColor { get; set; }
        public string ClassName { get; set; }
        public int AppointmentLength { get; set; }
    }
}
