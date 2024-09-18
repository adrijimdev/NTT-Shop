using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class UpdateOrderRequest
    {
        public int idOrder { get; set; }
        public int idStatus { get; set; }
    }
}
