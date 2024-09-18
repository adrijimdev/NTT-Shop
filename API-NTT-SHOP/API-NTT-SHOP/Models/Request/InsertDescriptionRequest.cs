using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class InsertDescriptionRequest
    {
        public int product { get; set; }
        public string language { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}