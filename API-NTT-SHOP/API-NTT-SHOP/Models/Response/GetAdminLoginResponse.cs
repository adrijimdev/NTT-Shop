using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetAdminLoginResponse : BaseModelResponse
    {
        //public Users user { get; set; }
        public bool response { get; set; }
        public int idManUser { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname1 { get; set; }
        public string email { get; set; }
        public string language { get; set; }
    }
}
