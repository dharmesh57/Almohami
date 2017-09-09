namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Case")]
    public partial class Case
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Case()
        {
            CasePaymentDetails = new HashSet<CasePaymentDetail>();
        }

        public long CaseId { get; set; }

        [Required]
        public string CaseNo { get; set; }

        [Required]
        public string CaseGlobalNo { get; set; }

        public long? CaseUserID { get; set; }

        public long CaseAssignedTo { get; set; }

        public long? CaseClient { get; set; }

        [StringLength(500)]
        public string CaseName { get; set; }

        public string CaseNotes { get; set; }

        public string CaseSubjectOfLawsuit { get; set; }

        public long? CaseTypeID { get; set; }

        public long? CasePrimaryStatusID { get; set; }

        public long? CaseAppealStatusID { get; set; }

        public long? CaseDiscriminationStatusID { get; set; }

        public long? CaseCourtID { get; set; }

        [StringLength(50)]
        public string CaseGovNo { get; set; }

        [StringLength(50)]
        public string CaseOneNo { get; set; }

        [StringLength(50)]
        public string CaseAppealNo { get; set; }

        [StringLength(50)]
        public string CaseLastNo { get; set; }

        [StringLength(50)]
        public string CaseFloorNo { get; set; }

        [StringLength(50)]
        public string CaseHallNo { get; set; }

        public DateTime? CaseCreatedDate { get; set; }

        public long? CaseCreatedBy { get; set; }

        public DateTime? CaseLastModifiedDate { get; set; }

        public long? CaseLastModifiedBy { get; set; }

        public bool? CaseStatus { get; set; }

        public bool? CaseDelete { get; set; }

        [StringLength(50)]
        public string CaseActiveStatus { get; set; }

        public virtual CaseAppealStatusMst CaseAppealStatusMst { get; set; }

        public virtual CaseDiscriminationStatusMst CaseDiscriminationStatusMst { get; set; }

        public virtual CasePrimaryStatusMst CasePrimaryStatusMst { get; set; }

        public virtual CaseType CaseType { get; set; }

        public virtual Client Client { get; set; }

        public virtual Court Court { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CasePaymentDetail> CasePaymentDetails { get; set; }
    }
}
