using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetProductRates
    {
        public int product { get; set; }
        public int idRate { get; set; }
        public string rate { get; set; }
        public decimal price { get; set; }
    }
}
