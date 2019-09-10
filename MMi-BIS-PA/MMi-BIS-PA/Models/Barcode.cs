using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMi_BIS_PA.Models
{
    public class Barcode
    {
        public string Data { get; set; }
        public int CurrentShiftTotalJobCount { get; set; }
        public float weightSetPoint { get; set; }
    }
}