namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApplicationModule")]
    public partial class ApplicationModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationModule()
        {
            SecurityRolePermissions = new HashSet<SecurityRolePermission>();
        }

        [Key]
        public int ModuleId { get; set; }

        public int? ParentModuleId { get; set; }

        [StringLength(50)]
        public string Permissionname { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string CssClass { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsSubMoudle { get; set; }

        public bool? IsVisibleMenu { get; set; }

        public virtual ApplicationParentModule ApplicationParentModule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityRolePermission> SecurityRolePermissions { get; set; }
    }
}
