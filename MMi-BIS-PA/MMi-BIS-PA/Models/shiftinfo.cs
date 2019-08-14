namespace MMi_BIS_PA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_mmi_bis_pa.shiftinfo")]
    public partial class shiftinfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int shift_id { get; set; }

        public TimeSpan start_time { get; set; }

        public TimeSpan end_time { get; set; }
    }
}
