namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class menu_mst
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public menu_mst()
        {
            submenu_mst = new HashSet<submenu_mst>();
        }

        [Key]
        public long menu_id { get; set; }

        [StringLength(200)]
        public string menu_name { get; set; }

        [StringLength(200)]
        public string menu_url { get; set; }

        [StringLength(100)]
        public string menu_icon { get; set; }

        public int? menu_priority { get; set; }

        public bool? menu_active { get; set; }

        [StringLength(50)]
        public string menu_createddate { get; set; }

        [StringLength(50)]
        public string menu_createdtime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<submenu_mst> submenu_mst { get; set; }
    }
}
