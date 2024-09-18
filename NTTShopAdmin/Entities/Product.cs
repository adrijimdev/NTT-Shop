using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTTShopAdmin.Entities
{
    public class Product
    {
        public int idProduct { get; set; }
        public int stock { get; set; }
        public bool enabled { get; set; }
    }
}