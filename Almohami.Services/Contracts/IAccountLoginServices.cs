using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using Almohami.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Contracts
{
    public interface IAccountLoginServices
    {
        List<AccountLoginServices> GetAllData();
        User CheckLogin(LoginModel a);
        RegisterModel AddUser(RegisterModel registermodel);
        LoginModel GetUserById(Int64 id);

        bool CheckCurrentOTP(string currentOTP, int userId);

        bool updateUserPhoneConfirm(int userId);

        bool UpdateUserOTP(int userID, string newOTP);
        bool ActiveUser(Int64 userid);
        bool CheckEmailIdExist(RegisterModel loginmodel);
        User getUser(string Emailid);
        bool UpdatePassword(long id, string Password, string VCode);

        User CheckTokenAndId(long Id, string token);
        bool UpdatePasswordToken(long id, string Token, DateTime TokenExpirationDate);

    }
}
