using System;
using System.Collections.Generic;
using System.Linq;
using WINConnect.Models;

namespace WINConnect.Models.Extensions
{
    public static class AgentExtensions
    {
        public static DateTime? GetLastLogin(this ICollection<Contact_Login> logins)
        {
            if (logins == null)
            {
                return null;
            }

            Contact_Login login = logins.OrderByDescending(x => x.LoggedOn).FirstOrDefault();
            if (login == null)
            {
                return null;
            }

            return login.LoggedOn;
        }
    }
}