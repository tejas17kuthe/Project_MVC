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
        [Route("Reports/GetYears")]
        public JsonResult GetYears()
        {
            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();
            List<int> year = connection.GetYear();
            ViewBag.Year = new SelectList(year, "date_time", "date_time");
            return Json(year, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Reports/GetMonths")]
        public JsonResult GetMonths(string year)
        {
            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();
            List<int> months = connection.GetMonth(year);

            return Json(months, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Reports/GetDates")]
        public JsonResult GetDates(string year, string month)
        {
            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();
            List<int> date = connection.GetDate(year, month);

            return Json(date, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Reports/GetShifts")]
        public JsonResult GetShifts(string year, string month, string Date)
        {
            MySqlDatabaseInteraction connection = new MySqlDatabaseInteraction();
            List<int> shift = connection.GetShift(year, month, Date);

            return Json(shift, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult GenerateReport(string year)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            return PartialView("_DataCard", i);
        }

        public PartialViewResult GenerateReport(string year,string month)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            return PartialView("_DataCard", i);
        }

        public PartialViewResult GenerateReport(string year,string month,string date)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            return PartialView("_DataCard", i);
        }

        public PartialViewResult GenerateReport(string year, string month, string date,string shift)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            return PartialView("_DataCard", i);
        }

    }
}