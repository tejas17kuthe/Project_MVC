using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MMi_BIS_PA.Models
{
    [Table("db_mmi_bis_pa.currentdata")]
    public class barcodedata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idBarcodeData { get; set; }

        public string BarcodeData { get; set; }
    }
}