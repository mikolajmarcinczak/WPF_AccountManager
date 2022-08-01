using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AccountManager.Models;
using AccountManager.Services.Implementations;
using AccountManager.Services.Interfaces;

namespace AccountManager.ViewModels
{
    public class LoginViewModel
    {
        private readonly IUsersService usersService;

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            this.usersService = usersService;
        }

        private Login()
        {
            var users = new User()
            {
                
            };
        }

        private Register()
        {

        }
    }
}
