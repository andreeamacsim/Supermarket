using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
