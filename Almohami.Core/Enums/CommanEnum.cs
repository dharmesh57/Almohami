using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Core.Enums
{
    public enum AppointmentStatus
    {
        [Description("#FF0000:PENDING")] // red
        Pending = 0,

        [Description("#FF8000:INPROGRESS")] // orange
        InProgress,

        [Description("#01DF3A:COMPLETED")] // green
        Completed

    }

    public enum Language
    {

        English = 1,
        Arabic = 2
    }
    public enum Role
    {

        MasterAdmin = 26,
        SystemAdmin = 28,
        LawyerAdmin = 30,
        User = 32,
        AraMasterAdmin = 33,
        AraSystemAdmin = 34,
        AraLawyerAdmin = 41,
        AraUser = 42
    }
}
