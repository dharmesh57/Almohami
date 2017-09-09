using Almohami.Core.Helper;
using System.Web;
using System.Web.Mvc;

namespace AlmohamiWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        
    }
}
