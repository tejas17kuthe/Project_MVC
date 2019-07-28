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
    }
}