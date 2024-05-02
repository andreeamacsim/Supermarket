﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    public class ReceiptDetail
    {
        public int ReceiptID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public bool IsActive { get; set; } = true;

        
        public virtual Receipt Receipt { get; set; }
        public virtual Product Product { get; set; }
    }

}
