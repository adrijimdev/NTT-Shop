﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetProductRatesResponse : BaseModelResponse
    {
        public List<GetProductRates> rates { get; set; }
    }
}
