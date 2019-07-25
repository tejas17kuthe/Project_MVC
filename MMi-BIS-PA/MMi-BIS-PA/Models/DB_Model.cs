namespace MMi_BIS_PA.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB_Model : DbContext
    {
        public DB_Model()
            : base("name=DB_Model1")
        {
        }

        public virtual DbSet<currentdata> currentdatas { get; set; }
        public virtual DbSet<currentshiftdata> currentshiftdatas { get; set; }
        public virtual DbSet<shiftinfo> shiftinfoes { get; set; }
        public virtual DbSet<userinfo> userinfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<currentdata>()
                .Property(e => e.qr1)
                .IsUnicode(false);

            modelBuilder.Entity<currentdata>()
                .Property(e => e.qr2)
                .IsUnicode(false);

            modelBuilder.Entity<currentdata>()
                .Property(e => e.qr3)
                .IsUnicode(false);

            modelBuilder.Entity<currentdata>()
                .Property(e => e.qr4)
                .IsUnicode(false);

            modelBuilder.Entity<currentshiftdata>()
                .Property(e => e.qr1)
                .IsUnicode(false);

            modelBuilder.Entity<currentshiftdata>()
                .Property(e => e.qr2)
                .IsUnicode(false);

            modelBuilder.Entity<currentshiftdata>()
                .Property(e => e.qr3)
                .IsUnicode(false);

            modelBuilder.Entity<currentshiftdata>()
                .Property(e => e.qr4)
                .IsUnicode(false);

            modelBuilder.Entity<userinfo>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<userinfo>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
