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
    public class UsersDAC
    {
        public List<Users> GetAllUsers()
        {
            List<Users> users = new List<Users>();

            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                for (int i = 0; i < context.Users.Count(); i++)
                {
                    users.Add(context.Users.ToList()[i]);
                }
            }
            return users;
        }

        public List<Managementusers> GetAllManUsers()
        {
            List<Managementusers> users = new List<Managementusers>();

            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                for (int i = 0; i < context.Managementusers.Count(); i++)
                {
                    users.Add(context.Managementusers.ToList()[i]);
                }
            }
            return users;
        }

        public Users GetUser(int idUser)
        {
            Users user = new Users();

            using (NTTSHOPContext context = new NTTSHOPContext())
            {

                // Lambda LinQ
                Users userTemp = context.Users.FirstOrDefault(x => x.PkUser == idUser);

                if (userTemp != null)
                {
                    user.PkUser = userTemp.PkUser;
                    user.Login = userTemp.Login;
                    user.Password = userTemp.Password;
                    user.Name = userTemp.Name;
                    user.Surname1 = userTemp.Surname1;
                    user.Surname2 = userTemp.Surname2;
                    user.Address = userTemp.Address;
                    user.Province = userTemp.Province;
                    user.Town = userTemp.Town;
                    user.PostalCode = userTemp.PostalCode;
                    user.Phone = userTemp.Phone;
                    user.Email = userTemp.Email;
                    user.Language = userTemp.Language;
                    user.Rate = userTemp.Rate;
                }
            }

            return user;
            
        }

        public Users GetUserId(string login)
        {
            Users user = new Users();

            using (NTTSHOPContext context = new NTTSHOPContext())
            {

                // Lambda LinQ
                Users userTemp = context.Users.FirstOrDefault(x => x.Login == login);

                if (userTemp != null)
                {
                    user.PkUser = userTemp.PkUser;
                    user.Login = userTemp.Login;
                    user.Password = userTemp.Password;
                    user.Name = userTemp.Name;
                    user.Surname1 = userTemp.Surname1;
                    user.Surname2 = userTemp.Surname2;
                    user.Address = userTemp.Address;
                    user.Province = userTemp.Province;
                    user.Town = userTemp.Town;
                    user.PostalCode = userTemp.PostalCode;
                    user.Phone = userTemp.Phone;
                    user.Email = userTemp.Email;
                    user.Language = userTemp.Language;
                    user.Rate = userTemp.Rate;
                }
            }
            return user;
        }

        public Managementusers GetManUser(int idUser)
        {
            Managementusers manuser = new Managementusers();

            using (NTTSHOPContext context = new NTTSHOPContext())
            {

                // Lambda LinQ
                Managementusers userTemp = context.Managementusers.FirstOrDefault(x => x.PkManuser == idUser);

                if (userTemp != null)
                {
                    manuser.PkManuser = userTemp.PkManuser;
                    manuser.Login = userTemp.Login;
                    manuser.Password = userTemp.Password;
                    manuser.Name = userTemp.Name;
                    manuser.Surname1 = userTemp.Surname1;
                    manuser.Surname2 = userTemp.Surname2;
                    manuser.Email = userTemp.Email;
                    manuser.Language = userTemp.Language;
                }
            }

            return manuser;
        }

        public bool UpdateUser(Users user)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                try
                {
                    if (context.Users.Any(x => x.PkUser == user.PkUser))
                    {
                        Users userTemp = context.Users.FirstOrDefault(x => x.PkUser == user.PkUser);
                        if (user.Login != null && user.Login != "")
                        {
                            userTemp.Login = user.Login;
                        }
                        if (user.Name != null && user.Name != "")
                        {
                            userTemp.Name = user.Name;
                        }
                        if (user.Surname1 != null && user.Surname1 != "")
                        {
                            userTemp.Surname1 = user.Surname1;
                        }
                        if (user.Surname2 != null && user.Surname2 != "")
                        {
                            userTemp.Surname2 = user.Surname2;
                        }
                        if (user.Address != null && user.Address != "")
                        {
                            userTemp.Address = user.Address;
                        }
                        if (user.Province != null && user.Province != "")
                        {
                            userTemp.Province = user.Province;
                        }
                        if (user.Town != null && user.Town != "")
                        {
                            userTemp.Town = user.Town;
                        }
                        if (user.PostalCode != null && user.PostalCode != "")
                        {
                            userTemp.PostalCode = user.PostalCode;
                        }
                        if (user.Phone != null && user.Phone != "")
                        {
                            userTemp.Phone = user.Phone;
                        }
                        if (user.Email != null && user.Email != "")
                        {
                            userTemp.Email = user.Email;
                        }
                        if (user.Language != null && user.Language != "")
                        {
                            userTemp.Language = user.Language;
                        }
                        if (user.Rate != null)
                        {
                            userTemp.Rate = user.Rate;
                        }

                        context.Users.Update(userTemp);
                        context.SaveChanges();
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
            }
        }

        public bool UpdateManUser(Managementusers manuser)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                try
                {
                    if (context.Managementusers.Any(x => x.PkManuser == manuser.PkManuser))
                    {
                        Managementusers userTemp = context.Managementusers.FirstOrDefault(x => x.PkManuser == manuser.PkManuser);
                        if (manuser.Login != null)
                        {
                            userTemp.Login = manuser.Login;
                        }
                        if (manuser.Name != null)
                        {
                            userTemp.Name = manuser.Name;
                        }
                        if (manuser.Surname1 != null)
                        {
                            userTemp.Surname1 = manuser.Surname1;
                        }
                        if (manuser.Surname2 != null)
                        {
                            userTemp.Surname2 = manuser.Surname2;
                        }
                        if (manuser.Email != null)
                        {
                            userTemp.Email = manuser.Email;
                        }
                        if (manuser.Language != null)
                        {
                            userTemp.Language = manuser.Language;
                        }

                        context.Managementusers.Update(userTemp);
                        context.SaveChanges();
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
            }
        }

        public bool UpdatePassword(Users user)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                try
                {
                    if (context.Users.Any(x => x.PkUser == user.PkUser))
                    {
                        Users userTemp = context.Users.FirstOrDefault(x => x.PkUser == user.PkUser);
                        if (user.Password != null)
                        {
                            userTemp.Password = Encript(user.Password);
                        }
                        context.Users.Update(userTemp);
                        context.SaveChanges();
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
            }
        }

        public bool UpdateManUserPassword(Managementusers manuser)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                try
                {
                    if (context.Managementusers.Any(x => x.PkManuser == manuser.PkManuser))
                    {
                        Managementusers userTemp = context.Managementusers.FirstOrDefault(x => x.PkManuser == manuser.PkManuser);
                        if (manuser.Password != null)
                        {
                            userTemp.Password = Encript(manuser.Password);
                        }
                        context.Managementusers.Update(userTemp);
                        context.SaveChanges();
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
            }
        }

        public bool InsertUser(Users user)
        {
            try
            {
                using (NTTSHOPContext context = new NTTSHOPContext())
                {
                    user.Password = Encript(user.Password);
                    if (user.Rate == null)
                        user.Rate = 1;
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
            }
            catch 
            {
                throw;
            }
        }

        public bool InsertManUser(Managementusers manuser)
        {
            try
            {
                using (NTTSHOPContext context = new NTTSHOPContext())
                {

                    manuser.Password = Encript(manuser.Password);
                    context.Managementusers.Add(manuser);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteUser(int idUser)
        {
            Users user = new Users();
            try
            {
                using (NTTSHOPContext context = new NTTSHOPContext())
                {
                    //if (CheckUserOrders(idUser) == true)
                    //{
                    //    return false;
                    //}
                    //else
                    //{
                        user = context.Users.FirstOrDefault(x => x.PkUser == idUser);
                        context.Users.Remove(user);
                        context.SaveChanges();
                        return true;
                    //}
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckUserOrders(int idUser)
        {
            SqlConnection conn = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT FK_USER FROM ORDERS", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    if (idUser == int.Parse(reader["FK_USER"].ToString()))
                    {
                            return true;
                    }                    
                }
                return false;
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

        public bool DeleteManUser(int idManUser)
        {
            Managementusers manuser = new Managementusers();
            try
            {
                using (NTTSHOPContext context = new NTTSHOPContext())
                {
                    manuser = context.Managementusers.FirstOrDefault(x => x.PkManuser == idManUser);
                    context.Managementusers.Remove(manuser);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
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
