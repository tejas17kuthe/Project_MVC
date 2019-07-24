
using MMi_BIS_PA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MMi_BIS_PA.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string user_name,string password)
        {
            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();

            bool result = connection.AuthenticateUser(user_name,password);

            if (result)
                return RedirectToRoute;
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