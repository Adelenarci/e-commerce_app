using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E_Ticaret_Uygulaması.Models
{
    public partial class SmartprodatabaseContext : DbContext
    {
        public SmartprodatabaseContext()
        {
        }

        public SmartprodatabaseContext(DbContextOptions<SmartprodatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourConnectionStringHere");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Turkish_CS_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.SiparişId).HasName("PK__Order__0CAFAEF2FDDE4F51");

                entity.ToTable("Order");

                entity.Property(e => e.SiparişId)
                    .ValueGeneratedNever()
                    .HasColumnName("SiparişID");
                entity.Property(e => e.KullanıcıId).HasColumnName("KullanıcıID");
                entity.Property(e => e.SiparişTarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("Sipariş_Tarihi");
                entity.Property(e => e.ToplamTutar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Toplam_Tutar");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.SiparişDetayId).HasName("PK__OrderDet__25559644E9AD0BD4");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.SiparişDetayId)
                    .ValueGeneratedNever()
                    .HasColumnName("SiparişDetayID");
                entity.Property(e => e.Fiyat).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.SiparişId).HasColumnName("SiparişID");
                entity.Property(e => e.ÜrünId).HasColumnName("ÜrünID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ÜrünId).HasName("PK__Product__A6014F1B01B3164E");

                entity.ToTable("Product");

                entity.Property(e => e.ÜrünId)
                    .ValueGeneratedNever()
                    .HasColumnName("ÜrünID");
                entity.Property(e => e.Açıklama).HasMaxLength(100);
                entity.Property(e => e.Fiyat).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.İsim).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.KullanıcıId).HasName("PK__User__C3631621DAD939E8");

                entity.ToTable("User");

                entity.Property(e => e.KullanıcıId)
                    .ValueGeneratedNever()
                    .HasColumnName("KullanıcıID");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.KullanıcıAdı)
                    .HasMaxLength(50)
                    .HasColumnName("Kullanıcı_Adı");
                entity.Property(e => e.Rol)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Şifre).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
