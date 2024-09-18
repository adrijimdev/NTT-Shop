using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Detail
    {
        public int idOrder { get; set; }
        public int idProduct { get; set; }
        public decimal price { get; set; }
        public int units { get; set; }
    }
}
