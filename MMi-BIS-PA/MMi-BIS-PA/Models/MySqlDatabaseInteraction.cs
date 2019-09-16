using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMi_BIS_PA.Models;
using System.Web.Mvc;


namespace MMi_BIS_PA.Models
{
    public class MySqlDatabaseInteraction
    {


        public Boolean AuthenticateUser(string username, string password)
        {

            try
            {
                using (DB_Model dbConnect = new DB_Model())
                {
                    string query = "SELECT * FROM db_mmi_bis_pa.userinfo WHERE username =\"" + username + "\" and password =\"" + password + "\"";
                    var count = dbConnect.userinfoes.SqlQuery(query).ToList();
                    if (count.Count == 1)
                        return true;
                    else
                        return false;


                }
            }

            catch (Exception e)
            {
                return false;
            }

        }

        public bool AddCurrentData(currentdata currentdata)
        {

            using (DB_Model db = new DB_Model())
            {

                db.currentdatas.Add(currentdata);
                db.SaveChanges();

                return true;


            }

        }



        public bool RemoveTableData()
        {

            try
            {
                using (DB_Model db = new DB_Model())
                {
                    db.tableData.RemoveRange(db.tableData);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }



        }


        public bool RemoveCurrentShiftData()
        {
            try
            {
                using (DB_Model db = new DB_Model())
                {
                    db.currentshiftdatas.RemoveRange(db.currentshiftdatas);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }



        }

        public bool IfDateTimeIsSame(DateTime date)
        {
            try
            {
                using (DB_Model db = new DB_Model())
                {
                    string query = "select * from db_mmi_bis_pa.currentshiftdata where day(date_time)!=" + date.Day;

                    var d = db.currentshiftdatas.SqlQuery(query).ToList();

                    if (d.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {

                return false;
            }
        }

        public bool AddCurrentShiftData(currentshiftdata data, int shift)
        {
            //modification needed please check current shift status if the shift have been changed please clean the data and then add new data.

            try
            {
                using (DB_Model db = new DB_Model())
                {
                    if (shift == GetCurrentShiftDataShiftID())
                    {
                        db.currentshiftdatas.Add(data);
                    }
                    else
                    {
                        RemoveCurrentShiftData();
                        db.currentshiftdatas.Add(data);
                    }
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {

                return false;
            }

        }



        public bool AddUserInfo(userinfo data)
        {

            try
            {
                using (DB_Model db = new DB_Model())
                {

                    db.userinfoes.Add(data);

                    db.SaveChanges();

                    return true;


                }
            }
            catch
            {

                return false;
            }



        }

        public bool AddShiftData(shiftinfo data)
        {

            try
            {
                using (DB_Model db = new DB_Model())
                {

                    db.shiftinfoes.Add(data);

                    db.SaveChanges();

                    return true;

                }
            }
            catch
            {
                return false;
            }


        }


        #region retrieving data from the database

        public int GetCurrentShiftTotalJobCount()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata group by shiftid";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data.Count;
            }
        }

        public int GetCurrentShiftDataShiftID()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata group by shiftid";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                int shift = -1;
                foreach (var d in data)
                {
                    shift = d.shiftid;
                }
                return shift;
            }
        }

        public List<currentshiftdata> GetCurrentShiftData()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata order by time(date_time) desc";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentshiftdata> GetCurrentShiftAcceptedData()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata where status=1";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentshiftdata> GetCurrentShiftRejectedData()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata where status=0";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data;
            }
        }
        public List<currentdata> GetCurrentData()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata";
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        //function is used in yearly report generation 
        public List<currentdata> GetCurrentData(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " order by date_time desc";
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentDataAccepted(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=1 and year(date_time)=" + year;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentDataRejected(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=0 and year(date_time)=" + year;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }
        public List<string> GetPieChart(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string failure = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " status=0";   //Query for failure records
                var failureData = db.currentdatas.SqlQuery(failure).ToList();
                string success = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " status=0";   //Query for success records
                var successData = db.currentdatas.SqlQuery(success).ToList();
                List<string> data = new List<string>();

                int clip1 = 0;
                int clip2 = 0;
                int ring = 0;
                int weight = 0;

                //foreach (var d in failureData)
                //{
                //    d.
                //}
                return data;
            }
        }

        public List<currentdata> GetCurrentData(string year, string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " and month(date_time)=" + month+ " order by date_time desc";
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        #region monthly accepted rejected count
        public List<currentdata> GetCurrentDataAccepted(string year, string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=1 and year(date_time)=" + year + " and month(date_time)=" + month;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentDataRejected(string year, string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=0 and year(date_time)=" + year + " and month(date_time)=" + month;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }
        #endregion

        public List<currentdata> GetCurrentData(string year, string month, string day)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day + " order by date_time desc";
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        #region currentdata dayly accepted and rejected data
        public List<currentdata> GetCurrentDataAccepted(string year, string month, string day)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status =1 and year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentDataRejected(string year, string month, string day)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status = 0 and year(date_time)= " + year + " and month(date_time)= " + month + " and day(date_time)= " + day;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }
        #endregion

        public List<currentdata> GetCurrentData(string year, string month, string day, string shift)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day + " and shiftid=" + shift + " order by date_time desc";
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }


        public List<currentdata> GetCurrentDataAccepted(string year, string month, string day, string shift)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=1 and year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day + " and shiftid=" + shift;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentDataRejected(string year, string month, string day, string shift)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=0 and year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day + " and shiftid=" + shift;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }



