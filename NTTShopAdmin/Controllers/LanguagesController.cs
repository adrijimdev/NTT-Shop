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
    public class LanguagesController : Controller
    {
        // GET: Languages
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetLanguages();
            return View(languages);
        }

        // GET: Languages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            return View();
        }

        // POST: Languages/Create
        [HttpPost]
        public ActionResult Create(string description, string iso)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/languages/insertLanguage";

                string data = "{\"language\":{" + "\"description\":\"" + description + "\",\"iso\":\"" + iso + "\"}}";

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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(int? idLanguage, string iso)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            Session["iso"] = iso;
            if (idLanguage == null)
            {
                return HttpNotFound();
            }
            Entities.Language language = new Entities.Language();
            List<Entities.Language> languages = new List<Entities.Language>();
            languages = (List<Entities.Language>)Session["languages"];
            for (int i = 0; i < languages.Count; i++)
            {
                if (languages[i].idLanguage == idLanguage)
                {
                    language = languages[i];
                }
            }
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: Languages/Edit/5
        [HttpPost]
        public ActionResult Edit(int idLanguage, string description)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/languages/updateLanguage";

                string data = "{\"language\":{\"idLanguage\":" + idLanguage + ",\"description\":\"" + description + "\",\"iso\":\"" + Session["iso"] + "\"}}";

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

        // GET: Languages/Delete/5
        public ActionResult Delete(int id)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            return View();
        }

        // POST: Languages/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<Entities.Language> GetLanguages()
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
                    Session["languages"] = languages;
                }
            }
            catch (Exception ex)
            {

            }
            return languages;
        }
    }
}
