using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;
using MMi_BIS_PA.Models;

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
        public ActionResult Index(string user_name,string password) {

            if (new MySqlDatabaseInteraction().AuthenticateUser(user_name, password))
                return RedirectToAction("CurrentDataPage/CurrentDataPage");
            else
                return View();

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