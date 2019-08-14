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
        // GET: Master
        public ActionResult Master()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddMaster(master_table data )
        {
            DB_Model db = new DB_Model();
            db.master_table.Add(data);
            db.SaveChanges();
            return RedirectToAction("Master", "Master");
        }
    }

}