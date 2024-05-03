using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Supermarket_Application.ViewModels
{
    public class AddStockViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }
        public ICommand UpdatePriceCommand { get; private set; }
        public ICommand AddStockCommand { get; private set; }
        public decimal PurchasePrice { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime ProvisioningDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        private decimal _newSellingPrice;
        public decimal NewSellingPrice
        {
            get { return _newSellingPrice; }
            set
            {
                if (_newSellingPrice != value)
                {
                    _newSellingPrice = value;
                    OnPropertyChanged(nameof(NewSellingPrice));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddStockViewModel()
        {
            AddStockCommand = new RelayCommand(AddStock);
            UpdatePriceCommand=new RelayCommand(UpdatePrice);

            LoadProducts();
            ProvisioningDate = DateTime.Now; 
            ExpirationDate = DateTime.Now;
        }

        private void LoadProducts()
        {
            using (var db = new SupermarketDbContext())
            {
                Products = new ObservableCollection<Product>(db.Products.ToList());
            }
        }

        private void AddStock(object parameter)
        {
            using (var db = new SupermarketDbContext())
            {
                var markupPercentage = GetMarkupPercentageFromConfig(); 
                var sellingPrice = PurchasePrice + (PurchasePrice * markupPercentage / 100);

                var stock = new Stock
                {
                    ProductID = ProductID,
                    PurchasePrice = PurchasePrice,
                    SellingPrice = sellingPrice,
                    Quantity = Quantity,
                    UnitOfMeasure = UnitOfMeasure,
                    ProvisioningDate = ProvisioningDate, 
                    ExpirationDate = ExpirationDate
                };

                db.Stock.Add(stock);
                db.SaveChanges();

                System.Windows.MessageBox.Show("Stock added successfully.", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
        }

        private decimal GetMarkupPercentageFromConfig()
        {
            
            var markupPercentage = ConfigurationManager.AppSettings["MarkupPercentage"];
            if (decimal.TryParse(markupPercentage, out decimal result))
            {
                return result;
            }
            else
            {
                
                return 20; 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdatePrice(object parameter)
        {

            using (var db = new SupermarketDbContext())
            {
                var stock = db.Stock.FirstOrDefault(s => s.ProductID == ProductID);
                if (stock != null)
                {
                    if (NewSellingPrice < stock.PurchasePrice)
                    {
                        MessageBox.Show("New selling price cannot be lower than the purchase price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    stock.SellingPrice = NewSellingPrice;
                    db.SaveChanges();
                    MessageBox.Show("Selling price updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update selling price. Stock not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



    }
}
