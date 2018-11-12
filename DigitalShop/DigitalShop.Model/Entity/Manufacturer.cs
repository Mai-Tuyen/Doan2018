﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalShop.Entity
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
