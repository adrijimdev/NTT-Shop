using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class BaseModelResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public HttpStatusCode httpCode { get; set; }

        public BaseModelResponse(BaseModelResponse baseModelResponse)
        {
            this.message = baseModelResponse.message;
            this.httpCode = baseModelResponse.httpCode;
        }

        public BaseModelResponse()
        {
        }


    }
}
