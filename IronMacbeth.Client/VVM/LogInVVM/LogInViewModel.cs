using IronMacbeth.BFF.Contract;
using IronMacbeth.Client.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace IronMacbeth.Client.VVM.LogInVVM
{
    public class LogInViewModel
    {
        private bool _loginMode;

        public string ButtonContent => _loginMode ? "Log in" : "Register";

        public string Login { get; set; }
        public string Password { private get; set; }
        

        public ICommand LogInCommand { get; }
        public ICommand CloseCommand { get; }

        public LogInViewModel(bool loginMode = true)
        {
            _loginMode = loginMode;
            LogInCommand = new RelayCommand(LogInMethod) { CanExecuteFunc = LogInCanExecute };
            CloseCommand = new RelayCommand(CloseMethod);
        }

        public void LogInMethod(object parameter)
        {
            //if (_loginMode)
            //{
                if (UserService.LogIn(Login, Password))
                {
                    CloseMethod(parameter);
                }
                else
                {
                    MessageBox.Show("Incorrect username or password", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            //}
            //else
            //{
            //    var registrationStatus = AnonymousServerAdapter.Instance.Register(Login, Password);

            //    if (registrationStatus == UserRegistrationStatus.Success)
            //    {
            //        _loginMode = true;

            //        LogInMethod(parameter);
            //    }
            //    else if (registrationStatus == UserRegistrationStatus.UserNameAlreadyTaken)
            //    {
            //        MessageBox.Show("Username is already taken.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    else
            //    {
            //        throw new NotSupportedException($"{nameof(UserRegistrationStatus)} is not supported. value = '{registrationStatus}'.");
            //    }
            //}
        }

        private bool LogInCanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void CloseMethod(object parameter)
        {
            (parameter as Window)?.Close();
        }
    }
}