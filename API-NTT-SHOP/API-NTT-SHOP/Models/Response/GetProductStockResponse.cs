﻿using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetProductStockResponse : BaseModelResponse
    {
        public int stock { get; set; }
    }
}
