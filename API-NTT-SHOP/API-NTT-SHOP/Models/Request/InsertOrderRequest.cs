using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class InsertOrderRequest
    {
        public int idUser { get; set; }
        public decimal totalPrice { get; set; }
    }
}