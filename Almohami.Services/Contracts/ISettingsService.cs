using Almohami.Data.AlmohamiModel;
using Almohami.Services.Entities;
using System;
using System.Collections.Generic;
namespace Almohami.Services.Contracts
{
   public interface ISettingsService
    {
       UserEntityModel GetUserById(Int64 userid);
       void AddOrUpdateUser(UserEntityModel userentitymodel);
       User CheckPassword(ChangePasswordModel changepasswordentitymodel);

       void UpdatePassword(ChangePasswordModel changepasswordentitymodel);
    }
}
