namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaseAppointment")]
    public partial class CaseAppointment
    {
        public long CaseAppointmentId { get; set; }

        public long? CaseId { get; set; }

        public DateTime CaseAppointmentDate { get; set; }

        [StringLength(50)]
        public string CaseAppointmentTime { get; set; }

        [StringLength(100)]
        public string CaseAppointmentTitle { get; set; }

        public string CaseAppointmentDescription { get; set; }

        public DateTime? CaseAppointmentCreatedDate { get; set; }

        public long? CaseAppointmentCreatedBy { get; set; }

        public DateTime? CaseAppointmentLastModifiedDate { get; set; }

        public long? CaseAppointmentLastModifiedBy { get; set; }

        public bool? CaseAppointmentStatus { get; set; }

        public int? CaseAppointmentCategory { get; set; }

        [StringLength(100)]
        public string CaseAppointmentProgress { get; set; }

        public bool? CaseAppointmentDelete { get; set; }

        public int AppointmentLength { get; set; }

        public int StatusENUM { get; set; }
    }
}
