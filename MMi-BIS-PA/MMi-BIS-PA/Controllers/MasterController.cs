using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;

namespace MMi_BIS_PA.Controllers
{
    public class MasterController : Controller
    {

        private DB_Model db;

        private MySqlDatabaseInteraction sql;

        public MasterController()
        {
            db = new DB_Model();
            sql = new MySqlDatabaseInteraction();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        // GET: Master
        public ActionResult Master()
        {
            master_table master = sql.GetMasterData();
            return View(master);
        }

       
       

        [HttpPost]
        public ActionResult AddMaster(master_table data )
        {
        
            db.master_table.RemoveRange(db.master_table);
            db.master_table.Add(data);
            db.SaveChanges();
            return RedirectToAction("Master", "Master");
        }
    }

}