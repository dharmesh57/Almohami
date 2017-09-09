using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Core.Enums
{
    /// <summary>
    /// Schedule Maintainance Types
    /// </summary>
    public enum ScheduleMaintainanceType
    {
        Monthly = 1,
        Quarterly = 2,
        BiAnnualLV = 3,
        BiAnnualHV = 4,
        AnnualLV = 5,
        AnnualHV = 6
    }

    /// <summary>
    /// Type of works
    /// </summary>
    public enum TypeOfWorks
    {
        Corrective = 1,
        Preventative = 2,
        Additional = 3,
        Reference = 4,
        Admin = 5,
        Scheduledother = 6
    }

    /// <summary>
    /// Status types
    /// </summary>
    public enum Status
    {
        Active = 1,
        InActive = 0
    }

    /// <summary>
    /// Priority options
    /// </summary>
    public enum Priority
    {
        Major = 1,
        Medium = 2,
        Minor = 3
    }

    public enum ActiveStatus
    {
        Open,
        Pending,
        Close
    }

    public enum WorkStatus
    {
        Pending,
        InProgress,
        Completed
    }
}
