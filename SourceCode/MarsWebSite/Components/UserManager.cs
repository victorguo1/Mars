using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarsWebSite.Components
{
    public class UserManager
    {
        public static string GetAuthenticatedEmail() {
            string email = null;
            try
            {
                email = System.Security.Claims.ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email).Value; 
            }
            catch
            {
                // Not authenticated
            }
            return email;
        }

        public static string GetAuthenticatedUser()
        {
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
