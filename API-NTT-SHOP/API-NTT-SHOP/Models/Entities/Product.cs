using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Product
    {
        public int idProduct { get; set; }
        public int stock { get; set; }
        public bool enabled { get; set; }
        public List<ProductRate> rates = new List<ProductRate>();
        public List<ProductDescription> descriptions = new List<ProductDescription>();
    }
}
