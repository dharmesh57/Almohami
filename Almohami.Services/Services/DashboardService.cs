using Almohami.Data.AlmohamiModel;
using Almohami.Data.UnitOfWork;
using Almohami.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Almohami.Services.Services
{
    public class DashboardService :IDashboardService
    {
        #region Private Variables
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public DashboardService()
        {
            _unitOfWork = new UnitOfWork();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all SecurityRolePermission.
        /// </summary>
        /// <returns></returns>
        public List<ApplicationModule> GetAllModuleList()
        {
            return _unitOfWork.Repository<ApplicationModule>().Table().OrderBy(row => row.SortOrder).Where(row => row.Status == true).ToList();

        }

        public List<ApplicationParentModule> GetAllParentModuleList()
        {
            return _unitOfWork.Repository<ApplicationParentModule>().Table().Where(row => row.Status == true).ToList();

        }

        #endregion
    }
}
