namespace MMi_BIS_PA.App_Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_mmi_bis_pa.shift_details")]
    public partial class shift_details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int shift_id { get; set; }

        public TimeSpan start_time { get; set; }

        public TimeSpan end_time { get; set; }
    }
}
