namespace MMi_BIS_PA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_mmi_bis_pa.user_info")]
    public partial class user_info
    {
        [Required]
        [StringLength(45)]
        public string username { get; set; }

        [StringLength(45)]
        public string password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
    }
}
