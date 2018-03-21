using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsWebSite.Components
{
    public class UserManager
    {
        public static string GetAuthenticatedUser() {
            string user = null;
            try
            {
                user = System.Security.Claims.ClaimsPrincipal.Current.Identity.Name;
            }
            catch
            {
                // Not authenticated
            }
            return user;
        }
    }
}
