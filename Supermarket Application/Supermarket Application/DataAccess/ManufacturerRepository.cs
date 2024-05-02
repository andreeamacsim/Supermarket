using Supermarket_Application.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Supermarket_Application.DataAccess
{
    public class ManufacturerRepository
    {
        private SupermarketDbContext _context;

        public ManufacturerRepository(SupermarketDbContext context)
        {
            _context = context;
        }

        public void Add(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();
        }

        public Manufacturer GetById(int id)
        {
            return _context.Manufacturers.Find(id);
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return _context.Manufacturers.ToList();
        }

        public void Update(Manufacturer manufacturer)
        {
            _context.Entry(manufacturer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var manufacturer = _context.Manufacturers.Find(id);
            if (manufacturer != null)
            {
                manufacturer.IsActive = false;
                _context.SaveChanges();
            }
        }
    }

}
