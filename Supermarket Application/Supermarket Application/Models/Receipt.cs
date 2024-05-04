using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    [Table("Receipts")]
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public DateTime DateIssued { get; set; }
        public int CashierID { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; } = true;

        //public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }

}
