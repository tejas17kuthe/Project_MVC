using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMi_BIS_PA.App_Data;
using System.Web.Mvc;


namespace MMi_BIS_PA.Models
{
    public class MySqlDatabaseInteraction
    {
        public void SetUserNamePassword(int id, string userName, string password)
        {
            DB_Model db = new DB_Model();

            db.user_info.Find();
            
        }

        public Boolean AuthenticateUser(string username,string password) {
            bool result;
            
           using ( DB_Model dbConnect = new DB_Model())
           {
                string query = "SELECT * FROM db_mmi_bis_pa.user_info WHERE username =\"" + username + "\" and password =\"" + password+"\"";
                var count = dbConnect.user_info.SqlQuery(query).ToList();

                if (count.Count() > 0)
                {
                    return true;
                }
                else
                    return false;
            }

           

        }
     }
}