using Supermarket_Application.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Supermarket_Application.DataAccess
{
    public class ProductRepository
    {
        private SupermarketDbContext _context;

        public ProductRepository(SupermarketDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.IsActive = false;
                _context.SaveChanges();
            }
        }
    }

}
