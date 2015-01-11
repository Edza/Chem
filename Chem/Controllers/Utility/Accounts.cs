using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;

namespace Chem.Controllers.Utility
{
    public static class Accounts
    {
        public static bool IsAdmin()
        {
            var roles = Roles.GetRolesForUser(WebSecurity.CurrentUserName);

            if (roles != null && roles.Contains("Administrator"))
                return true;
            else
                return false;
        }

        public static bool CanDoThis(int addedBy)
        {
            bool canDoThis = false;
            if (addedBy == WebSecurity.CurrentUserId)
                canDoThis = true;
            else if (Accounts.IsAdmin())
                canDoThis = true;
            return canDoThis;
        }
    }
}