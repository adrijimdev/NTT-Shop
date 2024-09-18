using System;
using System.Collections.Generic;

namespace API_NTT_SHOP.NTTSHOP_DB
{
    public partial class Managementusers
    {
        public int PkManuser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname1 { get; set; }
        public string Surname2 { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
    }
}
