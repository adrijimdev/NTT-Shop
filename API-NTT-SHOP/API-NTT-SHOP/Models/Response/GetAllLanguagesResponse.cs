using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class GetAllLanguagesResponse : BaseModelResponse
    {
        public List<Language> languages { get; set; }
    }
}
