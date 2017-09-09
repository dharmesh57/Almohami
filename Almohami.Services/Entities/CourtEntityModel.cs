using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class CourtEntityModel : BaseViewModel
    {
        [Key]
        public long CourtId { get; set; }

        [StringLength(500)]
        public string CourtName { get; set; }
    }
}
