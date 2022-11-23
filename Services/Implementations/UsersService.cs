using AccountManager.Models;
using AccountManager.Repository;
using AccountManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly AccountManagerDBFirstContext context;
        public UsersService(AccountManagerDBFirstContext context)
        {
            this.context = context;
        }

        public bool EditUser(User user)
        {
            var userdb = context.Users.Where(u => u.Id == user.Id).SingleOrDefault();

            try
            {
                userdb.PhoneNumber = user.PhoneNumber;
                userdb.UserName = user.UserName;
                userdb.Surname = user.Surname;
                userdb.Email = user.Email;
                userdb.Password = user.Password;
                userdb.IsPaid = user.IsPaid;
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public bool Login(User user)
        {
            var userdb = GetUsers();

            if (userdb.Where(u => u.Email == user.Email && u.Password == user.Password).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public int GetUserIdByName(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email).Id;
        }
    }
}
