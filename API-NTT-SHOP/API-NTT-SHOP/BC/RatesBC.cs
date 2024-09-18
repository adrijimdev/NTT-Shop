using API_NTT_SHOP.DAC;
using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.BC
{
    public class RatesBC
    {
        private readonly RatesDAC ratesDAC = new RatesDAC();

        public GetAllRatesResponse GetAllRates()
        {
            GetAllRatesResponse getAllRatesResponse = new GetAllRatesResponse();

            getAllRatesResponse.rates = ratesDAC.GetAllRates();

            if (getAllRatesResponse.rates != null && getAllRatesResponse.rates.Count > 0)
            {
                getAllRatesResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllRatesResponse.message = "No content";
                getAllRatesResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllRatesResponse;
        }

        public GetRateResponse GetRate(GetRateRequest request)
        {
            GetRateResponse getRateResponse = new GetRateResponse();
            getRateResponse.rate = ratesDAC.GetRate(request.idRate);

            if (GetRateValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getRateResponse.rate.idRate);
                getRateResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getRateResponse.message = "No content";
                getRateResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getRateResponse;
        }

        public GenericResponse UpdateRate(UpdateRateRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateRateValidation(request))
            {
                bool correctOperation = ratesDAC.UpdateRate(request.rate);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "Ya existe esta tarifa";
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        internal GenericResponse InsertRate(InsertRateRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertRateValidation(request))
            {
                bool correctOperation = ratesDAC.InsertRate(request.rate);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        internal GenericResponse DeleteRate(DeleteRateRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteRateValidation(request))
            {
                bool correctOperation = ratesDAC.DeleteRate(request.idRate);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }


        ////VALIDATIONS
        
        private bool UpdateRateValidation(UpdateRateRequest request)
        {
            if (request != null
                && request.rate != null
                && !string.IsNullOrWhiteSpace(request.rate.description)
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.rate._default))
                && request.rate.idRate > 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertRateValidation(InsertRateRequest request)
        {
            if (request != null
                && request.rate != null
                && !string.IsNullOrWhiteSpace(request.rate.description)
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.rate._default))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DeleteRateValidation(DeleteRateRequest request)
        {
            if (request != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetRateValidation(GetRateRequest request)
        {
            if (request != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}