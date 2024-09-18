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
    public class StatusController : ControllerBase
    {
        private readonly ProductsBC productsBC = new ProductsBC();

        //[HttpGet]
        //[Route("getAllStatus")]
        //public ActionResult<GetAllProductsResponse> GetAllProducts(GetAllProductsRequest request)
        //{
        //    GetAllProductsResponse response = productsBC.GetAllProducts(request.language);
        //    return HandleResponse(response);
        //}

        [HttpGet]
        [Route("getStatus")]
        public ActionResult<GetProductResponse> GetProduct(GetProductRequest request)
        {
            GetProductResponse response = productsBC.GetProduct(request);
            return HandleResponse(response);
        }


        [HttpPut]
        [Route("updateStatus")]
        public ActionResult<GenericResponse> UpdateProduct(UpdateProductRequest request)
        {
            GenericResponse response = productsBC.UpdateProduct(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertStatus")]
        public ActionResult<GenericResponse> InsertProduct(InsertProductRequest request)
        {
            GenericResponse response = productsBC.InsertProduct(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteStatus/")]
        public ActionResult<GenericResponse> DeleteProduct(DeleteProductRequest request)
        {
            GenericResponse response = productsBC.DeleteProduct(request);
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