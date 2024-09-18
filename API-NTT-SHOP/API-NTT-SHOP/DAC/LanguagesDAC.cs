using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.DAC
{
    public class LanguagesDAC
    {
        public List<Language> GetAllLanguages()
        {
            List<Language> languages = new List<Language>();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {  
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT PK_LANGUAGE,DESCRIPTION,ISO FROM LANGUAGES", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Language language = new Language();
                        language.idLanguage = int.Parse(reader["PK_LANGUAGE"].ToString());
                        language.iso = reader["ISO"].ToString();
                        language.description = reader["DESCRIPTION"].ToString();

                        languages.Add(language);
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
            return languages;
        }

        public Language GetLanguage(int idLanguage)
        {
            Language language = new Language();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                
                SqlCommand command = new SqlCommand("SELECT PK_LANGUAGE,DESCRIPTION,ISO FROM LANGUAGES WHERE PK_LANGUAGE=@idLanguage", conn);
                command.Parameters.AddWithValue("@idLanguage", idLanguage);
                using (SqlDataReader reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {                        
                        language.idLanguage = int.Parse(reader["PK_LANGUAGE"].ToString());
                        language.iso = reader["ISO"].ToString();
                        language.description = reader["DESCRIPTION"].ToString();
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
            return language;
        }

        public bool UpdateLanguage(Language language)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE LANGUAGES SET DESCRIPTION=@description,ISO=@iso WHERE PK_LANGUAGE=@idLanguage", conn);
                command.Parameters.AddWithValue("@description", language.description);
                command.Parameters.AddWithValue("@iso", language.iso);
                command.Parameters.AddWithValue("@idLanguage", language.idLanguage);

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

        public bool InsertLanguage(Language language)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO LANGUAGES VALUES (@description, @iso)", conn);
                command.Parameters.AddWithValue("@description", language.description);
                command.Parameters.AddWithValue("@iso", language.iso);

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

        public bool DeleteLanguage(int idLanguage)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE FROM LANGUAGES WHERE PK_LANGUAGE=@idLanguage", conn);
                command.Parameters.AddWithValue("@idLanguage", idLanguage);

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
