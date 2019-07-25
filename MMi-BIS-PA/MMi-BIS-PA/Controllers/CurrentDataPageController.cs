using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMi_BIS_PA.Models;

namespace MMi_BIS_PA.Controllers
{
    public class CurrentDataPageController : Controller
    {
        [Route("CurrentDataPage/CurrentDataPage")]
        public ActionResult CurrentDataPage()
        {
            int[] i = new ModbusTCP_IP().GetValueFromPLC();

            TableData tblData = new TableData();
            if (i[0] == 1)
            {
                tblData.clip1 = true;
            }
            else
                tblData.clip1 = false;

             if (i[1] == 1)
            {
                tblData.clip2 = true;
            }
            else
                tblData.clip2 = false;

            if (i[2] == 1)
            {
                tblData.ring = true;
            }
            else
                tblData.ring = false;

            string temp = i[3] + "." + i[4];

            tblData.weight = float.Parse(temp);




            return View(tblData);

        }
    }
}