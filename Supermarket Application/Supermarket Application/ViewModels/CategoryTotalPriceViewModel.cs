using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Supermarket_Application.ViewModels
{
    public class CategoryTotalPriceViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CategoryTotalPriceItem> CategoryTotalPrices { get; set; }

        public CategoryTotalPriceViewModel()
        {
            LoadCategoryTotalPrices();
        }

        private void LoadCategoryTotalPrices()
        {
            CategoryTotalPrices = new ObservableCollection<CategoryTotalPriceItem>();

            using (var dbContext = new SupermarketDbContext())
            {
                var categoryTotalPrices = dbContext.Products
                    .Join(
                        dbContext.Stock,
                        p => p.ProductID,
                        s => s.ProductID,
                        (p, s) => new { Product = p, Stock = s }
                    )
                    .Join(
                        dbContext.Categories,
                        ps => ps.Product.CategoryID,
                        c => c.CategoryID,
                        (ps, c) => new
                        {
                            CategoryName = c.CategoryName,
                            SellingPrice = ps.Stock.SellingPrice,
                            Product = ps.Product 
                        }
                    )
                    .Where(ps => ps.SellingPrice != null && ps.Product.IsActive)
                    .GroupBy(ps => ps.CategoryName)
                    .Select(g => new CategoryTotalPriceItem
                    {
                        CategoryName = g.Key,
                        TotalPrice = g.Sum(ps => ps.SellingPrice)
                    })
                    .ToList();

                foreach (var categoryTotalPrice in categoryTotalPrices)
                {
                    CategoryTotalPrices.Add(categoryTotalPrice);
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CategoryTotalPriceItem
    {
        public string CategoryName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
