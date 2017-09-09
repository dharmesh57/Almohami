using Almohami.Data.AlmohamiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Services.Contracts
{
    public interface IDashboardService
    {
        List<ApplicationModule> GetAllModuleList();
        List<ApplicationParentModule> GetAllParentModuleList();
    }
}
