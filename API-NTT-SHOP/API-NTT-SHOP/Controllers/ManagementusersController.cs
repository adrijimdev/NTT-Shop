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
    public class ManagementusersController : ControllerBase
    {
        private readonly ManagementusersBC manusersBC = new ManagementusersBC();

        

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