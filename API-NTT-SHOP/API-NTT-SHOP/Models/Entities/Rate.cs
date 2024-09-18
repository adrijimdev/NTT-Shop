using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Rate
    {
        public int idRate { get; set; }
        public string description { get; set; } //not null unique
        public bool _default { get; set; } //not null
    }
}
