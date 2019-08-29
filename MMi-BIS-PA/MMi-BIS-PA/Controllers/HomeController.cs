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
                string liciencekey = "3e66b20e04b95ef9b95936a1357ac98e";
                License.License key = new License.License();
                key.Getkey();
             

                if (liciencekey == key.License_key)
                {
                    return View();
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Contact AT for Licience key');</script>");
                }
            

            
        }

        [HttpPost]
        public ActionResult Index(string user_name,string password) {

            if (new MySqlDatabaseInteraction().AuthenticateUser(user_name, password))
                return RedirectToAction("CurrentDataPage","CurrentDataPage"); //RedirectToAction(<actionName>,<ControllerName>)
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