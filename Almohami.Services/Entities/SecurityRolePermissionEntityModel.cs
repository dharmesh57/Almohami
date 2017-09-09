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
    public class SecurityRolePermissionEntityModel : BaseViewModel
    {

        public SecurityRolePermissionEntityModel()
        {

            RolePermissionList = new List<vRolePermission>();
            SecurityRoleList = new List<SecurityRole>();
        }

        public List<vRolePermission> RolePermissionList { get; set; }
        public List<SecurityRole> SecurityRoleList { get; set; }
        public SecurityRole SecurityRoleAddModel { get; set; }

        public int SecurityRoleId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ValidationRequired")]
        [MaxLength(50, ErrorMessage = "Only 50 characters allowed!")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Role Description")]
        public string RoleDescription { get; set; }

        public bool IsActive { get; set; }

        public int LanguageId { get; set; }

    }
}
