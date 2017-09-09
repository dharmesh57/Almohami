namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Cases = new HashSet<Case>();
            CasePaymentDetails = new HashSet<CasePaymentDetail>();
        }

        public long ClientID { get; set; }

        [StringLength(100)]
        public string ClientName { get; set; }

        [StringLength(100)]
        public string ClientName2 { get; set; }

        [StringLength(50)]
        public string ClientCivilId { get; set; }

        [StringLength(100)]
        public string ClientCompany { get; set; }

        public string ClientNotes { get; set; }

        [StringLength(200)]
        public string ClientAvtar { get; set; }

        [StringLength(100)]
        public string ClientEmailId { get; set; }

        [StringLength(50)]
        public string ClientMobileNo { get; set; }

        [StringLength(50)]
        public string ClientOfficeNo { get; set; }

        [StringLength(50)]
        public string ClientFaxNo { get; set; }

        [StringLength(500)]
        public string ClientAddress { get; set; }

        [StringLength(100)]
        public string ClientWebsite { get; set; }

        public bool? ClientStatus { get; set; }

        public bool? ClientDelete { get; set; }

        public DateTime? ClientCreatedDate { get; set; }

        public long? ClientCreatedBy { get; set; }

        public DateTime? ClientLastModifiedDate { get; set; }

        public long? ClientLastModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CasePaymentDetail> CasePaymentDetails { get; set; }
    }
}
