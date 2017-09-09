using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Contracts
{
    public interface IUserServices
    {
        List<User> GetAllUsers(UserEntityModel model);
        void AddorUpdateUser(UserEntityModel userentitymodel);
        long AddorUpdateUserLog(UserLogEntityModel userlogentitymodel);
        UserEntityModel GetUserById(Int64 id);
        List<User> GetAllInactiveUsers(UserEntityModel model);
        List<User> GetAllActiveUsers(UserEntityModel model);
    }
}
