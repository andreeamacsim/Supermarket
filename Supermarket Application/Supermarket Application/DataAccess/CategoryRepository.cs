using Supermarket_Application.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Supermarket_Application.DataAccess
{
    public class CategoryRepository
    {
        private SupermarketDbContext _context;

        public CategoryRepository(SupermarketDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }

}
