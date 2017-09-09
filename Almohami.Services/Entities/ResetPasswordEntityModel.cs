using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
   public class ResetPasswordEntityModel
    {
       public long Id { get; set; }

        [EmailAddress]
        [Display(Name = "Your Email Address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string UserVCode { get; set; }

        public string Name { get; set; }
        public string PasswordVerificationToken { get; set; }
       
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public int? PasswordFailure { get; set; }

        public bool IsResetTokenInvalid { get; set; }

        public DateTime? LastPasswordModified { get; set; }
    }
}
