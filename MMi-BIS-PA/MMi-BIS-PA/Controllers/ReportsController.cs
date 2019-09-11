using MMi_BIS_PA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MMi_BIS_PA.Controllers
{
    public class ReportsController : Controller
    {
        PiechartData p;
        public ReportsController()
        {
            p = new PiechartData();
        }
        // GET: Reports
        public ActionResult ViewReports()
        {
            List<currentshiftdata> data = new MySqlDatabaseInteraction().GetCurrentShiftData();
            UpdateCurrentShiftDataPieChart();
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



        #region generate current data report code
        [HttpPost]
        [Route("Reports/GenerateYearReport")]
        public PartialViewResult GenerateYearReport(string year)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<currentdata> i = sql.GetCurrentData(year);
            UpdatePieChart(year);
            return PartialView("_generateReportCurrentData", i);
        }

        [HttpPost]
        [Route("Reports/GenerateMonthReport")]
        public PartialViewResult GenerateMonthReport(string year, string month)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            UpdatePieChart(year, month);
            List<currentdata> i = sql.GetCurrentData(year, month);
            return PartialView("_generateReportCurrentData", i);
        }

        [HttpPost]
        [Route("Reports/GenerateDayReport")]
        public PartialViewResult GenerateDayReport(string year, string month, string day)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            UpdatePieChart(year, month, day);
            List<currentdata> i = sql.GetCurrentData(year, month, day);
            return PartialView("_generateReportCurrentData", i);
        }

        [HttpPost]
        [Route("Reports/GenerateShiftReport")]
        public ActionResult GenerateShiftReport(string year, string month, string day, string shift)
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            UpdatePieChart(year, month, day, shift);
            List<currentdata> i = sql.GetCurrentData(year, month, day, shift);
            return PartialView("_generateReportCurrentData", i);
        }

        #endregion


        //update Pie chart for yearly report
        [HttpPost]
        [Route("Reports/UpdateGraph")]
        public JsonResult UpdateGraph(string year)
        {  
            return UpdatePieChart(year);
        }

        [HttpPost]
        [Route("Reports/UpdateGraphMonth")]
        public JsonResult UpdateGraphMonth(string year,string month)
        {
            return UpdatePieChart(year,month);
        }

        [HttpPost]
        [Route("Reports/UpdateGraphDay")]
        public JsonResult UpdateGraphDay(string year, string month , string day)
        {
            return UpdatePieChart(year, month,day);
        }

        [HttpPost]
        [Route("Reports/UpdateGraphShift")]
        public JsonResult UpdateGraphShift(string year, string month, string day,string shift)
        {
            return UpdatePieChart(year, month, day,shift);
        }

        [HttpGet]
        [Route("Reports/GenerateYearlyExcelFile")]
        public FileResult GenerateYearlyExcelFile(string year)
        {

            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<currentdata> i = sql.GetCurrentData(year);
            StringBuilder csv = new StringBuilder();
            int changeCsvPageCunter = 0; // this counter is taken because I want to divide data into 5 tables in one page
            int spacing = 0;
            foreach (currentdata d in i)
            {
                csv.AppendLine(",,Yearly Report,,");
                csv.AppendLine("");

                string dateTimeAndStatusHeader = "Date," + d.date_time.Date.ToString("dd-mm-yyyy") + ", Time," + d.date_time.ToString("hh:mm tt") + ",Accepted";
                string rowOneData = "";
                string rowTwoData = "";
                string rowThreeData = "";
                string rowFourData = "";

                #region Logic for row one of csv file
                if (d.c11 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.c12 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.r1 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                rowOneData += d.w1;
                #endregion

                #region Logic for row Two of csv file
                if (d.c21 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.c22 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.r2 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                rowTwoData += d.w2;
                #endregion

                #region Logic for row Three of csv file
                if (d.c31 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.c32 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.r3 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                rowThreeData += d.w3;
                #endregion

                #region Logic for row Four of csv file
                if (d.c41 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.c42 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.r4 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                rowFourData += d.w4;
                #endregion

                string weightDifference = ",,Weight Difference," + d.wd;

                if (changeCsvPageCunter % 5 == 0)
                {
                    if (spacing % 5 == 0 & spacing > 0)
                    {
                        csv.AppendLine("");
                        csv.AppendLine("");
                        csv.AppendLine("");
                    }
                    csv.AppendLine(",,Yearly Report,,");//row 1
                    csv.AppendLine("");//row 2
                    csv.AppendLine(dateTimeAndStatusHeader); //row 3
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight"); // row4
                    csv.AppendLine(rowOneData); //row5
                    csv.AppendLine(rowTwoData);//row6
                    csv.AppendLine(rowThreeData);//row7
                    csv.AppendLine(rowFourData);//row8
                    csv.AppendLine("");//row9
                    csv.AppendLine(weightDifference);//row10
                    csv.AppendLine("");//row11
                    changeCsvPageCunter++;
                    spacing++;
                }
                else
                {
                    csv.AppendLine(dateTimeAndStatusHeader);
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight");
                    csv.AppendLine(rowOneData);
                    csv.AppendLine(rowTwoData);
                    csv.AppendLine(rowThreeData);
                    csv.AppendLine(rowFourData);
                    csv.AppendLine("");
                    csv.AppendLine(weightDifference);
                    csv.AppendLine("");// row 9
                    changeCsvPageCunter++;
                    spacing++;
                }


            }

            // byte[] fileBytes = System.IO.File.ReadAllBytes(csv);
            string fileName = "Yearly.csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv.ToString()), "text/csv", fileName);

        }


        //Generate shiftwise report
        [HttpGet]
        [Route("Reports/GenerateShiftWiseExcelFile")]
        public FileResult GenerateShiftWiseExcelFile(string year, string month, string day, string shift)
        {

            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<currentdata> i = sql.GetCurrentData(year, month, day, shift);
            StringBuilder csv = new StringBuilder();
            int changeCsvPageCunter = 0; // this counter is taken because I want to divide data into 5 tables in one page
            int spacing = 0;
            foreach (currentdata d in i)
            {
                csv.AppendLine(",,Yearly Report,,");
                csv.AppendLine("");

                string dateTimeAndStatusHeader = "Date," + d.date_time.Date.ToString("dd-mm-yyyy") + ", Time," + d.date_time.ToString("hh:mm tt") + ",Accepted";
                string rowOneData = "";
                string rowTwoData = "";
                string rowThreeData = "";
                string rowFourData = "";

                #region Logic for row one of csv file
                if (d.c11 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.c12 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.r1 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                rowOneData += d.w1;
                #endregion

                #region Logic for row Two of csv file
                if (d.c21 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.c22 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.r2 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                rowTwoData += d.w2;
                #endregion

                #region Logic for row Three of csv file
                if (d.c31 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.c32 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.r3 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                rowThreeData += d.w3;
                #endregion

                #region Logic for row Four of csv file
                if (d.c41 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.c42 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.r4 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                rowFourData += d.w4;
                #endregion

                string weightDifference = ",,Weight Difference," + d.wd;

                if (changeCsvPageCunter % 5 == 0)
                {
                    if (spacing % 5 == 0 & spacing > 0)
                    {
                        csv.AppendLine("");
                        csv.AppendLine("");
                        csv.AppendLine("");
                    }
                    csv.AppendLine(",,Yearly Report,,");//row 1
                    csv.AppendLine("");//row 2
                    csv.AppendLine(dateTimeAndStatusHeader); //row 3
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight"); // row4
                    csv.AppendLine(rowOneData); //row5
                    csv.AppendLine(rowTwoData);//row6
                    csv.AppendLine(rowThreeData);//row7
                    csv.AppendLine(rowFourData);//row8
                    csv.AppendLine("");//row9
                    csv.AppendLine(weightDifference);//row10
                    csv.AppendLine("");//row11
                    changeCsvPageCunter++;
                    spacing++;
                }
                else
                {
                    csv.AppendLine(dateTimeAndStatusHeader);
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight");
                    csv.AppendLine(rowOneData);
                    csv.AppendLine(rowTwoData);
                    csv.AppendLine(rowThreeData);
                    csv.AppendLine(rowFourData);
                    csv.AppendLine("");
                    csv.AppendLine(weightDifference);
                    csv.AppendLine("");// row 9
                    changeCsvPageCunter++;
                    spacing++;
                }


            }

            // byte[] fileBytes = System.IO.File.ReadAllBytes(csv);
            string fileName = "Yearly.csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv.ToString()), "text/csv", fileName);

        }

        //generate Monthlyreport
        [HttpGet]
        [Route("Reports/GenerateMonthlyExcelFile")]
        public FileResult GenerateMonthlyExcelFile(string year, string month)
        {

            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<currentdata> i = sql.GetCurrentData(year, month);
            StringBuilder csv = new StringBuilder();
            int changeCsvPageCunter = 0; // this counter is taken because I want to divide data into 5 tables in one page
            int spacing = 0;
            foreach (currentdata d in i)
            {
                csv.AppendLine(",,Yearly Report,,");
                csv.AppendLine("");

                string dateTimeAndStatusHeader = "Date," + d.date_time.Date.ToString("dd-mm-yyyy") + ", Time," + d.date_time.ToString("hh:mm tt") + ",Accepted";
                string rowOneData = "";
                string rowTwoData = "";
                string rowThreeData = "";
                string rowFourData = "";

                #region Logic for row one of csv file
                if (d.c11 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.c12 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.r1 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                rowOneData += d.w1;
                #endregion

                #region Logic for row Two of csv file
                if (d.c21 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.c22 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.r2 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                rowTwoData += d.w2;
                #endregion

                #region Logic for row Three of csv file
                if (d.c31 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.c32 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.r3 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                rowThreeData += d.w3;
                #endregion

                #region Logic for row Four of csv file
                if (d.c41 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.c42 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.r4 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                rowFourData += d.w4;
                #endregion

                string weightDifference = ",,Weight Difference," + d.wd;

                if (changeCsvPageCunter % 5 == 0)
                {
                    if (spacing % 5 == 0 & spacing > 0)
                    {
                        csv.AppendLine("");
                        csv.AppendLine("");
                        csv.AppendLine("");
                    }
                    csv.AppendLine(",,Yearly Report,,");//row 1
                    csv.AppendLine("");//row 2
                    csv.AppendLine(dateTimeAndStatusHeader); //row 3
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight"); // row4
                    csv.AppendLine(rowOneData); //row5
                    csv.AppendLine(rowTwoData);//row6
                    csv.AppendLine(rowThreeData);//row7
                    csv.AppendLine(rowFourData);//row8
                    csv.AppendLine("");//row9
                    csv.AppendLine(weightDifference);//row10
                    csv.AppendLine("");//row11
                    changeCsvPageCunter++;
                    spacing++;
                }
                else
                {
                    csv.AppendLine(dateTimeAndStatusHeader);
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight");
                    csv.AppendLine(rowOneData);
                    csv.AppendLine(rowTwoData);
                    csv.AppendLine(rowThreeData);
                    csv.AppendLine(rowFourData);
                    csv.AppendLine("");
                    csv.AppendLine(weightDifference);
                    csv.AppendLine("");// row 9
                    changeCsvPageCunter++;
                    spacing++;
                }


            }

            // byte[] fileBytes = System.IO.File.ReadAllBytes(csv);
            string fileName = "Yearly.csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv.ToString()), "text/csv", fileName);

        }


        //Generate daily report
        [HttpGet]
        [Route("Reports/GenerateDailyExcelFile")]
        public FileResult GenerateDailyExcelFile(string year, string month, string day)
        {

            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<currentdata> i = sql.GetCurrentData(year, month, day);
            StringBuilder csv = new StringBuilder();
            int changeCsvPageCunter = 0; // this counter is taken because I want to divide data into 5 tables in one page
            int spacing = 0;
            foreach (currentdata d in i)
            {
                csv.AppendLine(",,Yearly Report,,");
                csv.AppendLine("");

                string dateTimeAndStatusHeader = "Date," + d.date_time.Date.ToString("dd-mm-yyyy") + ", Time," + d.date_time.ToString("hh:mm tt") + ",Accepted";
                string rowOneData = "";
                string rowTwoData = "";
                string rowThreeData = "";
                string rowFourData = "";

                #region Logic for row one of csv file
                if (d.c11 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.c12 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                if (d.r1 != 0)
                {
                    rowOneData += "PRESENT,";
                }
                else
                {
                    rowOneData += "ABSENT,";
                }
                rowOneData += d.w1;
                #endregion

                #region Logic for row Two of csv file
                if (d.c21 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.c22 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                if (d.r2 != 0)
                {
                    rowTwoData += "PRESENT,";
                }
                else
                {
                    rowTwoData += "ABSENT,";
                }
                rowTwoData += d.w2;
                #endregion

                #region Logic for row Three of csv file
                if (d.c31 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.c32 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                if (d.r3 != 0)
                {
                    rowThreeData += "PRESENT,";
                }
                else
                {
                    rowThreeData += "ABSENT,";
                }
                rowThreeData += d.w3;
                #endregion

                #region Logic for row Four of csv file
                if (d.c41 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.c42 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                if (d.r4 != 0)
                {
                    rowFourData += "PRESENT,";
                }
                else
                {
                    rowFourData += "ABSENT,";
                }
                rowFourData += d.w4;
                #endregion

                string weightDifference = ",,Weight Difference," + d.wd;

                if (changeCsvPageCunter % 5 == 0)
                {
                    if (spacing % 5 == 0 & spacing > 0)
                    {
                        csv.AppendLine("");
                        csv.AppendLine("");
                        csv.AppendLine("");
                    }
                    csv.AppendLine(",,Yearly Report,,");//row 1
                    csv.AppendLine("");//row 2
                    csv.AppendLine(dateTimeAndStatusHeader); //row 3
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight"); // row4
                    csv.AppendLine(rowOneData); //row5
                    csv.AppendLine(rowTwoData);//row6
                    csv.AppendLine(rowThreeData);//row7
                    csv.AppendLine(rowFourData);//row8
                    csv.AppendLine("");//row9
                    csv.AppendLine(weightDifference);//row10
                    csv.AppendLine("");//row11
                    changeCsvPageCunter++;
                    spacing++;
                }
                else
                {
                    csv.AppendLine(dateTimeAndStatusHeader);
                    csv.AppendLine("Clip 1,Clip 2,Ring,Weight");
                    csv.AppendLine(rowOneData);
                    csv.AppendLine(rowTwoData);
                    csv.AppendLine(rowThreeData);
                    csv.AppendLine(rowFourData);
                    csv.AppendLine("");
                    csv.AppendLine(weightDifference);
                    csv.AppendLine("");// row 9
                    changeCsvPageCunter++;
                    spacing++;
                }


            }

            // byte[] fileBytes = System.IO.File.ReadAllBytes(csv);
            string fileName = "Yearly.csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv.ToString()), "text/csv", fileName);

        }


        public void UpdateCurrentShiftDataPieChart()
        {
            List<currentshiftdata> d = new MySqlDatabaseInteraction().UpdateCurrentShiftDataPieChart();
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
            ViewBag.clip1 = c1;
            ViewBag.clip2 = c2;
            ViewBag.ring = r;
            ViewBag.weight = w;
            ViewBag.TotalSuccessfulCycles = new MySqlDatabaseInteraction().SuccessfulCycleCount();
        }

        public void UpdatePieChart()
        {
            List<currentdata> d = new MySqlDatabaseInteraction().UpdatePieChart();
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
            p.Barcode = "Current Data Pie Chart";

            ViewBag.pieData = p;
        }

        public JsonResult UpdatePieChart(string year)
        {
            List<currentdata> d = new MySqlDatabaseInteraction().UpdatePieChart(year);
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
            
            p.Barcode = "Current Data Yearly Report Pie Chart";

            ViewBag.pieData = p;

            //var pieDataJson = JsonConvert.SerializeObject(p);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePieChart(string year, string month)
        {
            List<currentdata> d = new MySqlDatabaseInteraction().UpdatePieChart(year, month);
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

            p.Barcode = "Current Data Monthly Report Pie Chart";

            ViewBag.pieData = p;

            //var pieDataJson = JsonConvert.SerializeObject(p);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePieChart(string year, string month, string day)
        {
            List<currentdata> d = new MySqlDatabaseInteraction().UpdatePieChart(year, month, day);
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

            p.Barcode = "Current Data Day Report Pie Chart";

            ViewBag.pieData = p;

            //var pieDataJson = JsonConvert.SerializeObject(p);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePieChart(string year, string month, string day, string shift)
        {
            List<currentdata> d = new MySqlDatabaseInteraction().UpdatePieChart(year, month, day, shift);
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

            p.Barcode = "Current Data Shift Wise Report Pie Chart";

            ViewBag.pieData = p;

            //var pieDataJson = JsonConvert.SerializeObject(p);
            return Json(p, JsonRequestBehavior.AllowGet);
        }
    }
}