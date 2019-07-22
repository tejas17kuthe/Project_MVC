namespace MMi_BIS_PA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_mmi_bis_pa.current_data")]
    public partial class current_data
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int unicode { get; set; }

        [Required]
        [StringLength(45)]
        public string Qrcode { get; set; }

        [Column("1c1")]
        public sbyte? C1c1 { get; set; }

        [Column("1c2")]
        public sbyte? C1c2 { get; set; }

        [Column("1r")]
        public sbyte? C1r { get; set; }

        [Column("1w")]
        public float? C1w { get; set; }

        [Column("2c1")]
        public sbyte? C2c1 { get; set; }

        [Column("2c2")]
        public sbyte? C2c2 { get; set; }

        [Column("2r")]
        public sbyte? C2r { get; set; }

        [Column("2w")]
        public float? C2w { get; set; }

        [Column("3c1")]
        public sbyte? C3c1 { get; set; }

        [Column("3c2")]
        public sbyte? C3c2 { get; set; }

        [Column("3r")]
        public sbyte? C3r { get; set; }

        [Column("3w")]
        public float? C3w { get; set; }

        [Column("4c1")]
        public sbyte? C4c1 { get; set; }

        [Column("4c2")]
        public sbyte? C4c2 { get; set; }

        [Column("4r")]
        public sbyte? C4r { get; set; }

        [Column("4w")]
        public float? C4w { get; set; }

        public float? weight_diff { get; set; }

        public sbyte? status { get; set; }
    }
}
