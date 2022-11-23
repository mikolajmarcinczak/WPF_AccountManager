using System;
using AccountManager.Models;
using AccountManager.Properties;
using AccountManager.Services.Implementations;
using AccountManager.Services.Interfaces;
using AccountManager.Utilities;

namespace AccountManager.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly IUsersService usersService;

        private LoginProperties _loginProperties = null;
        public LoginProperties LoginProperties
        {
            get => _loginProperties;
            set
            {
                _loginProperties = value;
                OnPropertyChanged(nameof(LoginProperties));
            }
        }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public LoginViewModel()
        {
            usersService = new UsersService(new AccountManagerDBFirstContext());

            this.LoginProperties = new LoginProperties();

            LoginCommand = new RelayCommand(Login, CanLoginCommand);
            RegisterCommand = new RelayCommand(Register);
        }


        private bool CanLoginCommand()
        {
            bool valid;
            if (string.IsNullOrEmpty(LoginProperties.EmailLogin) || string.IsNullOrEmpty(LoginProperties.PasswordLogin))
            {
                valid = false;
            }
            else
            {
                valid = true;
            }

            return valid;
        }

        private void Login()
        {
            var user = new User()
            {
                Email = LoginProperties.EmailLogin.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = LoginProperties.PasswordLogin.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Login(user);

            if (result == true)
            {
                Settings.Default.Email= user.Email;
                Settings.Default.UserName= user.UserName;
                LoginProperties.IsVisible = false;
            }
            else
            {
                LoginProperties.ErrorMessage = "Incorrect mail or password.";
            }
        }

        private void Register()
        {
            var user = new User()
            {
                Email = LoginProperties.EmailRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = LoginProperties.PasswordRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                UserName = LoginProperties.UsernameRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                Surname = LoginProperties.SurnameRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                PhoneNumber = LoginProperties.PhoneRegister.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Register(user);

            if (result == true)
            {
                Settings.Default.Email = user.Email;
                Settings.Default.UserName = user.UserName;
                LoginProperties.IsVisible = false;
            }
            else
            {
                LoginProperties.ErrorMessage = "Error while signing up.";
            }

            LoginProperties.IsVisible = false;
        }
    }
}
