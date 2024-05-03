using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using Supermarket_Application.Views;

namespace Supermarket_Application.ViewModels
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Manufacturer> _manufacturers;
        private Product _product;

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ObservableCollection<Manufacturer> Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public ICommand AddProductCommand { get; private set; }
        public ICommand OpenAddStockCommand { get; private set; }

        public AddProductViewModel()
        {
            Product = new Product();
            LoadCategories();
            LoadManufacturers();
            AddProductCommand = new RelayCommand(AddProduct);
            OpenAddStockCommand = new RelayCommand(OpenAddStock);
        }
        private void OpenAddStock(object parameter)
        {
            AddStockView addStockView = new AddStockView();
            addStockView.ShowDialog();
        }
        private void LoadCategories()
        {
            using (var db = new SupermarketDbContext())
            {
                Categories = new ObservableCollection<Category>(db.Categories.Where(c => c.IsActive).ToList());
            }
        }

        private void LoadManufacturers()
        {
            using (var db = new SupermarketDbContext())
            {
                Manufacturers = new ObservableCollection<Manufacturer>(db.Manufacturers.Where(m => m.IsActive).ToList());
            }
        }

        private void AddProduct(object parameter)
        {
            using (var db = new SupermarketDbContext())
            {
                db.Products.Add(Product);
                db.SaveChanges();

                System.Windows.MessageBox.Show("Product added successfully.", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
