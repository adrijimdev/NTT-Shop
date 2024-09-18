using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTTShopAdmin.Entities
{
    public class Rate
    {
        public int idRate { get; set; }
        public string Description { get; set; }
        public bool _default { get; set; }
    }
}