using API_NTT_SHOP.DAC;
using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.BC
{
    public class ProductsBC
    {
        private readonly ProductsDAC productsDAC = new ProductsDAC();

        public GetAllProductsResponse GetAllProducts()
        {
            GetAllProductsResponse getAllProductsResponse = new GetAllProductsResponse();

            getAllProductsResponse.products = productsDAC.GetAllProducts();

            if (getAllProductsResponse.products != null && getAllProductsResponse.products.Count > 0)
            {
                getAllProductsResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllProductsResponse.message = "No content";
                getAllProductsResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllProductsResponse;
        }

        public GetAllProductRatesResponse GetAllProductRates()
        {
            GetAllProductRatesResponse getAllProductRatesResponse = new GetAllProductRatesResponse();

            getAllProductRatesResponse.rates = productsDAC.GetAllProductRates();

            if (getAllProductRatesResponse.rates != null && getAllProductRatesResponse.rates.Count > 0)
            {
                getAllProductRatesResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllProductRatesResponse.message = "No content";
                getAllProductRatesResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllProductRatesResponse;
        }

        public GetProductDescriptionsResponse GetProductDescriptions(GetProductRequest request)
        {
            GetProductDescriptionsResponse response = new GetProductDescriptionsResponse();

            response.descriptions = productsDAC.GetProductDescriptions(request.idProduct);

            if (response.descriptions != null && response.descriptions.Count > 0)
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

        public GetProductRatesResponse GetProductRates(GetProductRequest request)
        {
            GetProductRatesResponse response = new GetProductRatesResponse();

            response.rates = productsDAC.GetProductRates(request.idProduct);

            if (response.rates != null && response.rates.Count > 0)
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

        public GetProductResponse GetProduct(GetProductRequest request)
        {
            GetProductResponse getProductResponse = new GetProductResponse();
            getProductResponse.product = productsDAC.GetProduct(request.idProduct);

            if (GetProductValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getProductResponse.product.idProduct);
                getProductResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getProductResponse.message = "No content";
                getProductResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getProductResponse;
        }

        public GetProductStockResponse GetProductStock(GetProductRequest request)
        {
            GetProductStockResponse getProductStockResponse = new GetProductStockResponse();
            getProductStockResponse.stock = productsDAC.GetProductStock(request.idProduct);

            if (GetProductValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getProductStockResponse.stock);
                getProductStockResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getProductStockResponse.message = "No content";
                getProductStockResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getProductStockResponse;
        }

        public ProductsForUserResponse GetProductsForUser(ProductsForUserRequest request)
        {
            ProductsForUserResponse response = new ProductsForUserResponse();
            response.products = productsDAC.GetProductsForUser(request.language, request.rate);

            if (response.products != null && response.products.Count > 0)
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

        public ProductsForAdminResponse GetProductsForAdmin()
        {
            ProductsForAdminResponse response = new ProductsForAdminResponse();
            response.products = productsDAC.GetProductsForAdmin();

            if (response.products != null && response.products.Count > 0)
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

        public GenericResponse UpdateProduct(UpdateProductRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateProductValidation(request))
            {
                bool correctOperation = productsDAC.UpdateProduct(request.product);

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

        public GenericResponse UpdateDescription(UpdateDescriptionRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateDescriptionValidation(request))
            {
                bool correctOperation = productsDAC.UpdateDescription(request);

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

        internal GenericResponse InsertProduct(InsertProductRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertProductValidation(request))
            {
                bool correctOperation = productsDAC.InsertProduct(request.product);

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

        internal GenericResponse InsertDescription(InsertDescriptionRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertDescriptionValidation(request))
            {
                bool correctOperation = productsDAC.InsertDescription(request);

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

        internal GenericResponse InsertRate(InsertProductRateRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertRateValidation(request))
            {
                bool correctOperation = productsDAC.InsertRate(request);

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

        internal GenericResponse DeleteProduct(DeleteProductRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteProductValidation(request))
            {
                bool correctOperation = productsDAC.DeleteProduct(request.idProduct);

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

        internal GenericResponse DeleteDescription(DeleteDescriptionRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteDescriptionValidation(request))
            {
                bool correctOperation = productsDAC.DeleteDescription(request.idProductDescription);

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

        internal GenericResponse DeleteRate(DeleteProductRateRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteRateValidation(request))
            {
                bool correctOperation = productsDAC.DeleteRate(request.idProduct, request.idRate);

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

        public GenericResponse SetPrice(SetPriceRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (SetPriceValidation(request))
            {
                bool correctOperation = productsDAC.SetPrice(request);

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

        private bool UpdateProductValidation(UpdateProductRequest request)
        {
            if (request != null
                && request.product != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.product.stock))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.product.enabled))
                && request.product.idProduct > 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UpdateDescriptionValidation(UpdateDescriptionRequest request)
        {
            if (request != null
                && request.description != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.description.language))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.description.name))
                && request.description.product > 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertDescriptionValidation(InsertDescriptionRequest request)
        {
            if (request != null
                && request.description != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.language))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.name))
                && request.product > 0
                )
            { 
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertRateValidation(InsertProductRateRequest request)
        {
            if (request != null
                && request.rate > 0
                && request.product > 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertProductValidation(InsertProductRequest request)
        {
            if (request != null
                && request.product != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.product.stock))
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.product.enabled))
                )

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DeleteProductValidation(DeleteProductRequest request)
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

        private bool DeleteDescriptionValidation(DeleteDescriptionRequest request)
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

        private bool DeleteRateValidation(DeleteProductRateRequest request)
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

        private bool GetProductValidation(GetProductRequest request)
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

        private bool SetPriceValidation(SetPriceRequest request)
        {
            if (request != null
                && request.idProduct != null
                && request.idRate != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(request.price))
                && request.idProduct > 0
                && request.idRate > 0
                )
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