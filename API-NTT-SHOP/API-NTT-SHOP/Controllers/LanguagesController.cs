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
    public class LanguagesController : ControllerBase
    {
        private readonly LanguagesBC languagesBC = new LanguagesBC();

        [HttpGet]
        [Route("getAllLanguages")]
        public ActionResult<GetAllLanguagesResponse> GetAllLanguages()
        {
            GetAllLanguagesResponse response = languagesBC.GetAllLanguages();
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("getLanguage")]
        public ActionResult<GetLanguageResponse> GetLanguage(GetLanguageRequest request)
        {
            GetLanguageResponse response = languagesBC.GetLanguage(request);
            return HandleResponse(response);
        }


        [HttpPut]
        [Route("updateLanguage")]
        public ActionResult<GenericResponse> UpdateLanguage(UpdateLanguageRequest request)
        {
            GenericResponse response = languagesBC.UpdateLanguage(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertLanguage")]
        public ActionResult<GenericResponse> InsertLanguage(InsertLanguageRequest request)
        {
            GenericResponse response = languagesBC.InsertLanguage(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteLanguage/")]
        public ActionResult<GenericResponse> DeleteLanguage(DeleteLanguageRequest request)
        {
            GenericResponse response = languagesBC.DeleteLanguage(request);
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