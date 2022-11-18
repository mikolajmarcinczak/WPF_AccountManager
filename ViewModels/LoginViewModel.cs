using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AccountManager.Models;
using AccountManager.Properties;
using AccountManager.Repository;
using AccountManager.Services.Implementations;
using AccountManager.Services.Interfaces;
using AccountManager.Utilities;

namespace AccountManager.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly MainWindow mainWindow;
        private readonly IUsersService usersService;

        private bool _isVisible = true;
        public bool IsVisible { 
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));

            }
        }

        private string _emailLogin;
        private string _passwordLogin;

        private string _emailRegister;
        private string _passwordRegister;
        private string _usernameRegister;
        private string _surnameRegister;
        private string _phoneRegister;

        private string _errorMessage;


        public string EmailLogin {
            get => _emailLogin;
            set {
                _emailLogin = value;
                OnPropertyChanged(nameof(EmailLogin));
            }
        }
        public string PasswordLogin {
            get => _passwordLogin;
            set {
                _passwordLogin = value;
                OnPropertyChanged(nameof(PasswordLogin));
            }
        }
        public string EmailRegister {
            get => _emailRegister;
            set
            {
                _emailRegister = value;
                OnPropertyChanged(nameof(EmailRegister));
            }
        }
        public string PasswordRegister {
            get => _passwordRegister;
            set
            {
                _passwordRegister = value;
                OnPropertyChanged(nameof(PasswordRegister));
            }
        }
        public string UsernameRegister {
            get => _usernameRegister;
            set
            {
                _usernameRegister = value;
                OnPropertyChanged(nameof(UsernameRegister));
            }
        }
        public string SurnameRegister {
            get => _surnameRegister;
            set
            {
                _surnameRegister = value;
                OnPropertyChanged(nameof(SurnameRegister));
            }
        }
        public string PhoneRegister {
            get => _phoneRegister;
            set {
                _phoneRegister = value;
                OnPropertyChanged(nameof(PhoneRegister));
            }
        }
        public string ErrorMessage {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }

        public LoginViewModel()
        {
            usersService = new UsersService(new AccountManagerDBFirstContext());
            mainWindow = new MainWindow();

            LoginCommand = new RelayCommand(Login, CanLoginCommand);
            RegisterCommand = new RelayCommand(Register);
        }


        private bool CanLoginCommand()
        {
            bool valid;
            if (string.IsNullOrEmpty(EmailLogin) || string.IsNullOrEmpty(PasswordLogin))
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
                Email = EmailLogin.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = PasswordLogin.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Login(user);

            if (result == true)
            {
                Settings.Default.Email= user.Email;
                //mainWindow.Show();
                IsVisible = false;
            }
            else
            {
                ErrorMessage = "Incorrect mail or password.";
            }
        }

        private void Register()
        {
            var user = new User()
            {
                Email = EmailRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                Password = PasswordRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                UserName = UsernameRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                Surname = SurnameRegister.Replace(Environment.NewLine, "").Replace("\n", ""),
                PhoneNumber = PhoneRegister.Replace(Environment.NewLine, "").Replace("\n", "")
            };

            var result = usersService.Register(user);

            if (result == true)
            {
                Settings.Default.Email = user.Email;
                //mainWindow.Show();
                IsVisible = false;
            }
            else
            {
                ErrorMessage = "Error while signing up.";
            }

            //mainWindow.Show();
            IsVisible = false;
        }
    }
}
