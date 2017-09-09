namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppointmentCategory")]
    public partial class AppointmentCategory
    {
        public int AppointmentCategoryId { get; set; }

        [StringLength(250)]
        public string AppointmentTitle { get; set; }

        [StringLength(500)]
        public string AppointmentDescription { get; set; }

        public DateTime? AppointmentCreatedDate { get; set; }

        public long? AppointmentCreatedBy { get; set; }

        public DateTime? AppointmentLastModifiedDate { get; set; }

        public long? AppointmentLastModifiedBy { get; set; }
    }
}