        public List<TableData> GetTableData()
        {


            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.table_data order by job_count asc";

                var data = db.tableData.SqlQuery(query).ToList();


                return data;


            }


        }

        public List<int> GetYear()
        {


            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata group by year(date_time)";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> year = new List<int>();
                foreach (var d in data)
                {
                    year.Add(d.date_time.Year);
                }
                return year;


            }


        }


        public List<int> GetMonth(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata where year(date_time) =" + year + " group by month(date_time)";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> Month = new List<int>();
                foreach (var d in data)
                {
                    Month.Add(d.date_time.Month);
                }
                return Month;
            }
        }

        //public string GetBarcodeData()
        //{
        //    using (DB_Model db = new DB_Model())
        //    {
        //        string query = "SELECT * FROM db_mmi_bis_pa.barcodedata where idBarcodeData=01";

        //        List<barcodedata> data = db.barcodedata.SqlQuery(query).ToList();
                
        //        foreach (var d in data)
        //        {
        //            return d.BarcodeData.ToString(); 
        //        }
              
        //    }
        //}

        public List<int> GetDate(string year, string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata where year(date_time) =" + year + " and month(date_time)=" + month + " group by date(date_time)";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> Date = new List<int>();
                foreach (var d in data)
                {
                    Date.Add(d.date_time.Day);
                }
                return Date;
            }
        }


        public List<int> GetShift(string year, string month, string date)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata where year(date_time) =" + year + " and month(date_time)=" + month + " and day(date_time)=" + date + " group by shiftid";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> shift = new List<int>();
                foreach (var d in data)
                {
                    shift.Add(d.shiftid);
                }
                return shift;
            }
        }



        public master_table GetMasterData()
        {


            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.master_table";
                master_table master = new master_table();
                List<master_table> data = db.master_table.SqlQuery(query).ToList();
                foreach (var d in data)
                {
                    master.Id = d.Id;
                    master.IP_Address = d.IP_Address;
                    master.Weight_Diffrence = d.Weight_Diffrence;
                    master.Barcode_Length = d.Barcode_Length;

                }
                return master;



            }


        }

        public List<shiftinfo> getshiftid(string time)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.shiftinfo where\'" + time + "\' between start_time and end_time";

                List<shiftinfo> data = db.shiftinfoes.SqlQuery(query).ToList();

                return data;

            }
        }

        //public List<DateTime> getdatetime()
        //{
        //    using (DB_Model db = new DB_Model())
        //    {
        //        string query = "SELECT now()";

        //        List<DateTime> data = db.tableData.;

        //        return data;

        //    }
        //}

        #region pieChart update

        public List<currentshiftdata> UpdateCurrentShiftDataPieChart()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata where status=0";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> UpdatePieChart()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata where status=0";
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public int SuccessfulCycleCount()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=1 ";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data.Count;
            }
        }

        public int SuccessfulCycleCount(string year,string month, string day)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=1 and year(date_time) =" + year+ " month(date_time) ="+month+ " day(date_time) ="+day;
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data.Count;
            }
        }
        public int SuccessfulCycleCount(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=1 and year(date_time) =" + year;
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data.Count;
            }
        }

        public int CurrentShiftTotalJobCount()
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentshiftdata";
                var data = db.currentshiftdatas.SqlQuery(query).ToList();
                return data.Count;
            }
        }
        public List<currentdata> UpdatePieChart(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=0 and year(date_time) =" + year;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> UpdatePieChart(string year, string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=0 and year(date_time)=" + year + " and month(date_time)=" + month;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> UpdatePieChart(string year, string month, string day)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status=0 and year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> UpdatePieChart(string year, string month, string day, string shift)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where status =0 and year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day + " and shiftid=" + shift;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public float GetWeightDifferenceSetPoint()
        {
            using (DB_Model db = new DB_Model())
            {
                master_table m = GetMasterData();
                return m.Weight_Diffrence;
            }
        }

        public void UpdateWeightDifferenceSetPoint(float setpoint)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "UPDATE `db_mmi_bis_pa`.`master_table` SET `Weight_diffrence` = " + setpoint + " WHERE (`Id` = '2')";
                var data = db.master_table.SqlQuery(query).ToList();

            }
        }
        #endregion

        #endregion
    }
}