using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Supermarket_Application.ViewModels
{
    public class AddReceiptViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> availableProducts = new ObservableCollection<Product>();
        private Product selectedProduct;
        private ObservableCollection<ReceiptDetail> receiptDetails = new ObservableCollection<ReceiptDetail>();
        private decimal totalAmount;

        public ObservableCollection<Product> AvailableProducts
        {
            get => availableProducts;
            set
            {
                availableProducts = value;
                OnPropertyChanged();
            }
        }
        private int userId;
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged();
            }
        }


        private int quantityInput;
        public int QuantityInput
        {
            get { return quantityInput; }
            set
            {
                quantityInput = value;
                OnPropertyChanged();
            }
        }

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ReceiptDetail> ReceiptDetails
        {
            get => receiptDetails;
            set
            {
                receiptDetails = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalAmount
        {
            get => totalAmount;
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddProductCommand { get; private set; }
        public ICommand SaveReceiptCommand { get; private set; }

        public AddReceiptViewModel()
        {
            LoadProducts();
            InitializeCommands();
            ReceiptDetails.CollectionChanged += (s, e) => CalculateTotal();
        }

        private void InitializeCommands()
        {
            AddProductCommand = new RelayCommand(AddProduct);
            SaveReceiptCommand = new RelayCommand(SaveReceipt);
        }

        private void LoadProducts()
        {
            using (var context = new SupermarketDbContext())
            {
                AvailableProducts = new ObservableCollection<Product>(context.Products.Where(p => p.IsActive).ToList());
            }
        }

        private void AddProduct(object parameter)
        {
            if (SelectedProduct != null && QuantityInput > 0)
            {
                using (var context = new SupermarketDbContext())
                {
                    var stock = context.Stock.FirstOrDefault(s => s.ProductID == SelectedProduct.ProductID && s.IsActive);

                    if (stock != null && stock.Quantity >= QuantityInput)
                    {
                        var detail = new ReceiptDetail
                        {
                            ProductID = SelectedProduct.ProductID,
                            Quantity = QuantityInput,
                            Subtotal = stock.SellingPrice * QuantityInput
                        };
                        ReceiptDetails.Add(detail);
                    }
                  
                }
            }
        }




        private void SaveReceipt(object parameter)
        {
            using (var context = new SupermarketDbContext())
            {
               
                Receipt newReceipt = new Receipt
                {
                    DateIssued = DateTime.Now,
                    TotalAmount = TotalAmount,
                    IsActive = true,
                    CashierID = UserId
                };
                context.Receipts.Add(newReceipt);
                context.SaveChanges(); 

               
                foreach (var detail in ReceiptDetails)
                {
                    detail.ReceiptID = newReceipt.ReceiptID; 
                    context.ReceiptDetails.Add(detail);
                }

                
                context.SaveChanges();
            }
        }




        private void CalculateTotal()
        {
            TotalAmount = ReceiptDetails.Sum(detail => detail.Subtotal);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
