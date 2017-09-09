namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityRolePermission")]
    public partial class SecurityRolePermission
    {
        public int SecurityRolePermissionId { get; set; }

        public long? SecurityRoleId { get; set; }

        public int? ModuleId { get; set; }

        public bool? IsFullControl { get; set; }

        public bool? CanAdd { get; set; }

        public bool? CanEdit { get; set; }

        public bool? CanView { get; set; }

        public bool? CanDelete { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastModifiedDate { get; set; }

        public int? LastModifiedBy { get; set; }

        public int LanguageId { get; set; }

        public virtual ApplicationModule ApplicationModule { get; set; }

        public virtual Language Language { get; set; }
    }
}
