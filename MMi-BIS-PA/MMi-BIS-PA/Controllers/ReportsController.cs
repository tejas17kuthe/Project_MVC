using MMi_BIS_PA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMi_BIS_PA.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult ViewReports()
        {
            List<currentdata> data = new MySqlDatabaseInteraction().GetCurrentData();
            return View(data);
        }
        [HttpGet]
        [Route("Reports/GetYear")]
        public JsonResult GetYear()
        {
            //var year = new List<string>();
            //year.Add("2019");
            //year.Add("2020");

            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();
            List<int> year = connection.GetYear();
            ViewBag.Year = new SelectList(year, "date_time", "date_time");
            return Json(year, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMonths(int year)
        {
            var months = new List<string>();
           
                if(year.Equals("2019"))
                {
                    months.Add("8");
                    months.Add("9");
                    months.Add("10");
                }
                else if(year.Equals("2020"))
                {
                    months.Add("1");
                    months.Add("2");
                    months.Add("3");
                }
           

            return Json(months, JsonRequestBehavior.AllowGet);
        }
    }
}