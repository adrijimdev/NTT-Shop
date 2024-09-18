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
    public class LoginController : ControllerBase
    {
        private readonly LoginBC loginBC = new LoginBC();

        [HttpPost]
        [Route("getLogin")]
        public ActionResult<GetLoginResponse> GetLogin(GetLoginRequest request)
        {
            GetLoginResponse response = loginBC.GetLogin(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getAdminLogin")]
        public ActionResult<GetAdminLoginResponse> GetAdminLogin(GetAdminLoginRequest request)
        {
            GetAdminLoginResponse response = loginBC.GetAdminLogin(request);
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