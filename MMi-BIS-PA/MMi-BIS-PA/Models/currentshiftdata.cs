namespace MMi_BIS_PA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_mmi_bis_pa.currentshiftdata")]
    public partial class currentshiftdata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long unicode { get; set; }

        [Required]
        [StringLength(45)]
        public string qr1 { get; set; }

        public sbyte? c11 { get; set; }

        public sbyte? c12 { get; set; }

        public sbyte? r1 { get; set; }

        public float? w1 { get; set; }

        [Required]
        [StringLength(45)]
        public string qr2 { get; set; }

        public sbyte? c21 { get; set; }

        public sbyte? c22 { get; set; }

        public sbyte? r2 { get; set; }

        public float? w2 { get; set; }

        [Required]
        [StringLength(45)]
        public string qr3 { get; set; }

        public sbyte? c31 { get; set; }

        public sbyte? c32 { get; set; }

        public sbyte? r3 { get; set; }

        public float? w3 { get; set; }

        [Required]
        [StringLength(45)]
        public string qr4 { get; set; }

        public sbyte? c41 { get; set; }

        public sbyte? c42 { get; set; }

        public sbyte? r4 { get; set; }

        public float? w4 { get; set; }

        public float? wd { get; set; }

        public sbyte? status { get; set; }

        public int shiftid { get; set; }

        public DateTime date_time { get; set; }
    }
}
