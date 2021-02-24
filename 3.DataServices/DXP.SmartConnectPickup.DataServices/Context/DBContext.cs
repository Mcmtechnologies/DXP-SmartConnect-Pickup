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
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<StoreService> StoreService { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.HasKey(e => e.Id).HasName("PK_Customer");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.Provider).HasMaxLength(256);

                entity.Property(e => e.ExternalId).HasMaxLength(256);

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

            modelBuilder.Entity<Site>(entity =>
            {
                entity.ToTable("Site");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.HasKey(e => e.Id).HasName("PK_Site");

                entity.Property(e => e.Provider).HasMaxLength(256);

                entity.Property(e => e.ExternalId).HasMaxLength(256);

                entity.Property(e => e.StoreId).HasMaxLength(128);

                entity.Property(e => e.StoreCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

            });

            modelBuilder.Entity<StoreService>(entity =>
            {
                entity.ToTable("StoreService");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.HasKey(e => e.Id).HasName("PK_StoreService");

                entity.Property(e => e.ServiceName).HasMaxLength(200);

                entity.Property(e => e.ServiceShortName).HasMaxLength(10);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.HasKey(e => e.Id).HasName("PK_Order");

                entity.Property(e => e.OrderApiId).HasMaxLength(128);

                entity.Property(e => e.Provider).HasMaxLength(256);

                entity.Property(e => e.ExternalId).HasMaxLength(256);

                entity.Property(e => e.ExternalStatus).HasMaxLength(50);

                entity.Property(e => e.RedemptionCode).HasMaxLength(100);

                entity.Property(e => e.RedemptionUrl).HasMaxLength(500);

                entity.Property(e => e.ExternalSiteId).HasMaxLength(256);

                entity.Property(e => e.PickupWindow).HasMaxLength(200);

                entity.Property(e => e.PickupType).HasMaxLength(50);

                entity.Property(e => e.DisplayId).HasMaxLength(200);

                entity.Property(e => e.StoreService).HasMaxLength(50);

                entity.Property(e => e.StoreId).HasMaxLength(128);

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.HasKey(e => e.Id).HasName("PK_Setting");

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
