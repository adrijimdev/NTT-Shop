using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class OrderDetail
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int units { get; set; }
        public decimal subtotal { get; set; }
    }
}
