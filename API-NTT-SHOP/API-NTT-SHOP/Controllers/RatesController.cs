using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_NTT_SHOP.BC;
using API_NTT_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_NTT_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly RatesBC ratesBC = new RatesBC();

        [HttpGet]
        [Route("getAllRates")]
        public ActionResult<GetAllRatesResponse> GetAllRates()
        {
            GetAllRatesResponse response = ratesBC.GetAllRates();
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("getRate")]
        public ActionResult<GetRateResponse> GetRate(GetRateRequest request)
        {
            GetRateResponse response = ratesBC.GetRate(request);
            return HandleResponse(response);
        }


        [HttpPut]
        [Route("updateRate")]
        public ActionResult<GenericResponse> UpdateRate(UpdateRateRequest request)
        {
            GenericResponse response = ratesBC.UpdateRate(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertRate")]
        public ActionResult<GenericResponse> InsertRate(InsertRateRequest request)
        {
            GenericResponse response = ratesBC.InsertRate(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteRate/")]
        public ActionResult<GenericResponse> DeleteRate(DeleteRateRequest request)
        {
            GenericResponse response = ratesBC.DeleteRate(request);
            return HandleResponse(response);
        }

        public ActionResult HandleResponse(BaseModelResponse response)
        {
            if (response.httpCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            if (response.httpCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (response.httpCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            if (response.httpCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                return Forbid();
            }
        }


    }
}