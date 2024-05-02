using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    public class Offer
    {
        public int OfferID { get; set; }
        public int ProductID { get; set; }
        public string OfferReason { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsActive { get; set; } = true;

       
        public virtual Product Product { get; set; }
    }

}
