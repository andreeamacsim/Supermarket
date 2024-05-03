
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using System.Windows.Controls;

namespace Supermarket_Application.ViewModels
{
    public class RegisterViewModel 
    {
        private string _username;
        private string _password;
        private string _userType;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string UserType
        {
            get { return _userType; }
            set
            {
                _userType = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        public ICommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object parameter)
        {
            
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter a username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(UserType))
            {
                MessageBox.Show("Please select a user type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                Username = Username,
                PasswordHash = Password,
                UserType = UserType,
                IsActive = true
            };


            using (var db = new SupermarketDbContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }





        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
