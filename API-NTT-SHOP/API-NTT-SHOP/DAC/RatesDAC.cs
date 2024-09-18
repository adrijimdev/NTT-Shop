using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.DAC
{
    public class RatesDAC
    {
        public List<Rate> GetAllRates()
        {
            List<Rate> rates = new List<Rate>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_RATE,DESCRIPTION,_DEFAULT FROM RATES", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rate rate = new Rate();
                        rate.idRate = int.Parse(reader["PK_RATE"].ToString());
                        rate._default = Convert.ToBoolean(reader["_DEFAULT"].ToString());
                        rate.description = reader["DESCRIPTION"].ToString();
                        
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

        public Rate GetRate(int idRate)
        {
            Rate rate = new Rate();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_RATE,DESCRIPTION,_DEFAULT FROM RATES WHERE PK_RATE=@idRate", conn);
                command.Parameters.AddWithValue("@idRate", idRate);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rate.idRate = int.Parse(reader["PK_RATE"].ToString());
                        rate._default = bool.Parse(reader["_DEFAULT"].ToString());
                        rate.description = reader["DESCRIPTION"].ToString();
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
            return rate;
        }

        public bool UpdateRate(Rate rate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            if (rate._default == true)
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("UPDATERATE1 @idrate, @description", conn);
                    command.Parameters.AddWithValue("@idRate", rate.idRate); 
                    command.Parameters.AddWithValue("@description", rate.description);
                    

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
            else
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("UPDATERATE2 @idrate, @description", conn);
                    command.Parameters.AddWithValue("@idRate", rate.idRate);
                    command.Parameters.AddWithValue("@description", rate.description);

                    if (CheckRate(rate.description) == true)
                    {
                        return false;
                    }
                    else
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

        public bool InsertRate(Rate rate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO RATES VALUES (@description, @_default)", conn);
                command.Parameters.AddWithValue("@description", rate.description);
                command.Parameters.AddWithValue("@_default", rate._default);

                int result = command.ExecuteNonQuery();

                if (result > 0)
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

        public bool CheckRate(string description)
        {
            bool check = false;
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM RATES", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (description == reader["DESCRIPTION"].ToString())
                    {
                        check = true;
                    }
                }
            }
            return check;

        }
        public bool DeleteRate(int idRate)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE FROM RATES WHERE PK_RATE=@idRate", conn);
                command.Parameters.AddWithValue("@idRate", idRate);

                int result = command.ExecuteNonQuery();

                if (result > 0)
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

    }
}
