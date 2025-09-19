using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Shared.CustomAttributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MarkaziaBITStore.Application.ApplicationDBContext;

public partial class BitStoreDbContext : DbContext
{
    public BitStoreDbContext()
    {
    }

    public BitStoreDbContext(DbContextOptions<BitStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BIT_CAT_Category> BIT_CAT_Category { get; set; }

    public virtual DbSet<BIT_COL_Colors> BIT_COL_Colors { get; set; }

    public virtual DbSet<BIT_CRL_CartLog> BIT_CRL_CartLog { get; set; }

    public virtual DbSet<BIT_CRT_Cart> BIT_CRT_Cart { get; set; }

    public virtual DbSet<BIT_ICI_ItemsColorImages> BIT_ICI_ItemsColorImages { get; set; }

    public virtual DbSet<BIT_ITC_ItemsColor> BIT_ITC_ItemsColor { get; set; }

    public virtual DbSet<BIT_ITM_Items> BIT_ITM_Items { get; set; }

    public virtual DbSet<BIT_OFD_OfferDetails> BIT_OFD_OfferDetails { get; set; }

    public virtual DbSet<BIT_OFH_OfferHeader> BIT_OFH_OfferHeader { get; set; }

    public virtual DbSet<BIT_ORD_OrderDetails> BIT_ORD_OrderDetails { get; set; }

    public virtual DbSet<BIT_ORH_OrderHeader> BIT_ORH_OrderHeader { get; set; }

    public virtual DbSet<BIT_ORU_OrderUpdate> BIT_ORU_OrderUpdate { get; set; }

    public virtual DbSet<BIT_OTR_OrderTrack> BIT_OTR_OrderTrack { get; set; }

    public virtual DbSet<BIT_SID_SupplierInvoiceDetails> BIT_SID_SupplierInvoiceDetails { get; set; }

    public virtual DbSet<BIT_SIH_SupplierInvoiceHeader> BIT_SIH_SupplierInvoiceHeader { get; set; }

    public virtual DbSet<BIT_STK_Stock> BIT_STK_Stock { get; set; }

    public virtual DbSet<BIT_STK_StockTransactions> BIT_STK_StockTransactions { get; set; }

    public virtual DbSet<BIT_UFV_UserFavorites> BIT_UFV_UserFavorites { get; set; }

    public virtual DbSet<Bit_Points> Bit_Points { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await AssignNextIdsAsync();

        int retryCount = 0;
        const int maxRetries = 3;

        while (retryCount < maxRetries)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                             sqlEx.Number == 2627) // Primary key violation
            {
                retryCount++;
                if (retryCount >= maxRetries)
                    throw;

                // Reassign IDs and retry
                await AssignNextIdsAsync();
                await Task.Delay(50 * retryCount); // Exponential backoff
            }
        }

