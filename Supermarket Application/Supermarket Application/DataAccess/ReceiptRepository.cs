//using Supermarket_Application.Models;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;


//namespace Supermarket_Application.DataAccess
//{
//    public class ReceiptRepository
//    {
//        private SupermarketDbContext _context;

//        public ReceiptRepository(SupermarketDbContext context)
//        {
//            _context = context;
//        }

//        public void Add(Receipt receipt)
//        {
//            _context.Receipts.Add(receipt);
//            _context.SaveChanges();
//        }

//        public Receipt GetById(int id)
//        {
//            return _context.Receipts.Find(id);
//        }

//        public IEnumerable<Receipt> GetAll()
//        {
//            return _context.Receipts.ToList();
//        }

//        public void Update(Receipt receipt)
//        {
//            _context.Entry(receipt).State = EntityState.Modified;
//            _context.SaveChanges();
//        }

//        public void Delete(int id)
//        {
//            var receipt = _context.Receipts.Find(id);
//            if (receipt != null)
//            {
//                receipt.IsActive = false;
//                _context.SaveChanges();
//            }
//        }
//    }

//}
