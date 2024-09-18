using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.DAC
{
    public class ProductsDAC
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("GETALLPRODUCTS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.idProduct = int.Parse(reader["PK_PRODUCT"].ToString());
                        product.stock = int.Parse(reader["STOCK"].ToString());
                        product.enabled = bool.Parse(reader["ENABLED"].ToString());
                        products.Add(product);
                    }
                }

                for (int i = 0; i < products.Count(); i++)
                {
                    SqlCommand getrates = new SqlCommand("GETRATESFROMPRODUCT @idProduct", conn);
                    getrates.Parameters.AddWithValue("@idProduct", products[i].idProduct);
                    using (SqlDataReader readrates = getrates.ExecuteReader())
                    {
                        while (readrates.Read())
                        {
                            ProductRate rate = new ProductRate();
                            rate.product = int.Parse(readrates["FK_PRODUCT"].ToString());
                            rate.rate = int.Parse(readrates["FK_RATE"].ToString());
                            rate.price = decimal.Parse(readrates["PRICE"].ToString());


                            products[i].rates.Add(rate);
                        }
                    }
                    SqlCommand getdescriptions = new SqlCommand("GETDESCRIPTIONSFROMPRODUCT @idProduct", conn);
                    getdescriptions.Parameters.AddWithValue("@idProduct", products[i].idProduct);
                    using (SqlDataReader readdescriptions = getdescriptions.ExecuteReader())
                    {
                        while (readdescriptions.Read())
                        {
                            ProductDescription description = new ProductDescription();
                            description.idProductDescription = int.Parse(readdescriptions["PK_PRODUCTDESCRIPTION"].ToString());
                            description.product = int.Parse(readdescriptions["FK_PRODUCT"].ToString());
                            description.language = readdescriptions["LANGUAGE"].ToString();
                            description.name = readdescriptions["TITLE"].ToString();
                            description.description = readdescriptions["DESCRIPTION"].ToString();

                            products[i].descriptions.Add(description);
                        }
                    }
                
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return products;
        }

        public List<DeleteProductRate> GetAllProductRates()
        {
            List<DeleteProductRate> rates = new List<DeleteProductRate>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("GETALLPRODUCTRATES", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DeleteProductRate rate = new DeleteProductRate();
                        rate.product = int.Parse(reader["FK_PRODUCT"].ToString());
                        rate.idRate = int.Parse(reader["FK_RATE"].ToString());
                        rate.rate = reader["DESCRIPTION"].ToString();
                        rate.price = Convert.ToDecimal(reader["PRICE"].ToString());

                        rates.Add(rate);
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rates;
        }

        private bool CheckLanguage(int idProduct, string language)
        {
            ProductDescription description = new ProductDescription();
            bool check = false;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand getdescriptions = new SqlCommand("GETDESCRIPTIONSFROMPRODUCT @idProduct", conn);
                getdescriptions.Parameters.AddWithValue("@idProduct", idProduct);
                using (SqlDataReader reader = getdescriptions.ExecuteReader())
                { 
                    while (reader.Read())
                    {
                        description.idProductDescription = int.Parse(reader["PK_PRODUCTDESCRIPTION"].ToString());
                        description.language = reader["LANGUAGE"].ToString();
                        if (reader["LANGUAGE"].ToString() == language)
                            check = true;
                        else
                            check = false;
                    }
                }
            }
            catch { throw; }
            finally
            {
                conn.Close();
            }
            return check;
        }
        public Product GetProduct(int idProduct)
        {
            Product product = new Product();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETPRODUCT @idProduct", conn);
                command.Parameters.AddWithValue("@idProduct", idProduct);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.idProduct = int.Parse(reader["PK_PRODUCT"].ToString());
                        product.stock = int.Parse(reader["STOCK"].ToString());
                        product.enabled = bool.Parse(reader["ENABLED"].ToString());
                    }
                }

                SqlCommand getrates = new SqlCommand("GETRATESFROMPRODUCT @idProduct", conn);
                getrates.Parameters.AddWithValue("@idProduct", idProduct);
                using (SqlDataReader reader = getrates.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductRate rate = new ProductRate();
                        rate.product = int.Parse(reader["FK_PRODUCT"].ToString());
                        rate.rate = int.Parse(reader["FK_RATE"].ToString());
                        rate.price = decimal.Parse(reader["PRICE"].ToString());


                        product.rates.Add(rate);
                    }
                }

                SqlCommand getdescriptions = new SqlCommand("GETDESCRIPTIONSFROMPRODUCT @idProduct", conn);
                getdescriptions.Parameters.AddWithValue("@idProduct", idProduct);                
                using (SqlDataReader reader = getdescriptions.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductDescription description = new ProductDescription();
                        description.idProductDescription = int.Parse(reader["PK_PRODUCTDESCRIPTION"].ToString());
                        description.product = int.Parse(reader["FK_PRODUCT"].ToString());
                        description.language = reader["LANGUAGE"].ToString();
                        description.name = reader["TITLE"].ToString();
                        description.description = reader["DESCRIPTION"].ToString();

                        product.descriptions.Add(description);
                    }
                }
                

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return product;
        }

        public int GetProductStock(int idProduct)
        {
            Product product = new Product();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETPRODUCT @idProduct", conn);
                command.Parameters.AddWithValue("@idProduct", idProduct);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.stock = int.Parse(reader["STOCK"].ToString());
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return product.stock;
        }

        public List<Filter> GetProductsForUser(string language, int rate)
        {
            List<Filter> products = new List<Filter>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("PRODUCTSFORUSER @language, @rate", conn);
                command.Parameters.AddWithValue("@language", language);
                command.Parameters.AddWithValue("@rate", rate);
                using (SqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    Filter product = new Filter();
                    product.stock = int.Parse(reader["STOCK"].ToString());
                    product.enabled = bool.Parse(reader["ENABLED"].ToString());
                    if (product.stock > 0 && product.enabled == true)
                    {
                        product.idProduct = int.Parse(reader["PK_PRODUCT"].ToString());
                        product.price = decimal.Parse(reader["PRICE"].ToString());
                        product.name = reader["TITLE"].ToString();
                        product.description = reader["DESCRIPTION"].ToString();
                        products.Add(product);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return products;
        }

        public List<ProductDescription> GetProductDescriptions(int idProduct)
        {
            List<ProductDescription> descriptions = new List<ProductDescription>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETPRODUCTDESCRIPTIONS @idproduct", conn);
                command.Parameters.AddWithValue("@idproduct", idProduct);
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        ProductDescription description = new ProductDescription();
                        description.idProductDescription = int.Parse(reader["PK_PRODUCTDESCRIPTION"].ToString());
                        description.product = int.Parse(reader["FK_PRODUCT"].ToString());
                        description.name = reader["TITLE"].ToString();
                        description.description = reader["DESCRIPTION"].ToString();
                        description.language = reader["LANGUAGE"].ToString();
                        descriptions.Add(description);
                    }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return descriptions;
        }

        public List<GetProductRates> GetProductRates(int idProduct)
        {
            List<GetProductRates> rates = new List<GetProductRates>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETPRODUCTRATES @idproduct", conn);
                command.Parameters.AddWithValue("@idproduct", idProduct);
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        GetProductRates rate = new GetProductRates();
                        rate.product = int.Parse(reader["FK_PRODUCT"].ToString());
                        rate.idRate = int.Parse(reader["FK_RATE"].ToString());
                        rate.rate = reader["DESCRIPTION"].ToString();
                        rate.price = Convert.ToDecimal(reader["PRICE"].ToString());
                        rates.Add(rate);
                    }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rates;
        }

        public List<AdminProduct> GetProductsForAdmin()
        {
            List<AdminProduct> products = new List<AdminProduct>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETALLPRODUCTS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        AdminProduct product = new AdminProduct();
                        product.idProduct = int.Parse(reader["PK_PRODUCT"].ToString());
                        product.stock = int.Parse(reader["STOCK"].ToString());
                        product.enabled = bool.Parse(reader["ENABLED"].ToString());
                        products.Add(product);
                    }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return products;
        }


        public bool UpdateProduct(Product product)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATEPRODUCT @idProduct, @stock, @enabled", conn);
                command.Parameters.AddWithValue("@stock", product.stock);
                command.Parameters.AddWithValue("@enabled", Convert.ToBoolean(product.enabled));
                command.Parameters.AddWithValue("@idProduct", product.idProduct);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateProductDescription(UpdateProduct product)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATEDESCRIPTION @idProduct, @language, @name, @description", conn);
                command.Parameters.AddWithValue("@idProduct", product.idProduct);
                command.Parameters.AddWithValue("@language", product.language);
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@description", product.description);

                if (CheckIso(product.language) == true)
                {
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateDescription(UpdateDescriptionRequest request)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());


            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("UPDATEDESCRIPTION @idProduct, @language, @name, @description", conn);
                    command.Parameters.AddWithValue("@idProduct", request.description.product);
                    command.Parameters.AddWithValue("@language", request.description.language);
                    command.Parameters.AddWithValue("@name", request.description.name);
                    command.Parameters.AddWithValue("@description", request.description.description);

                    if (CheckIso(request.description.language) == true)
                    {
                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {

                        return false;
                    }

                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool CheckIso(string iso)
        {
            bool check = false;

            List<Language> languages = new List<Language>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM LANGUAGES", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        Language language = new Language();
                        language.idLanguage = int.Parse(reader["PK_LANGUAGE"].ToString());
                        language.iso = reader["ISO"].ToString();
                        languages.Add(language);
                    }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            for (int i = 0; i < languages.Count; i++)
            {
                if (languages[i].iso == iso)
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        public bool InsertProduct(InsertProduct product)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("INSERTPRODUCT @stock, @enabled, @language, @name, @description", conn);
                command.Parameters.AddWithValue("@stock", product.stock);
                command.Parameters.AddWithValue("@enabled", Convert.ToBoolean(product.enabled));
                command.Parameters.AddWithValue("@language", product.language);
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@description", product.description);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool InsertDescription(InsertDescriptionRequest description)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("CREATEDESCRIPTION @idproduct, @language, @name, @description", conn);
                command.Parameters.AddWithValue("@idproduct", description.product);
                command.Parameters.AddWithValue("@language", description.language);
                command.Parameters.AddWithValue("@name", description.name);
                command.Parameters.AddWithValue("@description", description.description);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool InsertRate(InsertProductRateRequest rate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("INSERTPRODUCTRATE @idproduct, @idrate, @price", conn);
                command.Parameters.AddWithValue("@idproduct", rate.product);
                command.Parameters.AddWithValue("@idrate", rate.rate);
                command.Parameters.AddWithValue("@price", rate.price);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteProduct(int idProduct)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETEPRODUCT @idProduct", conn);
                command.Parameters.AddWithValue("@idProduct", idProduct);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteDescription(int idProductDescription)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETEDESCRIPTION @idProductDescription", conn);
                command.Parameters.AddWithValue("@idProductDescription", idProductDescription);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteRate(int idProduct, int idRate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETEPRODUCTRATE @idProduct, @idRate", conn);
                command.Parameters.AddWithValue("@idProduct", idProduct);
                command.Parameters.AddWithValue("@idRate", idRate);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool SetPrice(SetPriceRequest request)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                if (CheckPrice(request.idProduct, request.idRate) == true)
                {
                    SqlCommand command = new SqlCommand("SETPRICE @idProduct, @idRate, @price", conn);
                    command.Parameters.AddWithValue("@idProduct", request.idProduct);
                    command.Parameters.AddWithValue("@idRate", request.idRate);
                    command.Parameters.AddWithValue("@price", request.price);

                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else
                {
                    SqlCommand checkproduct = new SqlCommand("SELECT * FROM PRODUCTS WHERE PK_PRODUCT = @idProduct", conn);
                    checkproduct.Parameters.AddWithValue("@idProduct", request.idProduct);
                    using (SqlDataReader readproduct = checkproduct.ExecuteReader())
                    {
                        if (readproduct.Read())
                        {
                            SqlCommand checkrate = new SqlCommand("SELECT * FROM RATES WHERE PK_RATE = @idRate", conn);
                            checkrate.Parameters.AddWithValue("@idRate", request.idRate);
                            using (SqlDataReader readrate = checkrate.ExecuteReader())
                            {
                                if (readrate.Read())
                                {
                                    SqlCommand insert = new SqlCommand("INSERT INTO PRODUCTRATES VALUES(@idProduct, @idRate, @price)", conn);
                                    insert.Parameters.AddWithValue("@idProduct", request.idProduct);
                                    insert.Parameters.AddWithValue("@idRate", request.idRate);
                                    insert.Parameters.AddWithValue("@price", request.price);

                                    int result = insert.ExecuteNonQuery();

                                    if (result > 0)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                    return false;
                            }
                        }
                        else
                            return false;
                    }
                    
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        private bool CheckPrice(int idProduct, int idRate)
        {
            bool check = false;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand checkprice = new SqlCommand("SELECT * FROM PRODUCTRATES WHERE FK_PRODUCT = @idProduct AND FK_RATE = @idRate", conn);
                checkprice.Parameters.AddWithValue("@idProduct", idProduct);
                checkprice.Parameters.AddWithValue("@idRate", idRate);
                using (SqlDataReader reader = checkprice.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        check = true;
                    }

                    else
                        check = false;
                }
            }
            catch { throw; }
            finally
            {
                conn.Close();
            }
            return check;
        }

    }
}
