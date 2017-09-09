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
    public class UserService : IUserServices
    {
        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>

        public List<User> GetAllUsers(UserEntityModel model)
        {
            return _unitOfWork.Repository<User>().Table().Where(c => c.UserDelete == model.IsDeleted).ToList();
        }

        public List<User> GetAllActiveUsers(UserEntityModel model)
        {
            return _unitOfWork.Repository<User>().Table().Where(c => c.UserDelete == false && c.UserStatus == true).ToList();
        }

        public List<User> GetAllInactiveUsers(UserEntityModel model)
        {
            return _unitOfWork.Repository<User>().Table().Where(c => c.UserDelete == false && c.UserEmailConfirmed == true && c.UserPhoneConfirmed == true && c.UserStatus == false).ToList();
        }
        public void AddorUpdateUser(UserEntityModel userentitymodel)
        {
            if (userentitymodel.UserID > 0)
            {
                // update
                var userdata = _unitOfWork.Repository<User>().Table().FirstOrDefault(c => c.UserId == userentitymodel.UserID);

                if (userdata == null)
                {
                    throw new Exception("User not found");
                }

                userdata.UserName = userentitymodel.Name ?? userdata.UserName;
                userdata.UserEmailId = userentitymodel.Email ?? userdata.UserEmailId;
                userdata.UserMobileNo = userentitymodel.MobileNo ?? userdata.UserMobileNo;
                userdata.UserRoleId = userentitymodel.RoleId ?? userdata.UserRoleId;
                userdata.UserStatus = userentitymodel.Status ?? userdata.UserStatus;
                userdata.UserDelete = userentitymodel.IsDeleted ?? userdata.UserDelete;
                userdata.UserApprovedBy = userentitymodel.UserApprovedBy ?? userdata.UserApprovedBy;
                userdata.UserRejectedBy = userentitymodel.UserRejectedBy ?? userdata.UserRejectedBy;
                userdata.LastModifiedDate = DateTime.Now;
                userdata.LastModifiedBy = 1;
                _unitOfWork.Repository<User>().Update(userdata);
                _unitOfWork.Save();
            }
            else
            {
                Mapper.CreateMap<UserEntityModel, User>();
                var userdata = Mapper.Map<UserEntityModel, User>(userentitymodel);

                userdata.UserName = userentitymodel.Name;
                userdata.UserCompany = userentitymodel.Company;
                userdata.UserMobileNo = userentitymodel.MobileNo;
                userdata.UserEmailId = userentitymodel.Email;
                userdata.UserPassword = userentitymodel.Password;
                userdata.UserEmailConfirmed = false;
                userdata.UserPhoneConfirmed = false;
                userdata.UserStatus = false;
                userdata.UserDelete = false;
                userdata.CreatedBy = userentitymodel.CreatedBy;
                userdata.CreatedDate = DateTime.Now;
                _unitOfWork.Repository<User>().Add(userdata);
                _unitOfWork.Save();
            }
        }

        public UserEntityModel GetUserById(Int64 id)
        {

            return (from user in _unitOfWork.Repository<User>().Table()
                    where user.UserId == id && user.UserDelete == false
                    select new UserEntityModel
                    {
                        UserID = user.UserId,
                        Name = user.UserName,
                        Email = user.UserEmailId,
                        //RoleName = _unitOfWork.Repository<SecurityRolePermissionEntityModel>().Table().GetRoleById(user.UserRoleId).Select(c => c.RoleName).ToString()
                        RoleName = "Admin"

                    }).FirstOrDefault();
        }

        public long AddorUpdateUserLog(UserLogEntityModel userlogentitymodel)
        {
            if (userlogentitymodel.UserLogID > 0)
            {
                //Update
                var logdata = _unitOfWork.Repository<UserLog>().Table().FirstOrDefault(row => row.UserLogID == userlogentitymodel.UserLogID);
                if (logdata == null)
                {
                    throw new Exception("User Log Details Not Found !");
                }
                //logdata.UserLogUserID =(long?)userlogentitymodel.UserLogUserID ?? logdata.UserLogUserID;
                //logdata.UserLogRole = (long?)userlogentitymodel.UserLogRole ?? logdata.UserLogRole;
                logdata.UserStatus = userlogentitymodel.UserStatus ?? logdata.UserStatus;
                logdata.LastModifiedDate = DateTime.Now;
                logdata.LastModifiedBy = 1;
                _unitOfWork.Repository<UserLog>().Update(logdata);
                _unitOfWork.Save();
                return logdata.UserLogID;
            }

            else
            {
                //Insert
                Mapper.CreateMap<UserLogEntityModel, UserLog>();
                var userlogdata = Mapper.Map<UserLogEntityModel, UserLog>(userlogentitymodel);
                userlogdata.UserLogUserID = userlogentitymodel.UserLogUserID;
                // userlogdata.UserLogRole = userlogentitymodel.UserLogRole;
                userlogdata.UserStatus = userlogentitymodel.UserStatus;
                userlogdata.CreatedDate = DateTime.Now;
                userlogdata.CreatedBy = userlogentitymodel.CreatedBy;
                _unitOfWork.Repository<UserLog>().Add(userlogdata);
                _unitOfWork.Save();
                return userlogdata.UserLogID;
            }

        }
        #endregion
    }

}
