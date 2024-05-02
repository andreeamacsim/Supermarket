using Supermarket_Application.Views;
using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Windows.Input;

namespace Supermarket_Application.ViewModels
{
    public class MainViewModel
    {
        public ICommand OpenLoginCommand { get; private set; }

        public MainViewModel()
        {
            OpenLoginCommand = new RelayCommand(OpenLogin);
        }

        private void OpenLogin(object parameter)
        {
           
            var loginView = new LoginView();
            loginView.Show();
        }
    }


    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }



}

