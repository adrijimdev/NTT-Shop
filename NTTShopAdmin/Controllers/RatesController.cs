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
    public class RatesController : Controller
    {
        // GET: Rates
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Rate> rates = new List<Entities.Rate>();
            rates = GetRates();
            return View(rates);
        }

        // GET: Rates/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rates/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            return View();
        }

        // POST: Rates/Create
        [HttpPost]
        public ActionResult Create(string description)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/rates/insertRate";
                bool _default = false;

                string data = "{\"rate\":{" + "\"description\":\"" + description + "\",\"_default\":\"" + _default + "\"}}";

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

        // GET: Rates/Edit/5
        public ActionResult Edit(int? idRate)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            Session["idRate"] = idRate;
            if (idRate == null)
            {
                return HttpNotFound();
            }
            Entities.Rate rate = new Entities.Rate();
            List<Entities.Rate> rates = new List<Entities.Rate>();
            rates = (List<Entities.Rate>)Session["rates"];
            for (int i = 0; i < rates.Count; i++)
            {
                if (rates[i].idRate == idRate)
                {
                    rate = rates[i];
                }
            }
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rates/Edit/5
        [HttpPost]
        public ActionResult Edit(string description, bool _default)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/rates/updateRate";

                string data = "{\"rate\":{\"idRate\":" + Session["idRate"] + ",\"description\":\"" + description + "\",\"_default\":\"" + _default + "\"}}";

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
                    using (var streamWriter = new StreamWriter(httpResponse.GetResponseStream()))
                    {
                        streamWriter.Write(data);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Alert = "Error.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Rates/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rates/Delete/5
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

        private List<Entities.Rate> GetRates()
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
                    Session["rates"] = rates;
                }
            }
            catch (Exception ex)
            {

            }
            return rates;
        }
    }
}
