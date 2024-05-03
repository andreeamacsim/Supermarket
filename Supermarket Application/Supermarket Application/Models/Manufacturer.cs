﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_Application.Models
{
    [Table("Manufacturers")]
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
