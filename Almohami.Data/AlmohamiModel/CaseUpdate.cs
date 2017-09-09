namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CaseUpdate
    {
        public long CaseUpdateId { get; set; }

        public long? CaseUpdateCaseId { get; set; }

        [StringLength(500)]
        public string CaseUpdateTitle { get; set; }

        public string CaseUpdateDescription { get; set; }

        public DateTime? CaseUpdateCreatedDate { get; set; }

        public long? CaseUpdatesCreatedBy { get; set; }

        public DateTime? CasesUpdatesLastModifiedDate { get; set; }

        public long? CaseUpdatesLastModifiedBy { get; set; }

        public bool? CaseUpdatesDelete { get; set; }

        public bool? CaseUpdatesStatus { get; set; }
    }
}
