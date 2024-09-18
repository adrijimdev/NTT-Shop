using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Newtonsoft.Json;
using NTTShopAdmin.Entities;


namespace NTTShopAdmin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ManagementUser objUser)
        {
            ManagementUser usuario = new ManagementUser();
            string url = @"https://localhost:44300/api/login/getAdminLogin";

            string userEQ = objUser.login;
            string passEQ = objUser.password;

            string data = "{\"login\":\"" + userEQ + "\",\"password\":\"" + passEQ + "\"}"; // esto es crear un Json de forma manual. También podemos crear una clase con las mismas propiedades y pasarla a JSON de forma automática.

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                //httpRequest.Headers["Authorization"] = "Bearer " + token; //En nuestro caso no hay seguridad por token, esto no hace falta por ahora.
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    Dictionary<string, string> bodyContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    if (bodyContent["response"].ToString().Equals("true"))
                    {
                        usuario.idManUser = int.Parse(bodyContent["idManUser"].ToString());
                        usuario.login = bodyContent["login"].ToString();
                        usuario.password = bodyContent["password"].ToString();
                        usuario.name = bodyContent["name"].ToString();
                        usuario.surname1 = bodyContent["surname1"].ToString();
                        usuario.email = bodyContent["email"].ToString();
                        usuario.language = bodyContent["language"].ToString();

                        Session["log"] = true;
                    }
                    //else
                    //{
                    //    string script = "alert(\"Login incorrecto\");";
                    //    ScriptManager.RegisterStartupScript(this, GetType(),
                    //                          "ServerControlScript", script, true);
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            if (usuario.login != null)
            {
                Session["idManUser"] = usuario.idManUser.ToString();
                Session["login"] = usuario.login.ToString();
                return RedirectToAction("../Home/Index");
            }
            else
                return View(objUser);
        }


        public ActionResult UserDashBoard()
        {
            if (Session["idManUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}