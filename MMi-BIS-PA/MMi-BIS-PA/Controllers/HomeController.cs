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
        public ActionResult Index()
        {
            currentdata data = new currentdata()
            {
                unicode=1,
                qr1="muk",
                c11=1,
                c12=0,
                r1=1,
                w1=900,
                qr2 = "muk",
                c21 = 1,
                c22 = 0,
                r2 = 1,
                w2 = 900,
                qr3 = "muk",
                c31 = 1,
                c32 = 0,
                r3 = 1,
                w3 = 900,
                qr4 = "muk",
                c41 = 1,
                c42 = 0,
                r4 = 1,
                w4 = 900,
                wd=2,
                status=0,
                shiftid=2

            };

            if (new MySqlDatabaseInteraction().AddCurrentData(data))
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