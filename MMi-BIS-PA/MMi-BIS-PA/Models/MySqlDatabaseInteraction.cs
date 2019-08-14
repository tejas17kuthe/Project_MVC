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

            catch
            {
                return false;
            }

        }

        public bool AddCurrentData(currentdata currentdata)
        {

            try {    
                    using (DB_Model db = new DB_Model())
                    {

                        db.currentdatas.Add(currentdata);

                        db.SaveChanges();

                        return true;             

                
                    }
            }
            catch
            {
                
                return false;
            }

        }

        public bool AddCurrentShiftData(currentshiftdata data)
        {
            //modification needed please check current shift status if the shift have been changed please clean the data and then add new data.

            try
            {
                using (DB_Model db = new DB_Model())
                {

                    db.currentshiftdatas.Add(data);

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
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)="+year;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<string> GetPieChart(string year)
        {
            using (DB_Model db = new DB_Model())
            {
                string failure = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year+ " status=0";   //Query for failure records
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

        public List<currentdata> GetCurrentData(string year,string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year +" and month(date_time)="+month;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentData(string year, string month,string day)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " and month(date_time)=" + month+" and day(date_time)="+day;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }

        public List<currentdata> GetCurrentData(string year, string month, string day,string shift)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.currentdata where year(date_time)=" + year + " and month(date_time)=" + month + " and day(date_time)=" + day+" and shiftid="+shift;
                var data = db.currentdatas.SqlQuery(query).ToList();
                return data;
            }
        }


        public List<TableData> GetTableData()
        {


            using (DB_Model db = new DB_Model())
            {
                string query = "select * from db_mmi_bis_pa.table_data";

                var data = db.tableData.SqlQuery(query).ToList();


                return data;


            }


        }

        public List<int> GetYear()
        {


            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata group by year(date_time)" ;
                
                List<currentdata> data= db.currentdatas.SqlQuery(query).ToList();
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
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata where year(date_time) =" + year +" group by month(date_time)";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> Month = new List<int>();
                foreach (var d in data)
                {
                    Month.Add(d.date_time.Month);
                }
                return Month;
            }
        }


        public List<int> GetDate(string year,string month)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata where year(date_time) =" + year + " and month(date_time)="+ month+   " group by month(date_time)";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> Date = new List<int>();
                foreach (var d in data)
                {
                    Date.Add(d.date_time.Day);
                }
                return Date;
            }
        }


        public List<int> GetShift(string year, string month,string date)
        {
            using (DB_Model db = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.currentdata where year(date_time) =" + year + " and month(date_time)="+month + " and day(date_time)="+date+" group by shiftid";

                List<currentdata> data = db.currentdatas.SqlQuery(query).ToList();
                List<int> shift = new List<int>();
                foreach (var d in data)
                {
                    shift.Add(d.shiftid);
                }
                return shift;
            }
        }

        #endregion
    }
}