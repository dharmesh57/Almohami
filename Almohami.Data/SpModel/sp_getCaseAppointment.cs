using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Data.SpModel
{
    public class sp_getCaseAppointment
    {
        public long CaseAppointmentId { get; set; }

        public long? CaseId { get; set; }

        public DateTime CaseAppointmentDate { get; set; }

        public string CaseAppointmentTime { get; set; }

        public string CaseAppointmentTitle { get; set; }

        public string CaseAppointmentDescription { get; set; }

        public DateTime? CaseAppointmentCreatedDate { get; set; }

        public long? CaseAppointmentCreatedBy { get; set; }

        public DateTime? CaseAppointmentLastModifiedDate { get; set; }

        public long? CaseAppointmentLastModifiedBy { get; set; }

        public bool? CaseAppointmentStatus { get; set; }

        public int? CaseAppointmentCategory { get; set; }

        public string CaseAppointmentProgress { get; set; }

        public bool? CaseAppointmentDelete { get; set; }

        public int AppointmentLength { get; set; }

        public int StatusENUM { get; set; }
        public string AppointmentCategoryTitle { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
