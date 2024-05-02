using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public DateTime DateIssued { get; set; }
        public int CashierID { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; } = true;

       
        public virtual User Cashier { get; set; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }

}
