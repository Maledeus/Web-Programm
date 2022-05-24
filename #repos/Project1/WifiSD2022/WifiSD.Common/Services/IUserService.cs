using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WifiSD.Core.Entities;

namespace WifiSD.Common.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);

    }
}
