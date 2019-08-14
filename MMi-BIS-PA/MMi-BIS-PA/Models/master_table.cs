using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace MMi_BIS_PA.Models
{
    
    [Table("db_mmi_bis_pa.master_table")]
    public partial class master_table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string IP_Address { get; set; }

        public float Weight_Diffrence { get; set; }

        public int Barcode_Length  { get; set; }
    }
}
