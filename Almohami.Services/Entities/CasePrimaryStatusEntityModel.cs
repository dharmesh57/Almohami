using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class CasePrimaryStatusEntityModel : BaseViewModel
    {
        [Key]
        public long CasePrimaryStatusId { get; set; }

        [StringLength(500)]
        public string CasePrimaryStausTitle { get; set; }
    }
}
