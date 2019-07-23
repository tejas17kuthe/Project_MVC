using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;

namespace MMi_BIS_PA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();

            bool result = connection.AuthenticateUser("tejas", "kuthe");

            if (result)
                return View();
            else
                return Content("Authentication error");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}