using DXP.SmartConnectPickup.DataServices.Models;
using Microsoft.EntityFrameworkCore;

namespace DXP.SmartConnectPickup.DataServices.Context
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }


        public virtual DbSet<TransactionLog> TransactionLog { get; set; }

        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.Provider).HasMaxLength(256);

                entity.Property(e => e.ExternalId).HasMaxLength(128);

                entity.Property(e => e.ExternalApiToken).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Phone).HasMaxLength(128);

                entity.Property(e => e.CarColor).HasMaxLength(200);

                entity.Property(e => e.CarType).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SettingName).HasMaxLength(100);
            });

            modelBuilder.Entity<TransactionLog>(entity =>
            {
                entity.ToTable("TransactionLog");

                entity.HasKey(e => e.Id).HasName("PK_TransactionLog");

                entity.Property(e => e.TransactionName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.ExceptionMessage).HasMaxLength(200);

                entity.Property(e => e.HostName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);
            });
        }
    }
}
