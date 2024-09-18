using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NTTShopAdmin.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.User> users = new List<Entities.User>();
            users = GetUsers();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? idUser)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            Session["idUser"] = idUser;
            if (idUser == null)
            {
                return HttpNotFound();
            }
            Entities.User user = new Entities.User();
            List<Entities.User> users = new List<Entities.User>();
            users = (List<Entities.User>)Session["users"];
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].PkUser == idUser)
                {
                    user = users[i];
                }
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetAllLanguages();
            ViewData["languages"] = languages;

            List<Entities.Rate> rates = new List<Entities.Rate>();
            rates = GetAllRates();
            ViewData["rates"] = rates;
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(string login, string password, string name, string surname1, string surname2,
            string address, string province, string town, string postalcode, string phone, string email, string language, int rate)
        {
            //if (password != pswconfirm)
            //{
            //    ViewBag.Alert = "Las contraseñas no coinciden";
            //    return RedirectToAction("Create");
            //}
            //else
            //{
                try
                {
                    string url = @"https://localhost:44300/api/users/insertUser";

                    string data = "{\"user\":{\"Login\":\"" + login + "\",\"Password\":\"" + password + "\",\"Name\":\"" + name + "\",\"Surname1\":\"" + surname1
                        + "\",\"Surname2\":\"" + surname2 + "\",\"Address\":\"" + address + "\",\"Province\":\"" + province + "\",\"Town\":\"" + town
                        + "\",\"PostalCode\":\"" + postalcode + "\",\"Phone\":\"" + phone + "\",\"Email\":\"" + email + "\",\"Language\":\"" + language
                        + "\",\"Rate\":" + rate + "}}";

                    try
                    {
                        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpRequest.Method = "POST";

                        httpRequest.Accept = "application/json";
                        httpRequest.ContentType = "application/json";

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }

                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    }
                    catch (Exception ex)
                    {

                    }
                ViewBag.Alert = "Usuario creado correctamente";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            //}
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? idUser)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idUser == null)
            {
                return HttpNotFound();
            }
            Entities.User user = new Entities.User();
            List<Entities.User> users = new List<Entities.User>();
            users = (List<Entities.User>)Session["users"];
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].PkUser == idUser)
                {
                    user = users[i];
                }
            }
            if (user == null)
            {
                return HttpNotFound();
            }

            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetAllLanguages();
            ViewData["languages"] = languages;

            List<Entities.Rate> rates = new List<Entities.Rate>();
            rates = GetAllRates();
            ViewData["rates"] = rates;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("description");
            //dt.Columns.Add("iso");
            //List<Entities.Language> languages = new List<Entities.Language>();
            //ViewBag. = GetAllLanguages();

            //ViewBag.LanguageList = SelectList(languages, "CityID", "CityName");

            //////////
            //foreach (var item in languages)
            //{
            //    DataRow row = dt.NewRow();
            //    row["description"] = item.description;
            //    row["iso"] = item.iso;
            //    dt.Rows.Add(row);
            //}

            //ListItem i;
            //foreach (DataRow r in dt.Rows)
            //{
            //    i = new ListItem(r["description"].ToString(), r["iso"].ToString());
            //    ddlLanguage.Items.Add(i);
            //}

            return View(user);
        }
        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int idUser, string login, string name, string surname1, string surname2,
            string address, string province, string town, string postalcode, string phone, string email, string language, int rate)
        {
            try
            {
                string url = @"https://localhost:44300/api/users/updateUser";

                string data = "{\"user\":{\"PkUser\":" + idUser + ",\"Login\":\"" + login + "\",\"Name\":\"" + name + "\",\"Surname1\":\"" + surname1 
                    + "\",\"Surname2\":\"" + surname2 + "\",\"Address\":\"" + address + "\",\"Province\":\"" + province + "\",\"Town\":\"" + town 
                    + "\",\"PostalCode\":\"" + postalcode + "\",\"Phone\":\"" + phone + "\",\"Email\":\"" + email + "\",\"Language\":\"" + language 
                    + "\",\"Rate\":" + rate + "}}";

                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "PUT";

                    httpRequest.Accept = "application/json";
                    httpRequest.ContentType = "application/json";

                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                    }

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? idUser)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idUser == null)
            {
                return HttpNotFound();
            }
            Entities.User user = new Entities.User();
            List<Entities.User> users = new List<Entities.User>();
            users = (List<Entities.User>)Session["users"];
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].PkUser == idUser)
                {
                    user = users[i];
                }
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int idUser)
        {
            if (Session["orders"] != null)
            {
                List<Entities.Order> orders = new List<Entities.Order>();
                orders = (List<Entities.Order>)Session["orders"];
                for (int i = 0; i < orders.Count; i++)
                {
                    if (orders[i].idUser == idUser)
                    {
                        //mensaje
                        //ViewBag.Alert = "Lo sentimos, esta solicitud no existe.";
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                List<Entities.Order> orders = new List<Entities.Order>();
                orders = GetOrders();
                for (int i = 0; i < orders.Count; i++)
                {
                    if (orders[i].idUser == idUser)
                    {
                        //mensaje
                        //ViewBag.Alert = "Lo sentimos, esta solicitud no existe.";
                        return RedirectToAction("Index");
                    }
                }
            }
            try
            {
                string url = @"https://localhost:44300/api/users/deleteUser";

                string data = "{\"PkUser\":" + idUser + "}}";

                try
                {
                    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.Method = "DELETE";

                    httpRequest.Accept = "application/json";
                    httpRequest.ContentType = "application/json";

                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                    }

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<Entities.User> GetUsers()
        {
            List<Entities.User> users = new List<Entities.User>();

            string url = @"https://localhost:44300/api/users/getAllUsers";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject resultado = JObject.Parse(result);
                    var list = resultado["users"].Value<JArray>();
                    users = list.ToObject<List<Entities.User>>();
                    Session["users"] = users;
                }
            }
            catch (Exception ex)
            {

            }
            return users;
        }

        private List<Entities.Language> GetAllLanguages()
        {
            List<Entities.Language> languages = new List<Entities.Language>();

            string url = @"https://localhost:44300/api/languages/getAllLanguages";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject resultado = JObject.Parse(result);
                    var list = resultado["languages"].Value<JArray>();
                    languages = list.ToObject<List<Entities.Language>>();
                }
            }
            catch (Exception ex)
            {

            }
            return languages;
        }

        private List<Entities.Rate> GetAllRates()
        {
            List<Entities.Rate> rates = new List<Entities.Rate>();

            string url = @"https://localhost:44300/api/rates/getAllRates";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject resultado = JObject.Parse(result);
                    var list = resultado["rates"].Value<JArray>();
                    rates = list.ToObject<List<Entities.Rate>>();
                }
            }
            catch (Exception ex)
            {

            }
            return rates;
        }

        private List<Entities.Order> GetOrders()
        {
            List<Entities.Order> orders = new List<Entities.Order>();

            string url = @"https://localhost:44300/api/orders/getAllOrders";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject resultado = JObject.Parse(result);
                    var list = resultado["orders"].Value<JArray>();
                    orders = list.ToObject<List<Entities.Order>>();
                    Session["orders"] = orders;
                }
            }
            catch (Exception ex)
            {

            }
            return orders;
        }
    }
}
