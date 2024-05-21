using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Supermarket_Application.Models;
using Supermarket_Application.DataAccess;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Supermarket_Application.ViewModels
{
    public class CashierSearchViewModel : INotifyPropertyChanged
    {
        private string productName;
        private string barcode;
        private DateTime? expirationDate;
        private int? manufacturerId;
        private int? categoryId;
        public ObservableCollection<Product> SearchResults { get; set; } = new ObservableCollection<Product>();

        public string ProductName
        {
            get => productName;
            set { productName = value; OnPropertyChanged(); }
        }

        public string Barcode
        {
            get => barcode;
            set { barcode = value; OnPropertyChanged(); }
        }

        public DateTime? ExpirationDate
        {
            get => expirationDate;
            set { expirationDate = value; OnPropertyChanged(); }
        }

        public int? ManufacturerId
        {
            get => manufacturerId;
            set { manufacturerId = value; OnPropertyChanged(); }
        }

        public int? CategoryId
        {
            get => categoryId;
            set { categoryId = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; private set; }

        public CashierSearchViewModel()
        {
            SearchCommand = new RelayCommand(SearchProducts);
        }

        private void SearchProducts(object parameter)
        {
            using (var context = new SupermarketDbContext())
            {
                var query = "SELECT * FROM Products WHERE 1=1"; 
                var sqlParameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(ProductName))
                {
                    query += " AND ProductName LIKE @ProductName";
                    sqlParameters.Add(new SqlParameter("@ProductName", $"%{ProductName}%"));
                }

                if (!string.IsNullOrEmpty(Barcode))
                {
                    query += " AND Barcode = @Barcode";
                    sqlParameters.Add(new SqlParameter("@Barcode", Barcode));
                }

                if (ExpirationDate.HasValue)
                {
                    query = @"SELECT Products.* FROM Products
                      JOIN Stock ON Products.ProductID = Stock.ProductID
                      WHERE CAST(Stock.ExpirationDate AS DATE) = CAST(@ExpirationDate AS DATE)";
                    sqlParameters.Add(new SqlParameter("@ExpirationDate", ExpirationDate.Value.Date));
                }

                if (ManufacturerId.HasValue)
                {
                    query += " AND ManufacturerID = @ManufacturerId";
                    sqlParameters.Add(new SqlParameter("@ManufacturerId", ManufacturerId.Value));
                }

                if (CategoryId.HasValue)
                {
                    query += " AND CategoryID = @CategoryId";
                    sqlParameters.Add(new SqlParameter("@CategoryId", CategoryId.Value));
                }

                var products = context.Database.SqlQuery<Product>(query, sqlParameters.ToArray()).ToList();
                SearchResults.Clear();
                products.ForEach(p => SearchResults.Add(p));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
}
