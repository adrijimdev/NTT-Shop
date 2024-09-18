using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class UpdateProduct
    {
        public int idProduct { get; set; }
        public int stock { get; set; }
        public string enabled { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
    }
}
