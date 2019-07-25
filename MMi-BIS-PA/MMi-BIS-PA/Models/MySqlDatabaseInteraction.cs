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
                    
                        return true;
                    
                     
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

    }
}