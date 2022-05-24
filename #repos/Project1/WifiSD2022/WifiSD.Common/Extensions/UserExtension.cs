using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WifiSD.Core.Entities;

namespace WifiSD.Common.Extensions
{
    public static class UserExtension
    {
        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(s => s.WithoutPassword());
        }


    }
}
