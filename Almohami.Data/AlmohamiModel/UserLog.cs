namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLog")]
    public partial class UserLog
    {
        public long UserLogID { get; set; }

        public long? UserLogUserID { get; set; }

        public long? UserLogRole { get; set; }

        public bool? UserStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastModifiedDate { get; set; }

        public long? LastModifiedBy { get; set; }
    }
}
