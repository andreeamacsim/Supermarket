using Supermarket_Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Configuration;


namespace Supermarket_Application.DataAccess
{
    public class StockRepository
    {
        private SupermarketDbContext _context;

        public StockRepository(SupermarketDbContext context)
        {
            _context = context;
        }
        private decimal GetMarkupPercentage()
        {
            return Convert.ToDecimal(ConfigurationManager.AppSettings["MarkupPercentage"]);
        }
        public void Add(Stock stock)
        {
            decimal markupPercentage = GetMarkupPercentage();
            stock.SellingPrice = stock.PurchasePrice + (stock.PurchasePrice * markupPercentage / 100);
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }

        public Stock GetById(int id)
        {
            return _context.Stocks.Find(id);
        }

        public IEnumerable<Stock> GetAll()
        {
            return _context.Stocks.ToList();
        }

        public void Update(Stock stock)
        {
            var existingStock = _context.Stocks.AsNoTracking().FirstOrDefault(s => s.StockID == stock.StockID);
            if (existingStock == null)
            {
                throw new ArgumentException("Stock isn't  found");
            }

            
            if (existingStock.PurchasePrice != stock.PurchasePrice)
            {
                throw new InvalidOperationException("Modification of purchase price is not allowed after entry.");
            }

            
            if (stock.SellingPrice < stock.PurchasePrice)
            {
                throw new InvalidOperationException("Selling price cannot be less than the purchase price.");
            }

            _context.Entry(stock).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock != null)
            {
                stock.IsActive = false;
                _context.SaveChanges();
            }
        }
    }

}
