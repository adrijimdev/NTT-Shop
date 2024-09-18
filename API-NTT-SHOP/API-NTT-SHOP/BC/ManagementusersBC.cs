using API_NTT_SHOP.DAC;
using API_NTT_SHOP.Models;
using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API_NTT_SHOP.BC
{
    public class ManagementusersBC
    {
        private readonly ManagementusersDAC manusersDAC = new ManagementusersDAC();

        


        //////VALIDATIONS

        

        

       

       

        

        private bool PasswordValidator(string password)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{10,16}$");
                if (regex.IsMatch(password))
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
}