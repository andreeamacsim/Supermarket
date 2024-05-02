using Supermarket_Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.DataAccess
{
    public class StockRepository
    {
        private SupermarketDbContext _context;

        public StockRepository(SupermarketDbContext context)
        {
            _context = context;
        }

        public void Add(Stock stock)
        {
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
            _context.Entry(stock).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                _context.SaveChanges();
            }
        }
    }

}
