using Almohami.Data.AlmohamiModel;
using Almohami.Data.UnitOfWork;
using Almohami.Services.Contracts;
using Almohami.Services.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Services
{
    public class AccountLoginServices : IAccountLoginServices
    {

        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public AccountLoginServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all SecurityRolePermission.
        /// </summary>
        /// <returns></returns>
        public List<AccountLoginServices> GetAllData()
        {
            return _unitOfWork.Repository<AccountLoginServices>().Table().ToList();
        }
        //public User CheckLogin(LoginModel loginmodel)
        //{
        //    return _unitOfWork.Repository<User>().Table().Where(row => row.UserPassword == loginmodel.Password && row.UserEmailId == loginmodel.Email).FirstOrDefault();

        //}
        public User CheckLogin(LoginModel loginmodel)
        {
            var param1 = new SqlParameter();
            param1.ParameterName = "@Email";
            param1.SqlDbType = SqlDbType.NVarChar;
            param1.SqlValue = loginmodel.Email;

            var param2 = new SqlParameter();
            param2.ParameterName = "@Password";
            param2.SqlDbType = SqlDbType.NVarChar;
            param2.SqlValue = loginmodel.Password;

            return _unitOfWork.SQLQuery<User>("sp_CheckLogin @Email,@Password", param1, param2).FirstOrDefault();

        }
        public User getUser(string Emailid)
        {
            return _unitOfWork.Repository<User>().Table().FirstOrDefault(row => row.UserEmailId == Emailid);
        }
        public bool CheckEmailIdExist(RegisterModel model)
        {
            bool flag = false;
            var userdata = _unitOfWork.Repository<User>().Table().Where(row => row.UserEmailId == model.Email).ToList();
            if (userdata != null && userdata.Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        public RegisterModel AddUser(RegisterModel registermodel)
        {

            //insert
            Mapper.CreateMap<RegisterModel, User>();
            var userdata = Mapper.Map<RegisterModel, User>(registermodel);

            userdata.UserOTP = registermodel.HiddenOTP;
            userdata.UserName = registermodel.Name;
            userdata.UserCompany = registermodel.Company;
            userdata.UserMobileNo = registermodel.MobileNo;
            userdata.UserEmailId = registermodel.Email;
            userdata.UserPassword = registermodel.Password;
            userdata.UserEmailConfirmed = false;
            userdata.UserPhoneConfirmed = false;
            userdata.UserStatus = false;
            userdata.UserDelete = false;
            userdata.CreatedBy = 1;
            userdata.CreatedDate = DateTime.Now;
            _unitOfWork.Repository<User>().Add(userdata);
            _unitOfWork.Save();

            registermodel.UserID = userdata.UserId;
            return registermodel;

        }
        #endregion

        public bool UpdateUserOTP(int userID, string newOTP)
        {
            // update
            var user = _unitOfWork.Repository<User>().Table().FirstOrDefault(c => c.UserId == userID);

            if (user == null)
            {
                return false;
            }
            user.UserOTP = newOTP;
            _unitOfWork.Repository<User>().Update(user);
            _unitOfWork.Save();

            return true;
        }

        public bool ActiveUser(Int64 userid)
        {
            // update
            var user = _unitOfWork.Repository<User>().Table().FirstOrDefault(c => c.UserId == userid);

            if (user == null)
            {
                return false;
            }
            user.UserEmailConfirmed = true;
            user.UserStatus = false;
            user.UserDelete = false;

            _unitOfWork.Repository<User>().Update(user);
            _unitOfWork.Save();

            return true;
        }
        public LoginModel GetUserById(Int64 id)
        {

            return (from user in _unitOfWork.Repository<User>().Table()

                    where user.UserId == id && user.UserDelete == false
                    select new LoginModel
                    {
                        UserID = user.UserId,
                        Name = user.UserName,
                        Email = user.UserEmailId

                    }).FirstOrDefault();
        }

        public bool CheckCurrentOTP(string currentOTP, int userId)
        {

            bool otp = _unitOfWork.Repository<User>().Table().Where(row => row.UserOTP == currentOTP && row.UserId == userId).Any();
            return otp;
        }

        public bool updateUserPhoneConfirm(int userId)
        {
            // update
            var user = _unitOfWork.Repository<User>().Table().FirstOrDefault(c => c.UserId == userId);

            if (user == null)
            {
                return false;
            }

            user.UserPhoneConfirmed = true;
            _unitOfWork.Repository<User>().Update(user);
            _unitOfWork.Save();
            return true;

        }
        public User CheckTokenAndId(long Id, string token)
        {
            return _unitOfWork.Repository<User>().Table().FirstOrDefault(row => row.UserId == Id && row.PasswordVerificationToken == token);
        }
        public bool UpdatePassword(long id, string Password, string VCode)
        {
            bool flag = false;
            if (id > 0)
            {
                // update
                var userdata = _unitOfWork.Repository<User>().Table().FirstOrDefault(c => c.UserId == id);

                if (userdata == null)
                {
                    throw new Exception("User not found");
                }
                userdata.UserPassword = Password ?? userdata.UserPassword;
                userdata.UserVCode = VCode ?? userdata.UserVCode;
                userdata.LastPasswordModified = DateTime.Now;
                userdata.LastModifiedDate = DateTime.Now;
                userdata.LastModifiedBy = 1;
                _unitOfWork.Repository<User>().Update(userdata);
                _unitOfWork.Save();
                flag = true;
            }
            return flag;
        }

        public bool UpdatePasswordToken(long id, string Token, DateTime TokenExpirationDate)
        {
            bool flag = false;
            if (id > 0)
            {
                // update
                var userdata = _unitOfWork.Repository<User>().Table().FirstOrDefault(c => c.UserId == id);

                if (userdata == null)
                {
                    throw new Exception("User not found");
                }
                userdata.PasswordVerificationToken = Token ?? userdata.PasswordVerificationToken;
                userdata.PasswordVerificationTokenExpirationDate = TokenExpirationDate;
                userdata.LastModifiedDate = DateTime.Now;
                userdata.LastModifiedBy = 1;
                _unitOfWork.Repository<User>().Update(userdata);
                _unitOfWork.Save();
                flag = true;
            }
            return flag;
        }
    }
}
