using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class SetPriceRequest
    {
        public int idProduct { get; set; }
        public int idRate { get; set; }
        public decimal price { get; set; }
    }
}
