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
    }
}