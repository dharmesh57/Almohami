using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
   public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public Int64 UserID { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
    }
   public class MyPrincipal : IPrincipal
   {
       public MyPrincipal(IIdentity identity)
       {
           Identity = identity;
       }
       public IIdentity Identity
       {
           get;
           private set;
       }
       public User User { get; set; }
       public bool IsInRole(string role)
       {
           return true;
       }
   }
}
