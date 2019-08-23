using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace MMi_BIS_PA.Models
{
    [Table("db_mmi_bis_pa.table_data")]
    public partial class TableData
    {
        [Required]
        [Key]
        [StringLength(45)]
        public string qrcode { get; set; }

        public sbyte? c1 { get; set; }

        public sbyte? c2 { get; set; }

        public sbyte? r { get; set; }

        public float? w { get; set; }

        public float? wd { get; set; }

        public float? set_point { get; set; }

        public int? job_count { get; set; }

        public int? set_count { get; set; }
    }
}