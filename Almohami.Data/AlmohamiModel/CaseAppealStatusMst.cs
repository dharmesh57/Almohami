namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaseAppealStatusMst")]
    public partial class CaseAppealStatusMst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaseAppealStatusMst()
        {
            Cases = new HashSet<Case>();
        }

        [Key]
        public long CaseAppealStatusId { get; set; }

        [StringLength(100)]
        public string CaseAppealDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