        return 0;
    }

    private async Task AssignNextIdsAsync()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added)
            .ToList();

        foreach (var entry in addedEntities)
        {
            var properties = entry.Entity.GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<NextIdAttribute>() != null);

            foreach (var prop in properties)
            {
                // Handle both int and int? types
                var currentValue = prop.GetValue(entry.Entity);
                bool needsId = currentValue == null ||
                              (prop.PropertyType == typeof(int) && (int)currentValue == 0) ||
                              (prop.PropertyType == typeof(int?) && currentValue == null);

                if (needsId)
                {
                    var nextId = await GetNextIdForEntity(entry.Entity.GetType(), prop.Name);
                    prop.SetValue(entry.Entity, nextId);
                }
            }
        }
    }

    private async Task<int> GetNextIdForEntity(Type entityType, string propertyName)
    {
        var tableName = Model.FindEntityType(entityType)?.GetTableName();

        var sql = $@"
        DECLARE @NextId INT
        SELECT @NextId = ISNULL(MAX([{propertyName}]), 0) + 1 FROM [{tableName}]
        SELECT @NextId";

        // Use EF's Database.ExecuteSqlRawAsync to work with existing transaction
        var connection = Database.GetDbConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;

        // Important: Use the current transaction if one exists
        var currentTransaction = Database.CurrentTransaction;
        if (currentTransaction != null)
        {
            command.Transaction = currentTransaction.GetDbTransaction();
        }

        if (connection.State != ConnectionState.Open)
            await Database.OpenConnectionAsync();

        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BIT_CAT_Category>(entity =>
        {
            entity.HasKey(e => e.BIT_CAT_ID).HasName("PK__BIT_CAT___EE50DDF0F60ADC68");

            entity.Property(e => e.BIT_CAT_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.BIT_CAT_IconURL).HasMaxLength(255);
            entity.Property(e => e.BIT_CAT_NameAR).HasMaxLength(255);
            entity.Property(e => e.BIT_CAT_NameEN).HasMaxLength(255);
        });

        modelBuilder.Entity<BIT_COL_Colors>(entity =>
        {
            entity.HasKey(e => e.BIT_COL_ID).HasName("PK__BIT_COL___4B2C4A16F4B02C3D");

            entity.Property(e => e.BIT_COL_ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<BIT_CRL_CartLog>(entity =>
        {
            entity.HasKey(e => e.BIT_CRL_ID);

            entity.Property(e => e.BIT_CRL_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_CRL__BIT_CRT).WithMany(p => p.BIT_CRL_CartLog)
                .HasForeignKey(d => d.BIT_CRL__BIT_CRTID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_CRL_CartLog_BIT_CRL__BIT_CRTID_BIT_CRT_Cart_BIT_CRT_ID");

            entity.HasOne(d => d.BIT_CRL__BIT_ITC).WithMany(p => p.BIT_CRL_CartLog)
                .HasForeignKey(d => d.BIT_CRL__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_CRL_CartLog_BIT_CRL__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BIT_CRT_Cart>(entity =>
        {
            entity.HasKey(e => e.BIT_CRT_ID).HasName("PK__BIT_CRT___C944CDFCF9B115B0");

            entity.Property(e => e.BIT_CRT_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_CRT__BIT_ITC).WithMany(p => p.BIT_CRT_Cart)
                .HasForeignKey(d => d.BIT_CRT__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_CRT_Cart_BIT_CRT__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BIT_ICI_ItemsColorImages>(entity =>
        {
            entity.HasKey(e => e.BIT_ICI_ID).HasName("PK__BIT_ICI___72C3AD6A01567452");

            entity.Property(e => e.BIT_ICI_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.BIT_ICI_ImageURL).IsUnicode(false);

            entity.HasOne(d => d.BIT_ICI__BIT_ITC).WithMany(p => p.BIT_ICI_ItemsColorImages)
                .HasForeignKey(d => d.BIT_ICI__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ICI_ItemsColorImages_BIT_ICI__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BIT_ITC_ItemsColor>(entity =>
        {
            entity.HasKey(e => e.BIT_ITC_ID).HasName("PK__BIT_ITC___4E3B33391F33C1DC");

            entity.HasIndex(e => e.BIT_ITC_ID, "UniqeKey_COLID__ITMID").IsUnique();

            entity.Property(e => e.BIT_ITC_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_ITC__BIT_COL).WithMany(p => p.BIT_ITC_ItemsColor)
                .HasForeignKey(d => d.BIT_ITC__BIT_COLID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ITC_ItemsColor_BIT_ITC__BIT_COLID_BIT_COL_Colors_BIT_COL_ID");

            entity.HasOne(d => d.BIT_ITC__BIT_ITM).WithMany(p => p.BIT_ITC_ItemsColor)
                .HasForeignKey(d => d.BIT_ITC__BIT_ITMID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ITC_ItemsColor_BIT_ITC__BIT_ITMID_BIT_ITM_Items_BIT_ITM_ID");
        });

        modelBuilder.Entity<BIT_ITM_Items>(entity =>
        {
            entity.HasKey(e => e.BIT_ITM_ID).HasName("PK__BIT_ITM___913D097FEC5C4E4D");

            entity.Property(e => e.BIT_ITM_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_ITM__BIT_CAT).WithMany(p => p.BIT_ITM_Items)
                .HasForeignKey(d => d.BIT_ITM__BIT_CATID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ITM_Items_BIT_ITM__BIT_CATID_BIT_CAT_Category_BIT_CAT_ID");
        });

        modelBuilder.Entity<BIT_OFD_OfferDetails>(entity =>
        {
            entity.HasKey(e => e.BIT_OFD_ID).HasName("PK__BIT_OFF___225BBA05A1C063C4");

            entity.Property(e => e.BIT_OFD_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_OFD__BIT_ITC).WithMany(p => p.BIT_OFD_OfferDetails)
                .HasForeignKey(d => d.BIT_OFD__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_OFD_OfferDetails_BIT_OFD__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");

            entity.HasOne(d => d.BIT_OFD__BIT_OFH).WithMany(p => p.BIT_OFD_OfferDetails)
                .HasForeignKey(d => d.BIT_OFD__BIT_OFHID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_OFD_OfferDetails_BIT_OFD__BIT_OFHID_BIT_OFH_OfferHeader_BIT_OFH_ID");
        });

        modelBuilder.Entity<BIT_OFH_OfferHeader>(entity =>
        {
            entity.HasKey(e => e.BIT_OFH_ID).HasName("PK__BIT_OFF___A3DE69823C3D5F5B");

            entity.Property(e => e.BIT_OFH_ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<BIT_ORD_OrderDetails>(entity =>
        {
            entity.HasKey(e => e.BIT_ORD_ID).HasName("PK__BIT_ORD___CE5669E9A262465E");

            entity.Property(e => e.BIT_ORD_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_ORD__BIT_CRT).WithMany(p => p.BIT_ORD_OrderDetails)
                .HasForeignKey(d => d.BIT_ORD__BIT_CRTID)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_CRTID_BIT_CRT_Cart_BIT_CRT_ID");

            entity.HasOne(d => d.BIT_ORD__BIT_ITC).WithMany(p => p.BIT_ORD_OrderDetails)
                .HasForeignKey(d => d.BIT_ORD__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");

            entity.HasOne(d => d.BIT_ORD__BIT_OFD).WithMany(p => p.BIT_ORD_OrderDetails)
                .HasForeignKey(d => d.BIT_ORD__BIT_OFDID)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_OFDID_BIT_OFD_OfferDetails_BIT_OFD_ID");

            entity.HasOne(d => d.BIT_ORD__BIT_ORH).WithMany(p => p.BIT_ORD_OrderDetails)
                .HasForeignKey(d => d.BIT_ORD__BIT_ORHID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_ORHID_BIT_ORH_OrderHeader_BIT_ORH_ID");
        });

        modelBuilder.Entity<BIT_ORH_OrderHeader>(entity =>
        {
            entity.HasKey(e => e.BIT_ORH_ID).HasName("PK__BIT_ORD___15C4032E6AE3CFB0");

            entity.Property(e => e.BIT_ORH_ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<BIT_ORU_OrderUpdate>(entity =>
        {
            entity.HasKey(e => e.BIT_ORU_ID).HasName("PK__BIT_ORU___DE3793F5D71C423F");

            entity.Property(e => e.BIT_ORU_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_ORU__BIT_ORH).WithMany(p => p.BIT_ORU_OrderUpdate)
                .HasForeignKey(d => d.BIT_ORU__BIT_ORHID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ORU_OrderUpdate_BIT_ORU__BIT_ORHID_BIT_ORH_OrderHeader_BIT_ORH_ID");
        });

        modelBuilder.Entity<BIT_OTR_OrderTrack>(entity =>
        {
            entity.HasKey(e => e.BIT_OTR_ID).HasName("PK__BIT_OTR___BA1E2FF1163B35C3");

            entity.Property(e => e.BIT_OTR_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_OTR__BIT_ORH).WithMany(p => p.BIT_OTR_OrderTrack)
                .HasForeignKey(d => d.BIT_OTR__BIT_ORHID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_OTR_OrderTrack_BIT_OTR__BIT_ORHID_BIT_ORH_OrderHeader_BIT_ORH_ID");
        });

        modelBuilder.Entity<BIT_SID_SupplierInvoiceDetails>(entity =>
        {
            entity.HasKey(e => e.BIT_SID_ID);

            entity.HasIndex(e => e.BIT_SID_ID, "UniqeKey_SIHID__ITCID").IsUnique();

            entity.Property(e => e.BIT_SID_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_SID__BIT_ITC).WithMany(p => p.BIT_SID_SupplierInvoiceDetails)
                .HasForeignKey(d => d.BIT_SID__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_SID_SupplierInvoiceDetails_BIT_SID__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");

            entity.HasOne(d => d.BIT_SID__BIT_SIH).WithMany(p => p.BIT_SID_SupplierInvoiceDetails)
                .HasForeignKey(d => d.BIT_SID__BIT_SIHID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_SID_SupplierInvoiceDetails_BIT_SID__BIT_SIHID_BIT_SIH_SupplierInvoiceHeader_BIT_SIH_ID");
        });

        modelBuilder.Entity<BIT_SIH_SupplierInvoiceHeader>(entity =>
        {
            entity.HasKey(e => e.BIT_SIH_ID).HasName("PK__BIT_SUP1__9B6F70C9D31536E8");

            entity.Property(e => e.BIT_SIH_ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<BIT_STK_Stock>(entity =>
        {
            entity.HasKey(e => e.BIT_STK_ID).HasName("PK__BIT_STK___CD8A00C82AAA65B0");

            entity.Property(e => e.BIT_STK_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_STK__BIT_ITC).WithMany(p => p.BIT_STK_Stock)
                .HasForeignKey(d => d.BIT_STK__BIT_ITCID)
                .HasConstraintName("FK_BIT_STK_Stock_BIT_STK__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BIT_STK_StockTransactions>(entity =>
        {
            entity.HasKey(e => e.BIT_STK_ID).HasName("PK__BIT_STK___CD8A00C8F1DD4556");

            entity.Property(e => e.BIT_STK_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_STK__BIT_ITC).WithMany(p => p.BIT_STK_StockTransactions)
                .HasForeignKey(d => d.BIT_STK__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_STK_StockTransactions_BIT_STK__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BIT_UFV_UserFavorites>(entity =>
        {
            entity.HasKey(e => e.BIT_UFV_ID).HasName("PK__BIT_UFV___FE1EE66B4F35FA0D");

            entity.Property(e => e.BIT_UFV_ID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BIT_UFV__BIT_ITC).WithMany(p => p.BIT_UFV_UserFavorites)
                .HasForeignKey(d => d.BIT_UFV__BIT_ITCID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_UFV_UserFavorites_BIT_UFV__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<Bit_Points>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Bit_Points");

            entity.Property(e => e.UserName).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
