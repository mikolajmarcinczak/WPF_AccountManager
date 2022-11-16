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
    public class LoginViewModel : ObservableObject
    {
        private readonly MainWindow mainWindow;
        private readonly IUsersService usersService;
        private readonly Login loginWindow;

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public LoginViewModel(Login loginWindow, IUsersService usersService)
        {
            this.usersService = usersService;
            this.loginWindow = loginWindow;

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login()
        {
            var user = new User()
            {
                Email = loginWindow.LoginMailInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = loginWindow.LoginPasswordInput.Text.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Login(user);

            if (result == true)
            {
                Settings.Default.Email= user.Email;
                mainWindow.Show();
                loginWindow.Close();
            }
            else
            {
                loginWindow.statusLabel.Content = "Incorrect mail or password.";
            }
        }

        private void Register()
        {
            var user = new User()
            {
                Email = loginWindow.RegisterMailInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = loginWindow.RegisterPasswordInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                UserName = loginWindow.RegisterNameInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                Surname = loginWindow.RegisterSurnameInput.Text.Replace(Environment.NewLine, "").Replace("\n", ""),
                PhoneNumber = loginWindow.RegisterPhoneNumberInput.Text.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Register(user);

            if (result == true)
            {
                Settings.Default.Email = user.Email;
                mainWindow.Show();
                loginWindow.Close();
            }
            else
            {
                loginWindow.statusLabelReg.Content = "Error while signing up.";
            }

            mainWindow.Show();
            loginWindow.Close();
        }
    }
}
