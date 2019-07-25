using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMi_BIS_PA.Models
{
    public class TableData
    {
        public bool qrcode { get; set; }
        public bool clip1 { get; set; }
        public bool clip2 { get; set; }
        public bool ring { get; set; }
        public float weight { get; set; }
        public static int rowCount {get; set;}
    }
}