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
    public class OrdersController : ControllerBase
    {
        private readonly OrdersBC ordersBC = new OrdersBC();

        [HttpGet]
        [Route("getAllOrders")]
        public ActionResult<GetAllOrdersResponse> GetAllOrders()
        {
            GetAllOrdersResponse response = ordersBC.GetAllOrders();
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("getAllDetails")]
        public ActionResult<GetAllDetailsResponse> GetAllDetails()
        {
            GetAllDetailsResponse response = ordersBC.GetAllDetails();
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getOrdersFromUser")]
        public ActionResult<GetOrdersFromUserResponse> GetOrdersFromUser(GetOrdersFromUserRequest request)
        {
            GetOrdersFromUserResponse response = ordersBC.GetOrdersFromUser(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getDetailsFromOrder")]
        public ActionResult<GetDetailsFromOrderResponse> GetDetailsFromOrder(GetDetailsFromOrderRequest request)
        {
            GetDetailsFromOrderResponse response = ordersBC.GetDetailsFromOrder(request);
            return HandleResponse(response);
        }
        [HttpPut]
        [Route("updateOrder")]
        public ActionResult<GenericResponse> UpdateOrder(UpdateOrderRequest request)
        {
            GenericResponse response = ordersBC.UpdateOrder(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertOrder")]
        public ActionResult<GenericResponse> InsertOrder(InsertOrderRequest request)
        {
            GenericResponse response = ordersBC.InsertOrder(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertOrderDetail")]
        public ActionResult<GenericResponse> InsertOrderDetail(InsertOrderDetailRequest request)
        {
            GenericResponse response = ordersBC.InsertOrderDetail(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("cancelOrder/")]
        public ActionResult<GenericResponse> CancelOrder(CancelOrderRequest request)
        {
            GenericResponse response = ordersBC.CancelOrder(request);
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