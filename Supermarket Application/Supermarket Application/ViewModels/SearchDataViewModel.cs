using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using Supermarket_Application.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Supermarket_Application.ViewModels
{
    public class SearchDataViewModel : INotifyPropertyChanged
    {
        public ICommand DisplayManufacturerCommand { get; private set; }
        public ICommand DisplayCategoryTotalPriceCommand { get; private set; }

        public SearchDataViewModel()
        {
            Products = new ObservableCollection<Product>();
            Manufacturers = new ObservableCollection<Manufacturer>();
            Categories = new ObservableCollection<Category>();

            using (var dbContext = new SupermarketDbContext())
            {
                
                var manufacturersFromDatabase = dbContext.Manufacturers.ToList();
                foreach (var manufacturer in manufacturersFromDatabase)
                {
                    Manufacturers.Add(manufacturer);
                }

                
                var categoriesFromDatabase = dbContext.Categories.ToList();
                foreach (var category in categoriesFromDatabase)
                {
                    Categories.Add(category);
                }
            }

            DisplayManufacturerCommand = new RelayCommand(DisplayManufacturer);
            DisplayCategoryTotalPriceCommand = new RelayCommand(DisplayCategoryTotalPrice);

        }
        private void DisplayCategoryTotalPrice(object obj)
        {
           
            CategoryTotalPriceView categoryTotalPriceView = new CategoryTotalPriceView();
            categoryTotalPriceView.Show();
        }

        private void DisplayManufacturer(object obj)
        {
            ProductDisplayByManufacturer productDisplayByManufacturer = new ProductDisplayByManufacturer();
            productDisplayByManufacturer.Show();
        }

        private ObservableCollection<Manufacturer> _manufacturers;
        private ObservableCollection<Product> _products;
        private ObservableCollection<Category> _categories;
        private Manufacturer _selectedManufacturer;
        private Category _selectedCategory;

        public ObservableCollection<Manufacturer> Manufacturers
        {
            get { return _manufacturers; }
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public Manufacturer SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged(nameof(SelectedManufacturer));
                LoadProducts();
            }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            Products.Clear();

            if (SelectedManufacturer != null && SelectedCategory != null)
            {
                using (var dbContext = new SupermarketDbContext())
                {
                    var productsFromDatabase = dbContext.Products
                        .Where(p => p.ManufacturerID == SelectedManufacturer.ManufacturerID && p.CategoryID == SelectedCategory.CategoryID)
                        .ToList();
                    foreach (var product in productsFromDatabase)
                    {
                        Products.Add(product);
                    }
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

