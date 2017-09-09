using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Entities
{
    public class DashboardModel:BaseViewModel
    {

        public DashboardModel()
        {

            ModuleList = new List<ApplicationModule>();
            ParentModuleList = new List<ApplicationParentModule>();
            vRolePermissionList = new List<vRolePermission>();
         //   CurrentUser = new User();
        }
        public List<ApplicationModule> ModuleList { get; set; }
        public List<vRolePermission> vRolePermissionList { get; set; }
        public List<ApplicationParentModule> ParentModuleList { get; set; }
        public long SecurityRoleId { get; set; }
        public int LanguageId { get; set; }
        public User CurrentUser { get; set; }


    }
}
