using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetRateResponse : BaseModelResponse
    {
        public Rate rate { get; set; }
    }
}
