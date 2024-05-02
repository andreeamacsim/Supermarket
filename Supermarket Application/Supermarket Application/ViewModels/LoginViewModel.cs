using Supermarket_Application.Helpers;
using System.Windows.Input;
using Microsoft.Win32;
using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;

using Supermarket_Application.Views;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Windows;

namespace Supermarket_Application.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
               OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
               OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private UserRepository _userRepository;

        public LoginViewModel()
        {
            _userRepository = new UserRepository(new SupermarketDbContext());
             LoginCommand = new RelayCommand(Login);
             RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object parameter)
        {
            var user = _userRepository.Authenticate(Username, Password);
            if (user != null && user.IsActive)
            {
                if (user.UserType == "Administrator")
                {
                    var adminView = new AdministratorView();
                    adminView.Show();
                }
                else if (user.UserType == "Cashier")
                {
                    var cashierView = new CashierView();
                    cashierView.Show();
                }
                
            }
            else MessageBox.Show("The user is unknown.Register please.", "Autentificare", MessageBoxButton.OK, MessageBoxImage.Warning);

        }


        private void Register(object parameter)
        {
            RegisterView registerView = new RegisterView();
            registerView.ShowDialog();
        }
    }
}
