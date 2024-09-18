using API_NTT_SHOP.DAC;
using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.BC
{
    public class OrdersBC
    {
        private readonly OrdersDAC ordersDAC = new OrdersDAC();

        public GetAllOrdersResponse GetAllOrders()
        {
            GetAllOrdersResponse getAllOrdersResponse = new GetAllOrdersResponse();

            getAllOrdersResponse.orders = ordersDAC.GetAllOrders();

            if (getAllOrdersResponse.orders != null && getAllOrdersResponse.orders.Count > 0)
            {
                getAllOrdersResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllOrdersResponse.message = "No content";
                getAllOrdersResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllOrdersResponse;
        }

        public GetAllDetailsResponse GetAllDetails()
        {
            GetAllDetailsResponse getAllDetailsResponse = new GetAllDetailsResponse();

            getAllDetailsResponse.details = ordersDAC.GetAllDetails();

            if (getAllDetailsResponse.details != null && getAllDetailsResponse.details.Count > 0)
            {
                getAllDetailsResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllDetailsResponse.message = "No content";
                getAllDetailsResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllDetailsResponse;
        }

        public GetOrdersFromUserResponse GetOrdersFromUser(GetOrdersFromUserRequest request)
        {
            GetOrdersFromUserResponse response = new GetOrdersFromUserResponse();
            response.orders = ordersDAC.GetOrdersFromUser(request.idUser);

            if (response != null && response.orders.Count > 0)
            {
                response.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.message = "No content";
                response.httpCode = System.Net.HttpStatusCode.NoContent;
            }
            return response;
        }

        public GetDetailsFromOrderResponse GetDetailsFromOrder(GetDetailsFromOrderRequest request)
        {
            GetDetailsFromOrderResponse response = new GetDetailsFromOrderResponse();
            response.details = ordersDAC.GetDetailsFromOrder(request.idOrder, request.language);

            if (response != null && response.details.Count > 0)
            {
                response.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.message = "No content";
                response.httpCode = System.Net.HttpStatusCode.NoContent;
            }
            return response;
        }

        public GenericResponse UpdateOrder(UpdateOrderRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateOrderValidation(request))
            {
                bool correctOperation = ordersDAC.UpdateOrder(request.idOrder, request.idStatus);

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

        internal GenericResponse InsertOrder(InsertOrderRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertOrderValidation(request))
            {
                bool correctOperation = ordersDAC.InsertOrder(request);

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

        internal GenericResponse InsertOrderDetail(InsertOrderDetailRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertOrderDetailValidation(request))
            {
                bool correctOperation = ordersDAC.InsertOrderDetail(request);

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

        internal GenericResponse CancelOrder(CancelOrderRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (CancelOrderValidation(request))
            {
                bool correctOperation = ordersDAC.CancelOrder(request.idOrder);

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


        //VALIDATIONS

        private bool UpdateOrderValidation(UpdateOrderRequest request)
        {
            if (request != null
                && request.idOrder > 0
                && request.idStatus > 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertOrderValidation(InsertOrderRequest request)
        {
            if (request != null
                && request != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.idUser))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.totalPrice))
                )

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertOrderDetailValidation(InsertOrderDetailRequest request)
        {
            if (request != null
                && request != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.idOrder))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.idProduct))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.price))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.units))
                )

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CancelOrderValidation(CancelOrderRequest request)
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

        //private bool GetProductValidation(GetProductRequest request)
        //{
        //    if (request != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private bool SetPriceValidation(SetPriceRequest request)
        //{
        //    if (request != null
        //        && request.idProduct != null
        //        && request.idRate != null
        //        && !string.IsNullOrWhiteSpace(Convert.ToString(request.price))
        //        && request.idProduct > 0
        //        && request.idRate > 0
        //        )
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}