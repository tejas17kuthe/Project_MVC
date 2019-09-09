using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;
using CoreScanner;
using System.Xml;

namespace MMi_BIS_PA.Controllers
{
    public class CurrentDataPageController : Controller
    {
        static Barcode b;
        PiechartData p;
        static int tableCount;
        
        public CurrentDataPageController()
        {
            p = new PiechartData();
            b = new Barcode();
        }

        [Route("CurrentDataPage/CurrentDataPage")]
        [HttpGet]
        public ActionResult CurrentDataPage(LoginData id)
        {
            UpdatePieChart(); 
            return View();
        }

        [HttpPost]
        public ActionResult CurrentDataPage(string qr)
        {
           // InitializeBarcodeReader();
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return Content("There is some problem with the driver please check connection");
            }
        }

        [Route("CurrentDataPage/Delete")]
        public ActionResult Delete()
        {

            new MySqlDatabaseInteraction().RemoveTableData();
            return RedirectToAction("CurrentDataPage", "CurrentDataPage");

        }


        public PartialViewResult UpdateBarcode()
        {
            
            ViewBag.Barcode = b;
            return PartialView("_SearchBar");
        }

        public PartialViewResult UpdateTotalCurrentShiftJobCount()
        {
            b.CurrentShiftTotalJobCount = new MySqlDatabaseInteraction().CurrentShiftTotalJobCount();
            ViewBag.TotalCurrentShiftJobDone = b;
            return PartialView("_totalCurrentJobCount");
        }

        public PartialViewResult UpdateTable()
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            tableCount = i.Count;
            UpdatePieChart();
            return PartialView("_DataCardContent", i);
        }

        public void UpdatePieChart()
        {
            List<currentshiftdata> d = new MySqlDatabaseInteraction().UpdatePieChart();
            int c1 = 0;
            int c2 = 0;
            int r = 0;
            int w = 0;
            foreach (var data in d)
            {
                if (data.c11 == 0 || data.c21 == 0 || data.c31 == 0 || data.c41 == 0)
                {
                    c1 += 1;
                }

                if (data.c12 == 0 || data.c22 == 0 || data.c32 == 0 || data.c42 == 0)
                {
                    c2 += 1;
                }

                if (data.r1 == 0 || data.r2 == 0 || data.r3 == 0 || data.r4 == 0)
                {
                    r += 1;
                }

                if (data.wd > new MySqlDatabaseInteraction().GetWeightDifferenceSetPoint())
                {
                    w += 1;
                }
            }

            p.clip1 = c1;
            p.clip2 = c2;
            p.ring = r;
            p.weight = w;
            p.TotalSuccessfulCycles = new MySqlDatabaseInteraction().SuccessfulCycleCount();
            p.Barcode = Barcode.Data;

            ViewBag.pieData = p;

        }

        

       
    }
}

