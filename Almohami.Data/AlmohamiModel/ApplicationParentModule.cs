namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApplicationParentModule")]
    public partial class ApplicationParentModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationParentModule()
        {
            ApplicationModules = new HashSet<ApplicationModule>();
        }

        [Key]
        public int ParentModuleId { get; set; }

        [StringLength(50)]
        public string ParentModuleName { get; set; }

        [StringLength(50)]
        public string CssClass { get; set; }

        public bool? Status { get; set; }

        [StringLength(100)]
        public string ParentModuleURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationModule> ApplicationModules { get; set; }
    }
}
