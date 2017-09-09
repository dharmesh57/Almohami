namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Cases = new HashSet<Case>();
        }

        public long UserId { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string UserCompany { get; set; }

        [StringLength(200)]
        public string UserAvtar { get; set; }

        [StringLength(100)]
        public string UserEmailId { get; set; }

        [StringLength(100)]
        public string UserPassword { get; set; }

        public long? UserRoleId { get; set; }

        [StringLength(50)]
        public string UserMobileNo { get; set; }

        [StringLength(50)]
        public string UserOTP { get; set; }

        public bool? UserStatus { get; set; }

        public bool UserEmailConfirmed { get; set; }

        public bool UserPhoneConfirmed { get; set; }

        public long? UserApprovedBy { get; set; }

        public long? UserRejectedBy { get; set; }

        [StringLength(500)]
        public string UserRejectionReason { get; set; }

        public bool? UserDelete { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [StringLength(500)]
        public string UserVCode { get; set; }

        [StringLength(500)]
        public string PasswordVerificationToken { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public int? PasswordFailure { get; set; }

        public DateTime? LastPasswordModified { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
