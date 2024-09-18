using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTTShopAdmin.Entities
{
    public class ProductRate
    {
        public int product { get; set; }
        public int idRate { get; set; }
        public string rate { get; set; }
        public decimal price { get; set; }
    }
}