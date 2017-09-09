using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
   public class ForgotPasswordEntityModel
    {
      
           [Required]
           [EmailAddress]
           [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter Valid Email Id")]
           [Display(Name = "Email")]
           public string Email { get; set; }

           public string PasswordVerificationToken { get; set; }

           public DateTime? PasswordVerificationTokenExpirationDate { get; set; }
    }
}
