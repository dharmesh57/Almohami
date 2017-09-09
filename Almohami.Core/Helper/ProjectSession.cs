using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Configuration;
using System.Web;

namespace Almohami.Core.Helper
{
    public class ProjectSession
    {
        /// <summary>
        /// return User Id
        /// </summary>
        //    public static long UserId
        //    {
        //        get
        //        {
        //            if (HttpContext.Current.Session["UserId"] == null)
        //                return 0;
        //            else
        //                return Convert.ToInt64(HttpContext.Current.Session["UserId"].ToString());
        //        }
        //        set
        //        {
        //            HttpContext.Current.Session["UserId"] = value;
        //        }
        //    }

        //    /// <summary>
        //    ///Return UserName
        //    /// </summary>
        //    public static string UserName
        //    {
        //        get
        //        {
        //            if (HttpContext.Current.Session["UserName"] == null)
        //                return "";
        //            else
        //                return HttpContext.Current.Session["UserName"].ToString();
        //        }
        //        set
        //        {
        //            HttpContext.Current.Session["UserName"] = value;
        //        }
        //    }

        /// <summary>
        /// Gets or Sets the current User in Session
        /// </summary>
        //public static User User
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["User"] == null)
        //            return null;
        //        else
        //            return (User)HttpContext.Current.Session["User"];
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["User"] = value;
        //    }
        //}
    }
}
