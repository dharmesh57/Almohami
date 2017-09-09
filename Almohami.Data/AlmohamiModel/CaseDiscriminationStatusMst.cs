namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaseDiscriminationStatusMst")]
    public partial class CaseDiscriminationStatusMst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaseDiscriminationStatusMst()
        {
            Cases = new HashSet<Case>();
        }

        [Key]
        public long CaseDiscriminationStatusId { get; set; }

        [StringLength(100)]
        public string CaseDiscriminationStatusDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
