using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AccountManager.Models;
using AccountManager.Services.Implementations;
using AccountManager.Services.Interfaces;
using AccountManager.Utilities;

namespace AccountManager.ViewModels
{
    public class LoginViewModel
    {
        private readonly MainWindow mainWindow;
        private readonly IUsersService usersService;

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(MainWindow mainWindow, IUsersService UsersService)
        {
            this.usersService = UsersService;
            this.mainWindow = mainWindow;

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login()
        {
            var user = new User()
            {
                Email = LoginMailInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = LoginPasswordInput.Text.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Login(user);

            if (result == true)
            {
                Settings.Default.Email= user.Email;
                mainWindow.Show();
                Close();
            }
            else
            {
                statusLabel.Content = "Incorrect mail or password";
            }
        }

        private void Register()
        {
            var user = new User()
            {
                Email = RegisterMailInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = RegisterPasswordInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                UserName = RegisterNameInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                Surname = RegisterSurnameInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                PhoneNumber = RegisterPhoneNumberInput.Text.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Register(user);

            if (result == true)
            {
                Settings.Default.Email = user.Email;
                mainWindow.Show();
                Close();
            }
            else
            {
                statusLabelReg.Content = "Error while signing up";
            }

            mainWindow.Show();
            Close();
        }
    }
}
