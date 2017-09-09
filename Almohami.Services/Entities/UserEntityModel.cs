using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almohami.Core.Constants;
using Almohami.Core.Enums;
using Almohami.Data.AlmohamiModel;

namespace Almohami.Services.Entities
{
    public class UserEntityModel:BaseViewModel
    {
        public UserEntityModel()
        {
            UsersList = new List<User>();
            RoleList = new List<SecurityRole>();
        }
        public User UserAddModel { get; set; }

        [Key]
        public Int64 UserID { get; set; }



        [Required]
        [MaxLength(250, ErrorMessage = "Only 250 characters allowed!")]
        [Display(Name = "Name")]
        public string Name { get; set; }



        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }



        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Email")]
        [MaxLength(150, ErrorMessage = "Only 150 characters allowed!")]        
        public string Email { get; set; }


        public string Password { get; set; }


        [Display(Name = "Profile Picture")]
        public string UserAvtar { get; set; }

        public bool isAttachmentChanged { get; set; }


        [Display(Name = "Role")]
        public long? RoleId { get; set; }

        public string RoleName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Only 30 characters allowed!")]
        [Display(Name = "MobileNo")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Invalid format")]
        public string MobileNo { get; set; }


        [Required(ErrorMessage = " ")]
        public bool? Status { get; set; }  

        //[Display(Name="Active")]
        //public bool IsActive
        //{
        //    get
        //    {
        //       // return !string.IsNullOrEmpty(Status) ? Status.Equals("1") ? true : false : false;
        //       // return !string.IsNullOrEmpty(Status) ? false : true ;
        //    }
        //}
                 

        public bool? IsDeleted { get; set; }

        public string VerificationToken { get; set; }

        public bool? UserEmailConfirmed { get; set; }

        public bool? UserPhoneConfirmed { get; set; }

        public Int64? UserApprovedBy { get; set; }

        public Int64? UserRejectedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public Int64 LastModifiedBy { get; set; }

        public UserRoleNames UserType { get; set; }
        [Required(ErrorMessage = " ")]
        [MaxLength(50, ErrorMessage = "Only 50 characters allowed!")]
        public string JobTitle { get; set; }

        public List<User> UsersList { get; set; }

        public List<UserEntityModel> UserEntitylist { get; set; }

        public DateTime LastLogin { get; set; }

        public List<SecurityRole> RoleList { get; set; }
        public string getseterror { get; set; }

        public int ar { get; set; }

        public decimal filesize { get; set; }
       
    }

    public class ChangePasswordModel
    {
        public long UserID { get; set; }

        [Display(Name="Old Password")]
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed!")]
        public string OldPassword { get; set; }
        
        [Display(Name="New Password")]
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Display(Name="Confirm New Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and confirm password do not match")]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed!")]
        public string ConfirmPassword { get; set; }
    }

    public class UserLogEntityModel
    {
        [Key]
        public Int64 UserLogID { get; set; }

        public Int64 UserLogUserID { get; set; }

        public Int64 UserLogRole { get; set; }

        public bool? UserStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public Int64 CreatedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public Int64 LastModifiedBy { get; set; }

       
    }
}
