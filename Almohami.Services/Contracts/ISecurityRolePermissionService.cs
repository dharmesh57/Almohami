using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Contracts
{
   public interface ISecurityRolePermissionService
    {
       List<SecurityRole> GetAllRoleData(int LanguageId);
        void AddOrUpdateRole(SecurityRolePermissionEntityModel securityRolePermissionEntityModel);

        SecurityRolePermissionEntityModel GetRoleById(int roleId);

        void DeleteRole(int roleId);

        List<vRolePermission> GetAllRoleWithPermissionData();

        void AddOrUpdatePermission(SecurityRolePermissionEntityModel securityRolePermissionEntityModel);
    }
}
