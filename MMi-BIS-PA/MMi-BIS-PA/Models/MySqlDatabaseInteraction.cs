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


            using (DB_Model dbConnect = new DB_Model())
            {
                string query = "SELECT * FROM db_mmi_bis_pa.userinfo WHERE username =\"" + username + "\" and password =\"" + password + "\"";
                var count = dbConnect.userinfoes.SqlQuery(query).ToList();

                if (count.Count() > 0)
                {
                    return true;
                }
                else
                    return false;
            }



            public Boolean AddCurrentData
            using (DB_Model db = new DB_Model())
            {
               
                
            }


        }





    }
}