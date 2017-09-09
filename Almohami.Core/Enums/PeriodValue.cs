using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Core.Enums
{
    /// <summary>
    /// Rescheduling time period options
    /// </summary>
    public enum PeriodValue
    {
        Week = 1,
        Month = 2,
        Quarter = 3,
        [Description("N/A")]
        NA = 4
    }
}
