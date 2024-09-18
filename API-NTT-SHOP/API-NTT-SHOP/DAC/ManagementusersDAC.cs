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
    public class ManagementusersDAC
    {
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
