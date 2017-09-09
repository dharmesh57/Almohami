namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vRolePermission")]
    public partial class vRolePermission
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModuleId { get; set; }

        [StringLength(50)]
        public string Permissionname { get; set; }

        public bool? CanAdd { get; set; }

        public bool? CanEdit { get; set; }

        public bool? CanView { get; set; }

        public bool? CanDelete { get; set; }

        public bool? IsFullControl { get; set; }

        public string URL { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string CssClass { get; set; }

        public bool? IsSubMoudle { get; set; }

        public bool? IsVisibleMenu { get; set; }

        public int? SortOrder { get; set; }

        public int? ParentModuleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SecurityRolePermissionId { get; set; }

        public long? SecurityRoleId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LanguageId { get; set; }
    }
}
