using Supermarket_Application.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Supermarket_Application.DataAccess
{
    public class OfferRepository
    {
        private SupermarketDbContext _context;

        public OfferRepository(SupermarketDbContext context)
        {
            _context = context;
        }

        public void Add(Offer offer)
        {
            _context.Offers.Add(offer);
            _context.SaveChanges();
        }

        public Offer GetById(int id)
        {
            return _context.Offers.Find(id);
        }

        public IEnumerable<Offer> GetAll()
        {
            return _context.Offers.ToList();
        }

        public void Update(Offer offer)
        {
            _context.Entry(offer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var offer = _context.Offers.Find(id);
            if (offer != null)
            {
                offer.IsActive = false;
                _context.SaveChanges();
            }
        }
    }

}
