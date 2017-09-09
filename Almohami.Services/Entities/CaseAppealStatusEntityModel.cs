using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class CaseAppealStatusEntityModel : BaseViewModel
    {
        [Key]
        public long CaseAppealStatusId { get; set; }

        [StringLength(100)]
        public string CaseAppealDescription { get; set; }
    }
}
