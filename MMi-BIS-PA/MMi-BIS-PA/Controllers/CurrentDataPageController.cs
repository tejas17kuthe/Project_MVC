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
        CCoreScannerClass ccs;
        static string barcodeData;
        LoginData id;
        ProcessStartInfo myProcessStartInfo;
        PiechartData p;
        CycleStatusCount cycle;
        Barcode b;
        int i;
        string barcode = "";
        int barcodeEventCallCount;
        int barcodeEventCallCount2;
        List<string> scannedBarcodeList;
        bool initializeFlag;
        int tableCount;
        Process myProcess;
        DateAndTime d;
        MySqlDatabaseInteraction sql;
        currentdata c;
        currentshiftdata c2;
        public CurrentDataPageController()
        {
            p = new PiechartData();
            b = new Barcode();
            cycle = new CycleStatusCount();
            myProcess = new Process();
            d = new DateAndTime();
            sql = new MySqlDatabaseInteraction();
            c = new currentdata();
            c2 = new currentshiftdata();
        }

        [Route("CurrentDataPage/CurrentDataPage")]
        [HttpGet]
        public ActionResult CurrentDataPage(LoginData id)
        {
            InitializeBarcodeReader();
            string python = @"C:\ProgramData\Anaconda3\python.exe";
            myProcessStartInfo = new ProcessStartInfo(python);
           
            i = 0;
            scannedBarcodeList = new List<string>();
            barcodeEventCallCount = 0;
            initializeFlag = true;
            barcode = "initial QR Code";
            UpdatePieChart();
            this.id = id;
            barcodeData = "Please Scan the QR Code";
            b.Data = barcodeData;

            //b.Data = "No Barcode Scanned";
            //ViewBag.Barcode = b;
            //ViewBag.userName = this.id.userName;
            //ViewBag.password = this.id.password;
            return View();
        }

        [HttpPost]
        public ActionResult CurrentDataPage(string qr)
        {
            InitializeBarcodeReader();
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
        public PartialViewResult Delete()
        {
            barcodeEventCallCount2 = 0;
            InitializeBarcodeReader();
            myProcess.Close();
            barcodeData = "Please Scan the QR Code";
            ViewBag.QrCode = barcodeData;
            sql.RemoveTableData();
            return UpdateTable();
        }


        public void RemoveTableData()
        {
            MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            if(i.Count == 4)
            {
                sql.RemoveTableData();
            }
        }

        public void AddDataIntoCurrentTable()
        {
            //MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();

            List<TableData> i = sql.GetTableData();
            ViewBag.Barcode = ++this.i;
            if (i.Count == 4)
            {
                string unicode = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.ToLocalTime().ToString("HH") + DateTime.Now.ToLocalTime().ToString("mm") + DateTime.Now.ToLocalTime().ToString("ss");
                #region Creating CurrentData object


                //row 1 data collection
                c.unicode = Convert.ToInt64(unicode);
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

                if (c.c11 != 0 && c.c12 != 0 && c.r1 != 0 && c.c21 != 0 && c.c22 != 0 && c.r2 != 0 && c.c31 != 0 && c.c32 != 0 && c.r3 != 0 && c.c41 != 0 && c.c42 != 0 && c.r4 != 0 && c.wd <= i[3].set_point)
                {
                    c.status = 1;
                }
                else
                {
                    c.status = 0;
                }

                var shift = sql.getshiftid(DateTime.Now.ToString("HH:mm:ss")).ToList();

                c.shiftid = shift[0].shift_id;
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string time = DateTime.Now.ToString("HH:mm:ss");
                string dateT = date + " " + time;
                c.date_time = DateTime.Parse(dateT);
                #endregion

                #region currentShiftData
                //currentshiftdata c2 = new currentshiftdata();

                //row 1 data collection
                c2.unicode = c.unicode;
                c2.qr1 = i[0].qrcode;
                c2.c11 = i[0].c1;
                c2.c12 = i[0].c2;
                c2.r1 = i[0].r;
                c2.w1 = i[0].w;

                //row 2 data collection
                c2.qr2 = i[1].qrcode;
                c2.c21 = i[1].c1;
                c2.c22 = i[1].c2;
                c2.r2 = i[1].r;
                c2.w2 = i[1].w;

                //row 3 data collection
                c2.qr3 = i[2].qrcode;
                c2.c31 = i[2].c1;
                c2.c32 = i[2].c2;
                c2.r3 = i[2].r;
                c2.w3 = i[2].w;

                //row 4 data collection
                c2.qr4 = i[3].qrcode;
                c2.c41 = i[3].c1;
                c2.c42 = i[3].c2;
                c2.r4 = i[3].r;
                c2.w4 = i[3].w;

                c2.wd = i[3].wd;

                if (c2.c11 != 0 && c2.c12 != 0 && c2.r1 != 0 && c2.c21 != 0 && c2.c22 != 0 && c2.r2 != 0 && c2.c31 != 0 && c2.c32 != 0 && c2.r3 != 0 && c2.c41 != 0 && c2.c42 != 0 && c2.r4 != 0 && c2.wd <= i[3].set_point)
                {
                    c2.status = 1;
                }
                else
                {
                    c2.status = 0;
                }

                var currentShift = sql.getshiftid(DateTime.Now.ToString("HH:mm:ss")).ToList();

                c2.shiftid = shift[0].shift_id;
                c2.date_time = DateTime.Parse(dateT);
                #endregion


                if (barcodeEventCallCount == 1)
                {
                    barcodeEventCallCount = 0;
                    //float setpoint = (float)i[0].set_point;
                    //new MySqlDatabaseInteraction().UpdateWeightDifferenceSetPoint(setpoint);
                    sql.AddCurrentData(c);
                    sql.AddCurrentShiftData(c2,shift[0].shift_id);
                    
                    
                    scannedBarcodeList.Clear();
                }
            }

        }

        public PartialViewResult UpdateBarcode()
        {
            b.Data = barcodeData;
            ViewBag.Barcode = b;
            return PartialView("_SearchBar");
        }


        [HttpPost]
        [Route("CurrentDataPage/UpdateTotalCurrentShiftJobCount")]
        public JsonResult UpdateTotalCurrentShiftJobCount()
        {

            cycle.Total = sql.CurrentShiftTotalJobCount();
            cycle.Accepted = sql.GetCurrentShiftAcceptedData().Count;
            cycle.Rejected = sql.GetCurrentShiftRejectedData().Count;

            ViewBag.TotalCurrentShiftJobDone = cycle;
            return Json(cycle, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Route("CurrentDataPage/Date_Time")]
        public JsonResult Date_Time()
        {

            d.Date = DateTime.Now.Date.ToShortDateString();
            d.Time = DateTime.Now.ToString("hh:mm tt");

            ViewBag.TotalCurrentShiftJobDone = cycle;
            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult UpdateTable()
        {
            //MySqlDatabaseInteraction sql = new MySqlDatabaseInteraction();
            List<TableData> i = sql.GetTableData();
            tableCount = i.Count;
            UpdatePieChart();
            return PartialView("_DataCardContent", i);
        }



        public void UpdatePieChart()
        {
            List<currentshiftdata> d = sql.UpdateCurrentShiftDataPieChart();
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
            p.TotalSuccessfulCycles = sql.GetCurrentShiftAcceptedData().Count;
            p.Barcode = barcodeData;

            ViewBag.pieData = p;

        }


        private void InitializeBarcodeReader()
        {

            //Instantiate CoreScanner Class
            ccs = new CCoreScannerClass();
            //Call Open API
            short[] scannerTypes = new short[1];//Scanner Types you are interested in
            scannerTypes[0] = 1; // 1 for all scanner types
            short numberOfScannerTypes = 1; // Size of the scannerTypes array
            int status; // Extended API return code
            ccs.Open(0, scannerTypes, numberOfScannerTypes, out status);
            // Subscribe for barcode events in cCoreScannerClass
            ccs.BarcodeEvent += new _ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
            // Let's subscribe for events
            int opcode = 1001; // Method for Subscribe events
            string outXML; // XML Output
            string inXML = "<inArgs>" +
             "<cmdArgs>" +
             "<arg-int>1</arg-int>" + // Number of events you want to subscribe
             "<arg-int>1</arg-int>" + // Comma separated event IDs
             "</cmdArgs>" +
             "</inArgs>";
            ccs.ExecCommand(opcode, ref inXML, out outXML, out status);
            string text = "initialized " + status.ToString();
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\logs\mahindra.txt"))
            //{
            //    file.WriteLine(text);
            //}

            //System.IO.File.WriteAllText(@"E:\logs\mahindra.txt", text);



        }

        public void OnBarcodeEvent(short eventType, ref string pscanData)
        {

            ++barcodeEventCallCount2;
            string hashcode = "";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pscanData);

            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("datalabel");
            if (elemList.Count > 0)
            {
                for (int i = 0; i < elemList.Count; i++)
                {
                    hashcode = elemList[i].InnerXml;
                }
            }
            string temp = getbarcode(hashcode);
            //if (tableCount < 1)
            //{
            //    scannedBarcodeList.Clear();

            //}

            //if (tableCount == 1)
            //{
            //    scannedBarcodeList.Clear();
            //    scannedBarcodeList.Add(barcodeData);
            //}

            bool countFlag = temp.Equals(barcode);

            if (!countFlag)
            {
                if (barcodeEventCallCount2 == 1)
                {
                    barcodeEventCallCount2++;//to prevent multiple internal barcode event calls
                    barcodeEventCallCount = 1;
                    barcodeData = temp;
                    //scannedBarcodeList.Add(barcodeData);
                    barcode = barcodeData;
                    RemoveTableData();
                    CallPythonDriver(barcodeData);
                    AddDataIntoCurrentTable();
                }
                else
                {
                    //barcodeData = "Driver is running Please wait";
                }
            }
            else
            {
                if(barcodeEventCallCount2==1)
                {
                    barcodeData = "same";
                }
                //Console.WriteLine("Same Barcode Scanned or callCount="+barcodeEventCallCount2);
                barcodeEventCallCount2 = 0;
                
            }
            //if (barcodeEventCallCount == 1)
            //{

            //    //b.Data = barcode;
            //    //ViewBag.Barcode = b;
            //    string textbar = "Barcode " + barcodeData;
            //    bool flag = barcodeData.Equals(barcode);
            //    if (!flag)
            //    {

            //    }

            //}

            //this.Invoke((MethodInvoker)delegate { txtBarcode.Text = barcode; });
        }

        string getbarcode(string b)
        {
            try
            {
                string hex = string.Empty;
                string ascii = string.Empty;

                for (int i = 0; i < b.Length; i += 4)
                {
                    String hs = string.Empty;

                    hs = b.Substring(i, 4);

                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;
                    i++;
                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }







        private void CallPythonDriver(string qr)
        {
            if (barcodeEventCallCount == 1)
            {
                try
                {

                    string python = @"C:\ProgramData\Anaconda3\python.exe";

                    // python app to call 
                    string myPythonApp = "C:\\ProgramData\\Anaconda3\\driver.py";

                    // dummy parameters to send Python script 
                    string x = qr.ToString();


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

                    
                    // assign start information to the process 
                    myProcess.StartInfo = myProcessStartInfo;

                    Console.WriteLine("Calling Python script with arguments {0}", x);
                    // start the process 
                    myProcess.Start();
                    myProcess.WaitForExit();
                    myProcess.Close();

                   
                    barcodeEventCallCount2 = 0;
                    barcodeData = "Please Scan the QR Code";
                    // }

                }
                catch (Exception e)
                {
                    // return Content("There is some problem with the driver please check connection");
                }
            }
        }
    }
}

