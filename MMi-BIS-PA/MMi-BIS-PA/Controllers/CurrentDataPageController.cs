using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;

namespace MMi_BIS_PA.Controllers
{
    public class CurrentDataPageController : Controller
    {
        [Route("CurrentDataPage/CurrentDataPage")]
        [HttpGet]
        public ActionResult CurrentDataPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CurrentDataPage(string qr)
        {

            string fname1 = qr;


            string python = @"C:\ProgramData\Anaconda3\python.exe";

            // python app to call 
            string myPythonApp = "C:\\ProgramData\\Anaconda3\\driver.py";

            // dummy parameters to send Python script 
            string x = @fname1;


            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);
            myProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcessStartInfo.CreateNoWindow = true;


            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st arguments is pointer to itself,  
            // 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = myPythonApp + " " + x;

            Process myProcess = new Process();
            // assign start information to the process 
            myProcess.StartInfo = myProcessStartInfo;

            Console.WriteLine("Calling Python script with arguments {0} ", x);
            // start the process 
            myProcess.Start();
            myProcess.WaitForExit();
            myProcess.Close();

            return View();

        }

        [Route("CurrentDataPage/Delete")]
        public ActionResult Delete()
        {

            DB_Model db = new DB_Model();
            db.tableData.RemoveRange(db.tableData);
            db.SaveChanges();
            return RedirectToAction("CurrentDataPage", "CurrentDataPage");

        }

        public PartialViewResult UpdateTable()
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            if (i.Count == 4)
            {
                currentdata c = new currentdata();
                //row 1 data collection
                c.qr1 = i[0].qrcode;
                c.c11 = i[0].c1;
                c.c12 = i[0].c2;
                c.r1 = i[0].r;
                c.w1 = i[0].w;

                //row 2 data collection
                c.qr2 = i[1].qrcode;
                c.c21 = i[1].c1;
                c.c22 = i[1].c2;
                c.r2 = i[1].r;
                c.w2 = i[1].w;

                //row 3 data collection
                c.qr3 = i[2].qrcode;
                c.c31 = i[2].c1;
                c.c32 = i[2].c2;
                c.r3 = i[2].r;
                c.w3 = i[2].w;

                //row 4 data collection
                c.qr4 = i[3].qrcode;
                c.c41 = i[3].c1;
                c.c42 = i[3].c2;
                c.r4 = i[3].r;
                c.w4 = i[3].w;

                c.wd = i[3].wd;

                if (c.c11 != 0 && c.c12 != 0 && c.r1 != 0 && c.c21 != 0 && c.c22 != 0 && c.r2 != 0 && c.c31 != 0 && c.c32 != 0 && c.r3 != 0 && c.c41 != 0 && c.c42 != 0 && c.r4 != 0 || c.wd < i[3].set_point)
                {
                    c.status = 1;
                }
                else
                {
                    c.status = 0;
                }

                var shift = new MySqlDatabaseInteraction().getshiftid(DateTime.Now.ToString("HH:mm:ss")).ToList();

                c.shiftid = shift[0].shift_id;
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string time = DateTime.Now.ToString("HH:mm:ss");
                string dateT = date + " " + time;
                c.date_time = DateTime.Parse(dateT);
                
                new MySqlDatabaseInteraction().AddCurrentData(c);
                //if (new MySqlDatabaseInteraction().AddCurrentData(c))
                //{s                //    bool t = true;
                //}
                //else
                //{
                //    bool t2 = false;
                //}
            }
            return PartialView("_DataCard", i);





        }




    }
}
