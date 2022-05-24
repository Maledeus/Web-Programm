using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WifiSD.Common.Extensions;
using WifiSD.Core.Entities;

namespace WifiSD.Common.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User",
                Username = "Test",
                Password = new NetworkCredential("Test", "test").SecurePassword
            }

        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(x => string.Compare(x.Username, username, true) == 0
                                                  && new NetworkCredential(x.Username, x.Password).Password == password);

            if (user == null)
            {
                return null;
            }

            return await Task.FromResult(user.WithoutPassword());

        }
    }
}
