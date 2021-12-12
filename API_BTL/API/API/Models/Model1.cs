namespace API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<hoadon> hoadons { get; set; }
        public virtual DbSet<khachhang> khachhangs { get; set; }
        public virtual DbSet<loaisanpham> loaisanphams { get; set; }
        public virtual DbSet<nhacungcap> nhacungcaps { get; set; }
        public virtual DbSet<sanpham> sanphams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<khachhang>()
                .HasMany(e => e.hoadons)
                .WithOptional(e => e.khachhang)
                .HasForeignKey(e => e.makh);

            modelBuilder.Entity<loaisanpham>()
                .HasMany(e => e.sanphams)
                .WithOptional(e => e.loaisanpham)
                .HasForeignKey(e => e.loaisp);

            modelBuilder.Entity<nhacungcap>()
                .HasMany(e => e.sanphams)
                .WithOptional(e => e.nhacungcap)
                .HasForeignKey(e => e.ncc);

            modelBuilder.Entity<sanpham>()
                .HasMany(e => e.hoadons)
                .WithOptional(e => e.sanpham)
                .HasForeignKey(e => e.masp);
        }
    }
}
