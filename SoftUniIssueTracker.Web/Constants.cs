using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIT.Web
{
    public static class Constants
    {
        public const string DefaultSchemeName = "default scheme";
        public const string UnexistingStatusErrorMessage = "A status with this id doesn't exist";
        public const string UnexistingIssueErrorMessage = "An issue with this id doesn't exist";
        public const string UnexistingProjectErrorMessage = "A project with this id doesn't exist";
        public const string UnexistingUserErrorMessage = "An user with this id doesn't exist";
        public const string UnexistingPriorityForProjectErrorMessage = "There doesn't exist a priority with this id for this project";
        public const string UnexistingTransitionSchemeErrorMessage = "A transition scheme with this id doesn't exist";
        public const string UnavailableStatusForIssue = "The status with this id isn't available for this issue";


    }
}
