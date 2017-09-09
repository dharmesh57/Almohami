using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
   public class AppointmentCategoryEntityModel
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
