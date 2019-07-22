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
     }
}