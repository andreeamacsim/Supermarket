using Supermarket_Application.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace Supermarket_Application.ViewModels
{
    public class AdministratorViewModel : INotifyPropertyChanged
    {
        public ICommand OpenAddProductCommand { get; private set; }
        public ICommand OpenUpdateProductPriceCommand { get; private set; }
        public ICommand OpenLogicalDeletionCommand { get; private set; }

        public AdministratorViewModel()
        {
            OpenAddProductCommand = new RelayCommand(OpenAddProduct);
            OpenUpdateProductPriceCommand = new RelayCommand(OpenUpdateProductPrice);
            OpenLogicalDeletionCommand = new RelayCommand(OpenLogicalDeletion);
        }

        private void OpenAddProduct(object obj)
        {
            AddProductView addProductView = new AddProductView();
            addProductView.ShowDialog();
        }

        private void OpenUpdateProductPrice(object obj)
        {
            UpdateProductPriceView updateProductPriceView = new UpdateProductPriceView();
            updateProductPriceView.ShowDialog();
        }

        private void OpenLogicalDeletion(object obj)
        {
            LogicalDeletionView logicalDeletionView = new LogicalDeletionView();
            logicalDeletionView.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
