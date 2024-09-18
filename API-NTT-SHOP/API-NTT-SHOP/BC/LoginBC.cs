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
    public class LoginBC
    {
        private readonly LoginDAC loginDAC = new LoginDAC();

        public GetLoginResponse GetLogin(GetLoginRequest request)
        {
            GetLoginResponse getLoginResponse = new GetLoginResponse();
            getLoginResponse = loginDAC.GetLogin(request);

            if (GetLoginValidation(request))
            {
                getLoginResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getLoginResponse.message = "No content";
                getLoginResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getLoginResponse;
        }

        public GetAdminLoginResponse GetAdminLogin(GetAdminLoginRequest request)
        {
            GetAdminLoginResponse response = new GetAdminLoginResponse();
            response = loginDAC.GetAdminLogin(request);

            if (GetAdminLoginValidation(request))
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

        private bool GetLoginValidation(GetLoginRequest request)
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

        private bool GetAdminLoginValidation(GetAdminLoginRequest request)
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
    }
}
