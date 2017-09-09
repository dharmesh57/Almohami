using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class CaseUpdatesEntityModel : BaseViewModel
    {
        public CaseUpdatesEntityModel()
        {
            CaseList = new List<Case>();
            ClientList = new List<Client>();
        }
        public long CaseUpdateId { get; set; }

        [Display(Name = "Case")]
        public long? CaseUpdateCaseId { get; set; }

        [StringLength(500)]
        [Required]
        [Display(Name = "Subject")]
        public string CaseUpdateTitle { get; set; }

        [Display(Name = "Update")]
        [Required]
        public string CaseUpdateDescription { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CaseUpdateCreatedDate { get; set; }

        public long? CaseUpdatesCreatedBy { get; set; }

        public DateTime? CasesUpdatesLastModifiedDate { get; set; }

        public List<Case> CaseList { get; set; }

        public List<Client> ClientList { get; set; }
    }
}
