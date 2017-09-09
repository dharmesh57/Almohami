using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class CaseTypeEntityModel : BaseViewModel
    {
       [Key]
        public long CaseTypeId { get; set; }

        [StringLength(100)]
        public string CaseTypeDescription { get; set; }
    }
}
