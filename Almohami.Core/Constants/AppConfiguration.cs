using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Almohami.Core.Constants
{
    public static class AppConfiguration
    {
        public static readonly string TicketDocumentsPath = "~/Uploads/TicketDocuments/";
        public static readonly string TicketReportDocumentsPath = "~/Uploads/TicketDocuments/TicketReport/";
        public static readonly string TempUploadPath = "~/Uploads/Temp/";
        public static readonly long TicketDocumentUploadLimit = Convert.ToInt32(WebConfigurationManager.AppSettings["TicketDocumentUploadLimit"]);
        public static readonly bool NotificationOn = WebConfigurationManager.AppSettings["NotificationOn"].ToString().Equals("1") ? true : false;
        public static readonly int SessionTimeOut = Convert.ToInt32(WebConfigurationManager.AppSettings["SessionTimeOut"]);
        public static readonly int PermanentSessionTime = 525600;
        public static readonly int PasswordLength = 8;
        public static readonly string NewReportAdded = "New report created by {0}.";
        public static readonly string NewDocAdded = "New document {0} uploaded by {1}.";
        public static readonly string NewLogAdded = "New hours logged by {0}.";
        public static readonly string LocalHubPostCodeDefault = WebConfigurationManager.AppSettings["LocalHubPostCodeDefault"];
        public static readonly string CustomerCodeDefault = WebConfigurationManager.AppSettings["CustomerCodeDefault"];
        //public static readonly string CustomerCodeDefault = "GHSO";
    }
}
