using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int CategoryID { get; set; }
        public int ManufacturerID { get; set; }
        public bool IsActive { get; set; } 

      
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }

}
