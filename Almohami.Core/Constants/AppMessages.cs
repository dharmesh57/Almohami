using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Core.Constants
{
    public static class AppMessages
    {
        public static readonly string DisabledAccountLogin = "Your account has been disabled, please contact administrator";
        public static readonly string DeleteConfirmMessage = "Are you sure you want to delete this {0}?";
        public static readonly string RecordSavedSuccessfully = "Record saved successfully.";
        public static readonly string CorrectErrors = "Please correct the following errors.";
        public static readonly string AlreadyExist = "{0} already exist";
        public static readonly string LoginFailed = "User Login Failed";
        public static readonly string CantDelete = "This {0} can't be deleted since it is already assigned to {1}";
        public static readonly string DeleteSuccessfully = "{0} deleted successfully";
        public static readonly string MaxLength = "Only {0} characters are allowed";
        public static readonly string EmailError = "Email does not exist";
        public static readonly string Password = "Old password do not match";
        public static readonly string DeleteSucessfully = "Record deleted successfully";
        public static readonly string FileUploadLimitExceeded = "You can not upload more than 10 MB";
        public static readonly string MainSentSuccessfully = "Mail has been sent successfully";
        public static readonly string PasswordChange = "Password has been changed successfully";
        public static readonly string InActiveUser = "This user is Inactive";
        public static readonly string DeletedUSer = "This user is deleted";
        public static readonly string TicketScheduleChanged = "Ticket #{0} schedule has been changed from {1} to {2} by {3}";
        public static readonly string ChangesSavedSuccessfully = "Changes saved successfully.";
        public static readonly string InActiveEngineer = "User you want to inactive have already assigned ticket(s).";
        public static readonly string InActiveCustomer = "Customer you want to inactive have already assigned ticket(s).";
        public static readonly string InActiveSite = "Site you want to inactive have already assigned ticket(s).";
        public static readonly string ReplenishSpareSucessfully = "Spare replenished successfully";
        public static readonly string SomethingWentWrong = "Something went please try again";
    }
}
