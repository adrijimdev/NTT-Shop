using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NTTShopAdmin;
using NTTShopAdmin.Entities;

namespace NTTShopAdmin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Product> products = new List<Entities.Product>();
            products = GetProducts();
            return View(products);
        }

        // GET: ProductDescriptions
        public ActionResult IndexDescriptions(int? idProduct)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            Session["product"] = idProduct;
            List<Entities.Description> descriptions = new List<Entities.Description>();
            descriptions = GetDescriptions(idProduct);
            return View(descriptions);
        }

        // GET: ProductRates
        public ActionResult IndexRates(int? idProduct)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            Session["product"] = idProduct;
            List<Entities.ProductRate> rates = new List<Entities.ProductRate>();
            rates = GetRates(idProduct);
            return View(rates);
        }
        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(/*FormCollection collection*/string name, string description, string stock)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/products/insertProduct";
                Entities.InsertProduct product = new Entities.InsertProduct();
                //if (Session["idUser"] != null)
                //{
                //    //Converting your session variable value to integer

                //}
                product.name = name;
                product.description = description;
                product.stock = Convert.ToInt32(stock);
                if (product.stock > 0)
                {
                    product.enabled = "true";
                }
                else
                { 
                    product.enabled = "false";
                }
                product.language = Convert.ToString(Session["language"]);

                string data = "{\"product\":{" + "\"name\":\"" + product.name + "\",\"description\":\"" + product.description + "\",\"stock\":" + product.stock + ",\"enabled\":\"" + Convert.ToString(product.enabled) + "\",\"language\":\"" + product.language + "\"}}";

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

        // GET: Products/Create
        [HttpGet]
        public ActionResult CreateDescription()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetAllLanguages();
            ViewData["languages"] = languages;
            //var language = SdProveedor.ListaTipoDocumentoIdentidad();
            //ViewBag.ListaProveedores = ListaProveedores.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Descripcion }).ToList<SelectListItem>();
            //return View();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult CreateDescription(/*FormCollection collection*/string name, string description, string language)
        {
            List<Description> descriptions = new List<Description>();
            descriptions = (List<Description>)Session["descriptions"];
            bool check = false;
            for (int i = 0; i < descriptions.Count; i++)
            {
                if (descriptions[i].language == language)
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                @ViewBag.Error = "ERROR! El producto ya tiene una descripción en ese idioma.";
                List<Language> languages = new List<Language>();
                languages = GetAllLanguages();
                ViewData["languages"] = languages;
                return View();
            }
            if (check == false)
            {
                try
                {
                    // TODO: Add insert logic here
                    string url = @"https://localhost:44300/api/products/insertDescription";

                    string data = "{\"product\":" + Session["product"] + ",\"language\":\"" + language + "\",\"name\":\"" + name + "\",\"description\":\"" + description + "\"}";

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
                    @ViewBag.Message = "Descripción añadida correctamente.";
                    List<Language> languages = new List<Language>();
                    languages = GetAllLanguages();
                    ViewData["languages"] = languages;
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                List<Language> languages = new List<Language>();
                languages = GetAllLanguages();
                ViewData["languages"] = languages;
                return View();
            }
        }

        // GET: Products/Create
        [HttpGet]
        public ActionResult CreateRate()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            List<Entities.Rate> rates = new List<Entities.Rate>();
            rates = GetAllRates();
            ViewData["rates"] = rates;
            return View();
        }

        //POST: Products/Create
        [HttpPost]
        public ActionResult CreateRate(int rate, string price)
        {
            List<ProductRate> rates = new List<ProductRate>();
            rates = GetRates(int.Parse(Session["product"].ToString()));
            bool check = false;
            for (int i = 0; i < rates.Count; i++)
            {
                if (rates[i].product == int.Parse(Session["product"].ToString()) && rates[i].idRate == rate)
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                @ViewBag.Error = "ERROR! El producto ya tiene un precio estipulado para esa tarifa";
            }
            if (check == false)
            {
                try
                {
                    // TODO: Add insert logic here
                    string url = @"https://localhost:44300/api/products/insertRate";
                    price = price.Replace(",", ".");
                    string data = "{\"product\":" + Session["product"] + ",\"rate\":\"" + rate + "\",\"price\":\"" + price + "\"}";

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
                    @ViewBag.Message = "El nuevo precio se ha añadido correctamente.";
                    List<Entities.Rate> ratesList = new List<Entities.Rate>();
                    ratesList = GetAllRates();
                    ViewData["rates"] = ratesList;
                    return View();
                }
                catch
                {
                    return View();
                }
            }

            else
            {
                List<Entities.Rate> ratesList = new List<Entities.Rate>();
                ratesList = GetAllRates();
                ViewData["rates"] = ratesList;
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? idProduct)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idProduct == null)
            {
                return HttpNotFound();
            }
            Entities.Product product = new Entities.Product();
            List<Entities.Product> products = new List<Entities.Product>();
            products = (List<Entities.Product>)Session["products"];
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].idProduct == idProduct)
                { 
                    product = products[i];
                }
            }            
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(string idProduct, string stock)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/products/updateProduct";
                Entities.UpdateProduct product = new Entities.UpdateProduct();
                //if (Session["idUser"] != null)
                //{
                //    //Converting your session variable value to integer

                //}
                product.idProduct = Convert.ToInt32(idProduct);
                product.stock = Convert.ToInt32(stock);
                if (product.stock > 0)
                {
                    product.enabled = "true";
                }
                else
                {
                    product.enabled = "false";
                }
                product.language = Convert.ToString(Session["language"]);

                string data = "{\"product\":{\"idProduct\":" + product.idProduct + ",\"stock\":" + product.stock + ",\"enabled\":\"" + Convert.ToString(product.enabled) + "\"}}";

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

        // GET: Products/Edit/5
        public ActionResult EditDescription(int? idDescription, int? product)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idDescription == null)
            {
                return HttpNotFound();
            }
            Session["idDescription"] = idDescription;
            Session["product"] = product;
            Entities.Description description = new Entities.Description();
            List<Entities.Description> descriptions = new List<Entities.Description>();
            descriptions = (List<Entities.Description>)Session["descriptions"];
            for (int i = 0; i < descriptions.Count; i++)
            {
                if (descriptions[i].idProductDescription == idDescription)
                {
                description = descriptions[i];
                }
            }
            if (description == null)
            {
                return HttpNotFound();
            }

            List<Entities.Language> languages = new List<Entities.Language>();
            languages = GetAllLanguages();
            ViewData["languages"] = languages;
            return View(description);
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

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult EditDescription(string name, string description, string language)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/products/updateDescription";
                Entities.Description objDescription = new Entities.Description();
                objDescription.product = Convert.ToInt32(Session["product"]);
                objDescription.idProductDescription = Convert.ToInt32(Session["idDescription"]);
                objDescription.name = name;
                objDescription.description = description;
                objDescription.language = language;

                string data = "{\"description\":{" + "\"idProductDescription\":" + objDescription.idProductDescription + ",\"product\":" + objDescription.product + ",\"name\":\"" + objDescription.name + "\",\"description\":\"" + objDescription.description + "\",\"language\":\"" + objDescription.language + "\"}}";

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

        // GET: Products/Edit/5
        public ActionResult EditRate(int? product, int? idRate)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idRate == null || product == null)
            {
                return HttpNotFound();
            }
            Session["idRate"] = idRate;
            Session["product"] = product;
            Entities.ProductRate rate = new Entities.ProductRate();
            List<Entities.ProductRate> rates = new List<Entities.ProductRate>();
            rates = (List<Entities.ProductRate>)Session["rates"];
            for (int i = 0; i < rates.Count; i++)
            {
                if (rates[i].idRate == idRate && rates[i].product == product)
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

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult EditRate(string price)
        {
            try
            {
                // TODO: Add insert logic here
                string url = @"https://localhost:44300/api/products/setPrice";
                Entities.ProductRate objRate = new Entities.ProductRate();
                objRate.product = Convert.ToInt32(Session["product"]);
                objRate.idRate = Convert.ToInt32(Session["idRate"]);
                price = price.Replace(",", ".");
                objRate.price = Convert.ToDecimal(price);

                string data = "{\"idProduct\":" + Session["product"] + ",\"idRate\":" + Session["idRate"] + ",\"price\":" + price + "}";

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

        // GET: Products/Delete/5
        public ActionResult Delete(int? idProduct)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idProduct == null)
            {
                return HttpNotFound();
            }
            Entities.Product product = new Entities.Product();
            List<Entities.Product> products = new List<Entities.Product>();
            products = (List<Entities.Product>)Session["products"];
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].idProduct == idProduct)
                {
                    product = products[i];
                }
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int idProduct)
        {
            try
            {
                string url = @"https://localhost:44300/api/products/deleteProduct";

                string data = "{\"idProduct\":" + idProduct + "}}";

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

        public ActionResult DeleteDescription(int? idDescription)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (idDescription == null)
            {
                return HttpNotFound();
            }
            Entities.ProductDescription description = new Entities.ProductDescription();
            List<Entities.ProductDescription> descriptions = new List<Entities.ProductDescription>();
            descriptions = (List<Entities.ProductDescription>)Session["users"];
            for (int i = 0; i < descriptions.Count; i++)
            {
                if (descriptions[i].idProductDescription == idDescription)
                {
                    description = descriptions[i];
                }
            }
            if (description == null)
            {
                return HttpNotFound();
            }
            return View(description);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult DeleteDescription(int product, int idProductDescription, string language)
        {
            if (Session["details"] != null)
            {
                List<Entities.Detail> details = new List<Entities.Detail>();
                details = (List<Entities.Detail>)Session["details"];
                for (int i = 0; i < details.Count; i++)
                {
                    if (details[i].idProduct == product)
                    {
                        @ViewBag.Error = "ERROR! La descripción está asociada a un producto existente en uno o varios pedidos.";
                        return View();
                    }
                }
            }
            else
            {
                List<Entities.Detail> details = new List<Entities.Detail>();
                details = GetDetails();
                for (int i = 0; i < details.Count; i++)
                {
                    if (details[i].idProduct == product)
                    {
                        @ViewBag.Error = "ERROR! La descripción está asociada a un producto existente en uno o varios pedidos.";
                        return View();
                    }
                }
            }
            try
            {
                string url = @"https://localhost:44300/api/products/deleteDescription";

                string data = "{\"idProductDescription\":" + idProductDescription + "}}";

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

        public ActionResult DeleteRate(int? product, int? idRate)
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            if (product == null || idRate == null)
            {
                return HttpNotFound();
            }
            Entities.ProductRate rate = new Entities.ProductRate();
            List<Entities.ProductRate> rates = new List<Entities.ProductRate>();
            rates = (List<Entities.ProductRate>)Session["productrates"];
            for (int i = 0; i < rates.Count; i++)
            {
                if (rates[i].idRate == idRate && rates[i].product == product)
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

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult DeleteRate(int product, int idRate)
        {
            if (Session["details"] != null)
            {
                List<Entities.Detail> details = new List<Entities.Detail>();
                details = (List<Entities.Detail>)Session["details"];
                for (int i = 0; i < details.Count; i++)
                {
                    if (details[i].idProduct == product)
                    {
                        @ViewBag.Error = "ERROR! El precio está asociado a un producto existente en uno o varios pedidos.";
                        return View();
                    }
                }
            }
            else
            {
                List<Entities.Detail> details = new List<Entities.Detail>();
                details = GetDetails();
                for (int i = 0; i < details.Count; i++)
                {
                    if (details[i].idProduct == product)
                    {
                        @ViewBag.Error = "ERROR! El precio está asociado a un producto existente en uno o varios pedidos.";
                        return View();
                    }
                }
            }
            try
            {
                string url = @"https://localhost:44300/api/products/deleteRate";

                string data = "{\"idProduct\":" + product + ",\"idRate\":" + idRate + "}}";

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

        private List<Entities.Product> GetProducts()
        { 
            List<Entities.Product> products = new List<Entities.Product>();

            string url = @"https://localhost:44300/api/products/productsForAdmin";

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
                    var list = resultado["products"].Value<JArray>();
                    products = list.ToObject<List<Entities.Product>>();
                    Session["products"] = products;
                }
            }
            catch (Exception ex)
            {

            }
            return products;        
        }

        private List<Entities.Description> GetDescriptions(int? idProduct)
        {
            List<Entities.Description> descriptions = new List<Entities.Description>();

            string url = @"https://localhost:44300/api/products/getProductDescriptions";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                string data = "{\"idProduct\":" + idProduct + "}";

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
                    var list = resultado["descriptions"].Value<JArray>();
                    descriptions = list.ToObject<List<Entities.Description>>();
                    Session["descriptions"] = descriptions;
                }
            }
            catch (Exception ex)
            {

            }
            return descriptions;
        }

        private List<Entities.ProductRate> GetRates(int? idProduct)
        {
            List<Entities.ProductRate> rates = new List<Entities.ProductRate>();

            string url = @"https://localhost:44300/api/products/getProductRates";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                string data = "{\"idProduct\":" + idProduct + "}";

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
                    var list = resultado["rates"].Value<JArray>();
                    rates = list.ToObject<List<Entities.ProductRate>>();
                    Session["productrates"] = rates;
                }
            }
            catch (Exception ex)
            {

            }
            return rates;
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

        private List<Entities.ProductRate> GetAllProductRates()
        {
            List<Entities.ProductRate> rates = new List<Entities.ProductRate>();

            string url = @"https://localhost:44300/api/products/getAllProductRates";

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
                    var list = resultado["productrates"].Value<JArray>();
                    rates = list.ToObject<List<Entities.ProductRate>>();
                }
            }
            catch (Exception ex)
            {

            }
            return rates;
        }

        private List<Entities.Detail> GetDetails()
        {
            List<Entities.Detail> details = new List<Entities.Detail>();

            string url = @"https://localhost:44300/api/orders/getAllDetails";

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
                    var list = resultado["details"].Value<JArray>();
                    details = list.ToObject<List<Entities.Detail>>();
                    Session["details"] = details;
                }
            }
            catch (Exception ex)
            {

            }
            return details;
        }


        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}
