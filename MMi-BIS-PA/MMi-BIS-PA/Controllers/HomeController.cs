using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;
using MMi_BIS_PA.App_Data;

namespace MMi_BIS_PA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            //if (new MySqlDatabaseInteraction().AuthenticateUser("tejas", "kuthe"))
            //    return View();
            //else
            //    return Content("Authentication error");

            user_info data = new user_info()
            {
              username="tejas kuthe new",
              password="12345",
              id = 50
            
            };

            new MySqlDatabaseInteraction().SetCurrentData(data);

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