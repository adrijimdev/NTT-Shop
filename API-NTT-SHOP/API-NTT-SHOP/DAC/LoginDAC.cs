using API_NTT_SHOP.Models;
using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API_NTT_SHOP.DAC
{
    public class LoginDAC
    {
        public GetLoginResponse GetLogin(GetLoginRequest login)
        {
            //User user = new User();
            bool confirm = false;
            GetLoginResponse response = new GetLoginResponse();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETLOGIN @login, @password", conn);
                command.Parameters.AddWithValue("@login", login.login);
                command.Parameters.AddWithValue("@password", Encript(login.password));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        confirm = true;
                    else
                        response.response = false;
                }
                if (confirm == true)
                {
                    Users user = new Users();

                    using (NTTSHOPContext context = new NTTSHOPContext())
                    {

                        // Lambda LinQ
                        user = context.Users.FirstOrDefault(x => x.Login == login.login);

                        if (user != null)
                        {
                            response.idUser = user.PkUser;
                            response.language = user.Language;
                            response.rate = (int)user.Rate;
                            //user.Rate = user.Rate;
                        }
                        response.response = true;
                    }
                }
                
            }
            catch { throw; }

            return response;
        }

        public GetAdminLoginResponse GetAdminLogin(GetAdminLoginRequest login)
        {
            //User user = new User();
            bool confirm = false;
            GetAdminLoginResponse response = new GetAdminLoginResponse();
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GETADMINLOGIN @login, @password", conn);
                command.Parameters.AddWithValue("@login", login.login);
                command.Parameters.AddWithValue("@password", Encript(login.password));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        confirm = true;
                    else
                        response.response = false;
                }
                if (confirm == true)
                {
                    Managementusers admin = new Managementusers();

                    using (NTTSHOPContext context = new NTTSHOPContext())
                    {

                        // Lambda LinQ
                        admin = context.Managementusers.FirstOrDefault(x => x.Login == login.login);

                        if (admin != null)
                        {
                            response.idManUser = admin.PkManuser;
                            response.login = admin.Login;
                            response.password = admin.Password;
                            response.name = admin.Name;
                            response.surname1 = admin.Surname1;
                            response.email = admin.Email;
                            response.language = admin.Language;
                        }
                        response.response = true;
                    }
                }

            }
            catch { throw; }

            return response;
        }

        public static string Encript(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
