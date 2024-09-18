using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Filter
    {
        public int idProduct { get; set; }
        public int stock { get; set; }
        public bool enabled { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
