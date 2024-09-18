using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NTTShopAdmin.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admins
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Admin> admins = new List<Entities.Admin>();
            admins = GetAdmins();
            return View(admins);
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? idAdmin)
        {
            Session["idAdmin"] = idAdmin;
            if (idAdmin == null)
            {
                return HttpNotFound();
            }
            Entities.Admin admin = new Entities.Admin();
            List<Entities.Admin> admins = new List<Entities.Admin>();
            admins = (List<Entities.Admin>)Session["admins"];
            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i].PkManuser == idAdmin)
                {
                    admin = admins[i];
                }
            }
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetAllLanguages();
            ViewData["languages"] = languages;
            return View();
        }

        // POST: Admins/Create
        [HttpPost]
        public ActionResult Create(string login, string password, string name, string surname1, string surname2, string email, string language)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/users/insertManUser";

                string data = "{\"manuser\":{\"Login\":\"" + login + "\",\"Password\":\"" + password + "\",\"Name\":\"" + name + "\",\"Surname1\":\"" + surname1
                    + "\",\"Surname2\":\"" + surname2 + "\",\"Email\":\"" + email + "\",\"Language\":\"" + language + "\"}}";

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
                    ViewBag.Alert("Ha habido un error durante la creación del administrador.");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? idAdmin)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idAdmin == null)
            {
                return HttpNotFound();
            }
            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetAllLanguages();
            ViewData["languages"] = languages;

            Entities.Admin admin = new Entities.Admin();
            List<Entities.Admin> admins = new List<Entities.Admin>();
            admins = (List<Entities.Admin>)Session["admins"];
            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i].PkManuser == idAdmin)
                {
                    admin = admins[i];
                }
            }
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        public ActionResult Edit(int idAdmin, string login, string name, string surname1, string surname2, string email, string language)
        {
            try
            {
                string url = @"https://localhost:44300/api/users/updateManUser";

                string data = "{\"manuser\":{\"PkManuser\":" + idAdmin + ",\"Login\":\"" + login + "\",\"Name\":\"" + name + "\",\"Surname1\":\"" + surname1
                    + "\",\"Surname2\":\"" + surname2 + "\",\"Email\":\"" + email + "\",\"Language\":\"" + language + "\"}}";

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

        // GET: Admins/Delete/5
        public ActionResult Delete(int? idAdmin)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idAdmin == null)
            {
                return HttpNotFound();
            }
            Entities.Admin admin = new Entities.Admin();
            List<Entities.Admin> admins = new List<Entities.Admin>();
            admins = (List<Entities.Admin>)Session["admins"];
            for (int i = 0; i < admins.Count; i++)
            {
                if (admins[i].PkManuser == idAdmin)
                {
                    admin = admins[i];
                }
            }
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost]
        public ActionResult Delete(int idAdmin)
        {
            try
            {
                string url = @"https://localhost:44300/api/users/deleteManUser";

                string data = "{\"PkManuser\":" + idAdmin + "}}";

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

        private List<Entities.Admin> GetAdmins()
        {
            List<Entities.Admin> admins = new List<Entities.Admin>();

            string url = @"https://localhost:44300/api/users/getAllManUsers";

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
                    var list = resultado["manusers"].Value<JArray>();
                    admins = list.ToObject<List<Entities.Admin>>();
                    Session["admins"] = admins;
                }
            }
            catch (Exception ex)
            {

            }
            return admins;
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
    }
}
