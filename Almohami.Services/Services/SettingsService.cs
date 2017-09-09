using Almohami.Data.AlmohamiModel;
using Almohami.Data.UnitOfWork;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Almohami.Services.Services
{
    public class SettingsService : ISettingsService
    {
        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Controller
        public SettingsService()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region Public Methods
        public UserEntityModel GetUserById(Int64 userid)
        {
            return (from user in _unitOfWork.Repository<User>().Table()
                    where user.UserId == userid && user.UserDelete == false
                    select new UserEntityModel
                    {
                        UserID = user.UserId,
                        Name = user.UserName,
                        Email = user.UserEmailId,
                        MobileNo = user.UserMobileNo,
                        UserAvtar = user.UserAvtar,

                    }
                        ).FirstOrDefault();
        }
        public void AddOrUpdateUser(UserEntityModel userentitymodel)
        {
            //update
            if (userentitymodel.UserID > 0)
            {
                var userdata = _unitOfWork.Repository<User>().Table().FirstOrDefault(m => m.UserId == userentitymodel.UserID);
                if (userdata == null)
                {
                    throw new Exception("User not found");
                }
                userdata.UserAvtar = userentitymodel.UserAvtar;
                userdata.UserEmailId = userentitymodel.Email;
                userdata.UserPassword = userentitymodel.Password;
                userdata.UserRoleId = userentitymodel.RoleId;
                userdata.UserMobileNo = userentitymodel.MobileNo;
                userdata.UserStatus = userentitymodel.Status;
                userdata.UserName = userentitymodel.Name;
                userdata.LastModifiedDate = DateTime.Now;
                userdata.LastModifiedBy = 1;
                _unitOfWork.Repository<User>().Update(userdata);
                _unitOfWork.Save();
            }
        }
        #endregion

        public User CheckPassword(ChangePasswordModel changepasswordentitymodel)
        {
            var userdata = _unitOfWork.Repository<User>().Table().FirstOrDefault(m=>m.UserId==changepasswordentitymodel.UserID &&m.UserPassword==changepasswordentitymodel.OldPassword);
            return userdata;
        }

        public void UpdatePassword(ChangePasswordModel changepasswordentitymodel)
        {
            if (changepasswordentitymodel.UserID > 0)
            {
                var userdata = _unitOfWork.Repository<User>().Table().FirstOrDefault(m => m.UserId == changepasswordentitymodel.UserID);
                if (userdata == null)
                {
                    throw new Exception("User not found");
                }
                userdata.UserPassword = changepasswordentitymodel.NewPassword;
                userdata.LastModifiedDate = DateTime.Now;
                userdata.LastModifiedBy = 1;
                _unitOfWork.Repository<User>().Update(userdata);
                _unitOfWork.Save();
            }
        }
    }
}
