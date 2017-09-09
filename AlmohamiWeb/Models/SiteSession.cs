using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;

namespace AlmohamiWeb.Models
{
    public class SiteSession
    {

        /// <summary>
        /// Gets or sets the current UI culture.
        /// </summary>
        /// <remarks>
        /// Values meaning: 0 = InvariantCulture (en-US), 1 = ro-RO, 2 = de-DE.
        /// </remarks>
        public static int CurrentUICulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "ro-RO")
                    return 1;
                else if (Thread.CurrentThread.CurrentUICulture.Name == "de-DE")
                    return 2;
                else
                    return 0;
            }
            set
            {
                //
                // Set the thread's CurrentUICulture.
                //
                if (value == 1)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
                else if (value == 2)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
                else
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                //
                // Set the thread's CurrentCulture the same as CurrentUICulture.
                //
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }
        public SiteSession()
        {
            
            //
            // TO DO: Cache other user settings!
            //
        }
    }
}
