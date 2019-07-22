namespace MMi_BIS_PA.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB_Model : DbContext
    {
        public DB_Model()
            : base("name=DB_Model")
        {
        }

        public virtual DbSet<current_data> current_data { get; set; }
        public virtual DbSet<current_shift_data> current_shift_data { get; set; }
        public virtual DbSet<shift_details> shift_details { get; set; }
        public virtual DbSet<user_info> user_info { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<current_data>()
                .Property(e => e.Qrcode)
                .IsUnicode(false);

            modelBuilder.Entity<current_shift_data>()
                .Property(e => e.Qrcode)
                .IsUnicode(false);

            modelBuilder.Entity<user_info>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user_info>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
