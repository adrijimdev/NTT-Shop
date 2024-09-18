using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTTShopAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            return View();
        }

        public ActionResult About()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Convert.ToString(Session["log"]) != Convert.ToString(true))
            {
                Response.Redirect("/Login/Login");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}