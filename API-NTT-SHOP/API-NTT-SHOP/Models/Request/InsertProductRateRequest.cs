using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class InsertProductRateRequest
    {
        public int product { get; set; }
        public int rate { get; set; }
        public decimal price { get; set; }
    }
}