namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityRole")]
    public partial class SecurityRole
    {
        public long SecurityRoleId { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool Deleted { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
