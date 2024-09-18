using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetLoginResponse : BaseModelResponse
    {
        //public Users user { get; set; }
        public bool response { get; set; }
        public int idUser { get; set; }
        public string language { get; set; }
        public int rate { get; set; }
    }
}
