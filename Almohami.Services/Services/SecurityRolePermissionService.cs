using Almohami.Data.AlmohamiModel;
using Almohami.Data.UnitOfWork;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Services
{
    public class SecurityRolePermissionService : ISecurityRolePermissionService
    {
        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public SecurityRolePermissionService()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all SecurityRolePermission.
        /// </summary>
        /// <returns></returns>
        public List<SecurityRole> GetAllRoleData(int LanguageId)
        {
            return _unitOfWork.Repository<SecurityRole>().Table().Where(row => row.LanguageId==LanguageId && row.Deleted == false).ToList();

        }

        public void AddOrUpdateRole(SecurityRolePermissionEntityModel securityRolePermissionEntityModel)
        {

            if (securityRolePermissionEntityModel.SecurityRoleId > 0)
            {
                // update
                var role = _unitOfWork.Repository<SecurityRole>().Table().FirstOrDefault(c => c.SecurityRoleId == securityRolePermissionEntityModel.SecurityRoleId);

                if (role == null)
                {
                    throw new Exception("role not found");
                }

                role.IsActive = securityRolePermissionEntityModel.IsActive;
                role.RoleName = securityRolePermissionEntityModel.RoleName;
                role.RoleDescription = securityRolePermissionEntityModel.RoleDescription;
                role.LastModifiedDate = DateTime.Now;
                role.LastModifiedBy = 1;
                role.LanguageId = securityRolePermissionEntityModel.LanguageId;
                _unitOfWork.Repository<SecurityRole>().Update(role);
                _unitOfWork.Save();
            }
            else
            {
                //insert
                Mapper.CreateMap<SecurityRolePermissionEntityModel, SecurityRole>();
                var securityRole = Mapper.Map<SecurityRolePermissionEntityModel, SecurityRole>(securityRolePermissionEntityModel);

                securityRole.Deleted = false;
                securityRole.RoleName = securityRolePermissionEntityModel.RoleName;
                securityRole.RoleDescription = securityRolePermissionEntityModel.RoleDescription;
                securityRole.IsActive = securityRolePermissionEntityModel.IsActive;
                securityRole.LanguageId = securityRolePermissionEntityModel.LanguageId;
                securityRole.CreatedBy = 1;
                securityRole.CreatedDate = DateTime.Now;
                _unitOfWork.Repository<SecurityRole>().Add(securityRole);
                _unitOfWork.Save();

            }

        }
        #endregion

        public SecurityRolePermissionEntityModel GetRoleById(int roleId)
        {
            //return db.mrobProducts.Where(x => x.Status == 1).Select(x => new NamePriceModel { Name = x.Name, Id = x.Id, Price = x.Price }).OrderBy(x => x.Id).ToArray();

            //var list = _unitOfWork.Repository<SecurityRole>().Table().Where(row => row.Deleted == false)
            //     .Select(row => new SecurityRolePermissionEntityModel{ 
            //     aff= row.RoleName, 
            //      row.RoleDescription,
            //      row.IsActive
            //         });
            //return list;

            return (from role in _unitOfWork.Repository<SecurityRole>().Table()

                    where role.SecurityRoleId == roleId && role.Deleted == false
                    select new SecurityRolePermissionEntityModel
                    {

                        SecurityRoleId = (int)role.SecurityRoleId,
                        RoleName = role.RoleName,
                        RoleDescription = role.RoleDescription,
                        IsActive = (bool)role.IsActive

                    }).FirstOrDefault();
        }

        public void DeleteRole(int roleId)
        {
            var role = _unitOfWork.Repository<SecurityRole>().Table().FirstOrDefault(c => c.SecurityRoleId == roleId);


            if (role == null)
            {
                throw new Exception("role not found");
            }

            role.Deleted = true;
            role.IsActive = false;
            _unitOfWork.Repository<SecurityRole>().Update(role);
            _unitOfWork.Save();
        }

        public List<vRolePermission> GetAllRoleWithPermissionData()
        {
            return _unitOfWork.Repository<vRolePermission>().Table().ToList();

        }
        public void AddOrUpdatePermission(SecurityRolePermissionEntityModel securityRolePermissionEntityModel)
        {
          
                //insert
                Mapper.CreateMap<SecurityRolePermissionEntityModel, SecurityRolePermission>();
                var securityRolePermission = Mapper.Map<SecurityRolePermissionEntityModel, SecurityRolePermission>(securityRolePermissionEntityModel);

                foreach (var permissionItem in securityRolePermissionEntityModel.RolePermissionList)
                {

                    var getpermission = _unitOfWork.Repository<SecurityRolePermission>().Table().FirstOrDefault(c => c.SecurityRoleId == permissionItem.SecurityRoleId && c.ModuleId == permissionItem.ModuleId);

                    if (getpermission != null) {
                        _unitOfWork.Repository<SecurityRolePermission>().Remove(getpermission);
                        _unitOfWork.Save();

                    }

                    securityRolePermission.CreatedBy = 1;
                    securityRolePermission.CreatedDate = DateTime.Now;
                    securityRolePermission.CanAdd = permissionItem.CanAdd;
                    securityRolePermission.CanDelete = permissionItem.CanDelete;
                    securityRolePermission.CanEdit = permissionItem.CanEdit;
                    securityRolePermission.CanView = permissionItem.CanView;
                    securityRolePermission.IsFullControl = permissionItem.IsFullControl;
                    securityRolePermission.ModuleId = permissionItem.ModuleId;
                    securityRolePermission.SecurityRoleId = permissionItem.SecurityRoleId;
                    securityRolePermission.LanguageId = securityRolePermissionEntityModel.LanguageId;

                    _unitOfWork.Repository<SecurityRolePermission>().Add(securityRolePermission);
                    _unitOfWork.Save();
                }
            
        }
    }
}
