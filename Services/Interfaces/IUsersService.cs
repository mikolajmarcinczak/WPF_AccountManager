using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Models;

namespace AccountManager.Services.Interfaces
{
    internal interface IUsersService
    {
        bool Register(User user);
        bool Login(User user);
        public bool EditUser(User user);
        IEnumerable<User> GetUsers();
        int GetUserIdByName(string userName);
    }
}
