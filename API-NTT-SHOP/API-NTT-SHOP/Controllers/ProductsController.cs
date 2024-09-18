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
    public class ProductsController : ControllerBase
    {
        private readonly ProductsBC productsBC = new ProductsBC();

        [HttpGet]
        [Route("getAllProducts")]
        public ActionResult<GetAllProductsResponse> GetAllProducts()
        {
            GetAllProductsResponse response = productsBC.GetAllProducts();
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("getAllProductRates")]
        public ActionResult<GetAllProductRatesResponse> GetAllProductRates()
        {
            GetAllProductRatesResponse response = productsBC.GetAllProductRates();
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("productsForUser")]
        public ActionResult<ProductsForUserResponse> GetProductsForUser(ProductsForUserRequest request)
        {
            ProductsForUserResponse response = productsBC.GetProductsForUser(request);
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("productsForAdmin")]
        public ActionResult<ProductsForAdminResponse> GetProductsForAdmin()
        {
            ProductsForAdminResponse response = productsBC.GetProductsForAdmin();
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getProduct")]
        public ActionResult<GetProductResponse> GetProduct(GetProductRequest request)
        {
            GetProductResponse response = productsBC.GetProduct(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getProductStock")]
        public ActionResult<GetProductStockResponse> GetProductStock(GetProductRequest request)
        {
            GetProductStockResponse response = productsBC.GetProductStock(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getProductDescriptions")]
        public ActionResult<GetProductDescriptionsResponse> GetProductDescriptions(GetProductRequest request)
        {
            GetProductDescriptionsResponse response = productsBC.GetProductDescriptions(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getProductRates")]
        public ActionResult<GetProductRatesResponse> GetProductRates(GetProductRequest request)
        {
            GetProductRatesResponse response = productsBC.GetProductRates(request);
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("updateProduct")]
        public ActionResult<GenericResponse> UpdateProduct(UpdateProductRequest request)
        {
            GenericResponse response = productsBC.UpdateProduct(request);
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("updateDescription")]
        public ActionResult<GenericResponse> UpdateDescription(UpdateDescriptionRequest request)
        {
            GenericResponse response = productsBC.UpdateDescription(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertProduct")]
        public ActionResult<GenericResponse> InsertProduct(InsertProductRequest request)
        {
            GenericResponse response = productsBC.InsertProduct(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertDescription")]
        public ActionResult<GenericResponse> InsertDescription(InsertDescriptionRequest request)
        {
            GenericResponse response = productsBC.InsertDescription(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertRate")]
        public ActionResult<GenericResponse> InsertRate(InsertProductRateRequest request)
        {
            GenericResponse response = productsBC.InsertRate(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteProduct/")]
        public ActionResult<GenericResponse> DeleteProduct(DeleteProductRequest request)
        {
            GenericResponse response = productsBC.DeleteProduct(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteRate/")]
        public ActionResult<GenericResponse> DeleteRate(DeleteProductRateRequest request)
        {
            GenericResponse response = productsBC.DeleteRate(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteDescription/")]
        public ActionResult<GenericResponse> DeleteDescription(DeleteDescriptionRequest request)
        {
            GenericResponse response = productsBC.DeleteDescription(request);
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("setPrice")]
        public ActionResult<GenericResponse> SetPrice(SetPriceRequest request)
        {
            GenericResponse response = productsBC.SetPrice(request);
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