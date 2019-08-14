namespace MMi_BIS_PA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_mmi_bis_pa.userinfo")]
    public partial class userinfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userid { get; set; }

        [StringLength(45)]
        public string username { get; set; }

        [StringLength(45)]
        public string password { get; set; }
    }
}
