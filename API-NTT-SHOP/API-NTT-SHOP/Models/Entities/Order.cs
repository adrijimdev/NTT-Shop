using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Order
    {
        public int idOrder { get; set; }
        public int idUser { get; set; }
        public DateTime orderDate { get; set; }
        public int idStatus { get; set; }
        public string orderStatus { get; set; }
        public decimal totalPrice { get; set; }
        public List<OrderDetail> details { get; set; }
    }
}
