using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return PartialView("_DataCard", i);


        }


    }




}
