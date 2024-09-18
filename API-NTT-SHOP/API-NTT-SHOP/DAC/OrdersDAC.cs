using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.DAC
{
    public class OrdersDAC
    {
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("GETALLORDERS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.idOrder = int.Parse(reader["PK_ORDER"].ToString());
                        order.idUser = int.Parse(reader["FK_USER"].ToString());
                        order.orderDate = DateTime.Parse(reader["ORDERDATE"].ToString());
                        order.idStatus = int.Parse(reader["ORDERSTATUS"].ToString());
                        order.orderStatus = reader["DESCRIPTION"].ToString();
                        order.totalPrice = decimal.Parse(reader["TOTAL_PRICE"].ToString());

                        orders.Add(order);
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
            return orders;
        }

        public List<Detail> GetAllDetails()
        {
            List<Detail> details = new List<Detail>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM ORDERDETAILS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Detail detail = new Detail();
                        detail.idOrder = int.Parse(reader["FK_ORDER"].ToString());
                        detail.idProduct = int.Parse(reader["FK_PRODUCT"].ToString());
                        detail.price = decimal.Parse(reader["PRICE"].ToString());
                        detail.units = int.Parse(reader["UNITS"].ToString());

                        details.Add(detail);
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
            return details;
        }

        public List<Order> GetOrdersFromUser(int idUser)
        {
            List<Order> orders = new List<Order>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("GETORDERSFROMUSER @idUser", conn);
                command.Parameters.AddWithValue("@idUser", idUser);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.idOrder = int.Parse(reader["PK_ORDER"].ToString());
                        //order.idUser = int.Parse(reader["FK_USER"].ToString());
                        order.orderDate = DateTime.Parse(reader["ORDERDATE"].ToString());
                        order.orderStatus = reader["DESCRIPTION"].ToString();
                        order.totalPrice = decimal.Parse(reader["TOTAL_PRICE"].ToString());

                        orders.Add(order);
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
            return orders;
        }

        public List<OrderDetail> GetDetailsFromOrder(int idOrder, string language)
        {
            List<OrderDetail> details = new List<OrderDetail>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("GETDETAILSFROMORDER @idOrder, @language", conn);
                command.Parameters.AddWithValue("@idOrder", idOrder);
                command.Parameters.AddWithValue("@language", language);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderDetail detail = new OrderDetail();
                        //detail.idOrder = int.Parse(reader["FK_ORDER"].ToString());
                        //detail.idProduct = int.Parse(reader["FK_PRODUCT"].ToString());
                        detail.name = reader["TITLE"].ToString();
                        detail.description = reader["DESCRIPTION"].ToString();
                        detail.price = decimal.Parse(reader["PRICE"].ToString());
                        detail.units = int.Parse(reader["UNITS"].ToString());
                        detail.subtotal = detail.price * detail.units;
                        

                        details.Add(detail);
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
            return details;
        }
        

        public bool InsertOrder(InsertOrderRequest request)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("INSERTORDER @user, @price", conn);
                command.Parameters.AddWithValue("@user", request.idUser);
                command.Parameters.AddWithValue("@price", request.totalPrice);
                
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

        public bool InsertOrderDetail(InsertOrderDetailRequest request)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                Product product = new Product();
                SqlCommand command = new SqlCommand("INSERTORDERDETAIL @order, @product, @price, @units", conn);
                command.Parameters.AddWithValue("@order", request.idOrder);
                command.Parameters.AddWithValue("@product", request.idProduct);
                command.Parameters.AddWithValue("@price", request.price);
                command.Parameters.AddWithValue("@units", request.units);

                int result = command.ExecuteNonQuery();

                if (result < 0)
                {
                    UpdateStock(request.idProduct, request.units);
                    //product = GetProduct(request.idProduct);

                    //SqlCommand command2 = new SqlCommand("UPDATEPRODUCT @idProduct, @stock, @enabled", conn);
                    //command2.Parameters.AddWithValue("@stock", product.stock - request.units);
                    //command2.Parameters.AddWithValue("@enabled", product.enabled);
                    //command2.Parameters.AddWithValue("@idProduct", request.idProduct);

                    //int result2 = command2.ExecuteNonQuery();

                    //if (result2 < 0)
                    //{
                        return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}

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

        private bool UpdateStock(int idProduct, int units)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATESTOCK @product, @units", conn);
                command.Parameters.AddWithValue("@product", idProduct);
                command.Parameters.AddWithValue("@units", units);
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

        public bool UpdateOrder(int idOrder, int idStatus)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATEORDER @idOrder, @idStatus", conn);
                command.Parameters.AddWithValue("@idOrder", idOrder);
                command.Parameters.AddWithValue("@idStatus", idStatus);
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

        public Product GetProduct(int idProduct)
        {
            Product product = new Product();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
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
            return product;
        }

        public bool CancelOrder(int idOrder)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETEORDER @idOrder", conn);
                command.Parameters.AddWithValue("@idOrder", idOrder);

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

        //public bool SetPrice(SetPriceRequest request)
        //{
        //    SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

        //    try
        //    {
        //        conn.Open();

        //        if (CheckPrice(request.idProduct, request.idRate) == true)
        //        {
        //            SqlCommand command = new SqlCommand("SETPRICE @idProduct, @idRate, @price", conn);
        //            command.Parameters.AddWithValue("@idProduct", request.idProduct);
        //            command.Parameters.AddWithValue("@idRate", request.idRate);
        //            command.Parameters.AddWithValue("@price", request.price);

        //            int result = command.ExecuteNonQuery();

        //            if (result < 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        else
        //        {
        //            SqlCommand checkproduct = new SqlCommand("SELECT * FROM PRODUCTS WHERE PK_PRODUCT = @idProduct", conn);
        //            checkproduct.Parameters.AddWithValue("@idProduct", request.idProduct);
        //            using (SqlDataReader readproduct = checkproduct.ExecuteReader())
        //            {
        //                if (readproduct.Read())
        //                {
        //                    SqlCommand checkrate = new SqlCommand("SELECT * FROM RATES WHERE PK_RATE = @idRate", conn);
        //                    checkrate.Parameters.AddWithValue("@idRate", request.idRate);
        //                    using (SqlDataReader readrate = checkrate.ExecuteReader())
        //                    {
        //                        if (readrate.Read())
        //                        {
        //                            SqlCommand insert = new SqlCommand("INSERT INTO PRODUCTRATES VALUES(@idProduct, @idRate, @price)", conn);
        //                            insert.Parameters.AddWithValue("@idProduct", request.idProduct);
        //                            insert.Parameters.AddWithValue("@idRate", request.idRate);
        //                            insert.Parameters.AddWithValue("@price", request.price);

        //                            int result = insert.ExecuteNonQuery();

        //                            if (result > 0)
        //                            {
        //                                return true;
        //                            }
        //                            else
        //                            {
        //                                return false;
        //                            }
        //                        }
        //                        else
        //                            return false;
        //                    }
        //                }
        //                else
        //                    return false;
        //            }

        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        //private bool CheckPrice(int idProduct, int idRate)
        //{
        //    bool check = false;
        //    SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

        //    try
        //    {
        //        conn.Open();
        //        SqlCommand checkprice = new SqlCommand("SELECT * FROM PRODUCTRATES WHERE FK_PRODUCT = @idProduct AND FK_RATE = @idRate", conn);
        //        checkprice.Parameters.AddWithValue("@idProduct", idProduct);
        //        checkprice.Parameters.AddWithValue("@idRate", idRate);
        //        using (SqlDataReader reader = checkprice.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                check = true;
        //            }

        //            else
        //                check = false;
        //        }
        //    }
        //    catch { throw; }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return check;
        //}

    }
}
