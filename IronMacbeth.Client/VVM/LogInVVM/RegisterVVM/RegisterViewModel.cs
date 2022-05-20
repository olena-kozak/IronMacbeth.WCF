using IronMacbeth.BFF.Contract;
using IronMacbeth.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IronMacbeth.Client.VVM.LogInVVM.RegisterVVM
{
    public class RegisterViewModel
    {
        public string Login { get; set; }

        public string Password { private get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int? PhoneNumber { get; set; }


        public ICommand RegisterCommand { get; }
        public ICommand CloseCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterMethod) { CanExecuteFunc = RegisterCanExecute };
            CloseCommand = new RelayCommand(CloseMethod);
        }
        public void RegisterMethod(object parameter)
        {
            var registrationStatus = AnonymousServerAdapter.Instance.Register(Login, Password, Name, Surname, PhoneNumber.Value);

            if (registrationStatus == UserRegistrationStatus.Success)
            {

                if (UserService.LogIn(Login, Password))
                {
                    CloseMethod(parameter);
                }
                else
                {
                    MessageBox.Show("Incorrect username or password", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (registrationStatus == UserRegistrationStatus.UserNameAlreadyTaken)
            {
                MessageBox.Show("Username is already taken.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                throw new NotSupportedException($"{nameof(UserRegistrationStatus)} is not supported. value = '{registrationStatus}'.");
            }
        }

        private bool RegisterCanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(Name) &&
                !string.IsNullOrWhiteSpace(Surname) &&
                PhoneNumber != null;
        }


        private void CloseMethod(object parameter)
        {
            (parameter as Window)?.Close();
        }
    }
}
