using Almohami.Data.AlmohamiModel;
using Almohami.Services.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class RegisterModel
    {
       public long UserID { get; set; }

      // public user_profile_mst a { get; set; }
        [Required (ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationRequired")]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter Valid Email Id")]
        public string Email { get; set; }

        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Mobile No")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Not a valid Phone number Format(xxxxxxxx)")]
        public string MobileNo { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }

        public string OTP { get; set; }
        public string HiddenOTP { get; set; }

        public string UserVCode { get; set; }

        public bool? UserEmailConfirmed { get; set; }

        public bool? UserPhoneConfirmed { get; set; }

        public bool? UserDelete { get; set; }

        public bool? UserStatus { get; set; }
    }
}
