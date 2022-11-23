using AccountManager.Utilities;

namespace AccountManager.Models
{
    public class LoginProperties : ObservableObject
    {
        private bool _isVisible = true;

        private string _emailLogin = "";
        private string _passwordLogin = "";

        private string _emailRegister = "";
        private string _passwordRegister = "";
        private string _usernameRegister = "";
        private string _surnameRegister = "";
        private string _phoneRegister = "";

        private string _errorMessage = "";

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));

            }
        }

        public string EmailLogin
        {
            get => _emailLogin;
            set
            {
                _emailLogin = value;
                OnPropertyChanged(nameof(EmailLogin));
            }
        }
        public string PasswordLogin
        {
            get => _passwordLogin;
            set
            {
                _passwordLogin = value;
                OnPropertyChanged(nameof(PasswordLogin));
            }
        }

        public string EmailRegister
        {
            get => _emailRegister;
            set
            {
                _emailRegister = value;
                OnPropertyChanged(nameof(EmailRegister));
            }
        }
        public string PasswordRegister
        {
            get => _passwordRegister;
            set
            {
                _passwordRegister = value;
                OnPropertyChanged(nameof(PasswordRegister));
            }
        }
        public string UsernameRegister
        {
            get => _usernameRegister;
            set
            {
                _usernameRegister = value;
                OnPropertyChanged(nameof(UsernameRegister));
            }
        }
        public string SurnameRegister
        {
            get => _surnameRegister;
            set
            {
                _surnameRegister = value;
                OnPropertyChanged(nameof(SurnameRegister));
            }
        }
        public string PhoneRegister
        {
            get => _phoneRegister;
            set
            {
                _phoneRegister = value;
                OnPropertyChanged(nameof(PhoneRegister));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
    }
}
