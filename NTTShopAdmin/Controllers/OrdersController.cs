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
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Order> orders = new List<Entities.Order>();
            orders = GetOrders();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int idOrder)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.OrderDetail> details = new List<Entities.OrderDetail>();
            details = GetDetails(idOrder);
            return View(details);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? idOrder)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idOrder == null)
            {
                return HttpNotFound();
            }
            Entities.Order order = new Entities.Order();
            List<Entities.Order> orders = new List<Entities.Order>();
            orders = (List<Entities.Order>)Session["orders"];
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].idOrder == idOrder)
                {
                    order = orders[i];
                }
            }
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int idOrder, int idStatus)
        {
            try
            {
                string url = @"https://localhost:44300/api/orders/updateOrder";

                string data = "{\"idOrder\":" + idOrder + ",\"idStatus\":\"" + idStatus + "\"}";

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

        // GET: Orders/Delete/5
        public ActionResult Cancel(int? idOrder)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idOrder == null)
            {
                return HttpNotFound();
            }
            Entities.Order order = new Entities.Order();
            List<Entities.Order> orders = new List<Entities.Order>();
            orders = (List<Entities.Order>)Session["orders"];
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].idOrder == idOrder)
                {
                    order = orders[i];
                }
            }
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Cancel(int idOrder)
        {
            try
            {
                string url = @"https://localhost:44300/api/orders/cancelOrder";

                string data = "{\"idOrder\":" + idOrder + "}}";

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

        private List<Entities.OrderDetail> GetDetails(int idOrder)
        {
            List<Entities.OrderDetail> details = new List<Entities.OrderDetail>();

            try
            {
                string url = @"https://localhost:44300/api/orders/getDetailsFromOrder";
                string data = "{\"idOrder\":" + idOrder + ",\"language\":\"" + Session["language"] + "\"}";

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

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        JObject resultado = JObject.Parse(result);
                        var list = resultado["details"].Value<JArray>();
                        details = list.ToObject<List<Entities.OrderDetail>>();
                        Session["details"] = details;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            { 
            
            }
            return details;
        }
    }
}
