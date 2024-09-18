using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetAllProductRatesResponse : BaseModelResponse
    {
        public List<DeleteProductRate> rates { get; set; }
    }
}
