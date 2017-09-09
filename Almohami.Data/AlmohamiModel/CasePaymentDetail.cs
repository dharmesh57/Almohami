namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CasePaymentDetail
    {
        [Key]
        public long CasePaymentId { get; set; }

        public long? CaseId { get; set; }

        public long? CaseClientId { get; set; }

        [Column(TypeName = "money")]
        public decimal? CaseContractAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? CasePaidAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? CaseCreditAmount { get; set; }

        public DateTime? CasePaymentCreatedDate { get; set; }

        public long? CasePaymentCreateBy { get; set; }

        public DateTime? CasePaymentLastModifiedDate { get; set; }

        public long? CasePaymentLastModifiedBy { get; set; }

        public virtual Case Case { get; set; }

        public virtual Client Client { get; set; }
    }
}
