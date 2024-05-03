using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int StockID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime ProvisioningDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; } = true;

        
       // public virtual Product Product { get; set; }
    }

}
