namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class submenu_mst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public submenu_mst()
        {
            submenu_mst1 = new HashSet<submenu_mst>();
        }

        [Key]
        public long submenu_id { get; set; }

        [StringLength(200)]
        public string submenu_name { get; set; }

        [StringLength(500)]
        public string submenu_url { get; set; }

        public long? submenu_menu_id { get; set; }

        public long? submenu_parental_id { get; set; }

        [StringLength(200)]
        public string submenu_icon { get; set; }

        public bool? submenu_active { get; set; }

        public bool? submenu_isdisp { get; set; }

        [StringLength(50)]
        public string submenu_createddate { get; set; }

        [StringLength(50)]
        public string submenu_createdtime { get; set; }

        public virtual menu_mst menu_mst { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<submenu_mst> submenu_mst1 { get; set; }

        public virtual submenu_mst submenu_mst2 { get; set; }
    }
}
