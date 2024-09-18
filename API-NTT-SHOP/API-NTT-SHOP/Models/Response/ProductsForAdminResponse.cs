using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class ProductsForAdminResponse : BaseModelResponse
    {
        public List<AdminProduct> products { get; set; }
    }
}
