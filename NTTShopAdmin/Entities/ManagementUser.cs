using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTTShopAdmin.Entities
{
    public class ManagementUser
    {
        public int idManUser { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname1 { get; set; }
        public string surname2 { get; set; }
        public string email { get; set; }
        public string language { get; set; }
    }
}