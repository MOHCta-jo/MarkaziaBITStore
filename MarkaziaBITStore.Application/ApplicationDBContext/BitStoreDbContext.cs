using System;
using System.Collections.Generic;
using MarkaziaBITStore.Application.Entites;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<BitCatCategory> BitCatCategories { get; set; }

    public virtual DbSet<BitColColor> BitColColors { get; set; }

    public virtual DbSet<BitCrlCartLog> BitCrlCartLogs { get; set; }

    public virtual DbSet<BitCrtCart> BitCrtCarts { get; set; }

    public virtual DbSet<BitIciItemsColorImage> BitIciItemsColorImages { get; set; }

    public virtual DbSet<BitItcItemsColor> BitItcItemsColors { get; set; }

    public virtual DbSet<BitItmItem> BitItmItems { get; set; }

    public virtual DbSet<BitOfdOfferDetail> BitOfdOfferDetails { get; set; }

    public virtual DbSet<BitOfhOfferHeader> BitOfhOfferHeaders { get; set; }

    public virtual DbSet<BitOrdOrderDetail> BitOrdOrderDetails { get; set; }

    public virtual DbSet<BitOrhOrderHeader> BitOrhOrderHeaders { get; set; }

    public virtual DbSet<BitOruOrderUpdate> BitOruOrderUpdates { get; set; }

    public virtual DbSet<BitOtrOrderTrack> BitOtrOrderTracks { get; set; }

    public virtual DbSet<BitSidSupplierInvoiceDetail> BitSidSupplierInvoiceDetails { get; set; }

    public virtual DbSet<BitSihSupplierInvoiceHeader> BitSihSupplierInvoiceHeaders { get; set; }

    public virtual DbSet<BitStkStock> BitStkStocks { get; set; }

    public virtual DbSet<BitStkStockTransaction> BitStkStockTransactions { get; set; }

    public virtual DbSet<BitUfvUserFavorite> BitUfvUserFavorites { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP100\\SQLEXPRESS;Database=BITSTORE-Test;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BitCatCategory>(entity =>
        {
            entity.HasKey(e => e.BitCatId).HasName("PK__BIT_CAT___EE50DDF0F60ADC68");

            entity.ToTable("BIT_CAT_Category");

            entity.Property(e => e.BitCatId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_CAT_ID");
            entity.Property(e => e.BitCatBitUsridCancelledUser).HasColumnName("BIT_CAT__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitCatBitUsridEnterUser).HasColumnName("BIT_CAT__BIT_USRID_EnterUser");
            entity.Property(e => e.BitCatBitUsridModUser).HasColumnName("BIT_CAT__BIT_USRID_ModUser");
            entity.Property(e => e.BitCatCancelled).HasColumnName("BIT_CAT_Cancelled");
            entity.Property(e => e.BitCatCancelledDate).HasColumnName("BIT_CAT_CancelledDate");
            entity.Property(e => e.BitCatCancelledTime).HasColumnName("BIT_CAT_CancelledTime");
            entity.Property(e => e.BitCatEnterDate).HasColumnName("BIT_CAT_EnterDate");
            entity.Property(e => e.BitCatEnterTime).HasColumnName("BIT_CAT_EnterTime");
            entity.Property(e => e.BitCatIconUrl)
                .HasMaxLength(255)
                .HasColumnName("BIT_CAT_IconURL");
            entity.Property(e => e.BitCatIsActive).HasColumnName("BIT_CAT_IsActive");
            entity.Property(e => e.BitCatModDate).HasColumnName("BIT_CAT_ModDate");
            entity.Property(e => e.BitCatModTime).HasColumnName("BIT_CAT_ModTime");
            entity.Property(e => e.BitCatNameAr)
                .HasMaxLength(255)
                .HasColumnName("BIT_CAT_NameAR");
            entity.Property(e => e.BitCatNameEn)
                .HasMaxLength(255)
                .HasColumnName("BIT_CAT_NameEN");
        });

        modelBuilder.Entity<BitColColor>(entity =>
        {
            entity.HasKey(e => e.BitColId).HasName("PK__BIT_COL___4B2C4A16F4B02C3D");

            entity.ToTable("BIT_COL_Colors");

            entity.Property(e => e.BitColId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_COL_ID");
            entity.Property(e => e.BitColBitUsridCancelledUser).HasColumnName("BIT_COL__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitColBitUsridEnterUser).HasColumnName("BIT_COL__BIT_USRID_EnterUser");
            entity.Property(e => e.BitColBitUsridModUser).HasColumnName("BIT_COL__BIT_USRID_ModUser");
            entity.Property(e => e.BitColCancelled).HasColumnName("BIT_COL_Cancelled");
            entity.Property(e => e.BitColCancelledDate).HasColumnName("BIT_COL_CancelledDate");
            entity.Property(e => e.BitColCancelledTime).HasColumnName("BIT_COL_CancelledTime");
            entity.Property(e => e.BitColEnterDate).HasColumnName("BIT_COL_EnterDate");
            entity.Property(e => e.BitColEnterTime).HasColumnName("BIT_COL_EnterTime");
            entity.Property(e => e.BitColHexCode).HasColumnName("BIT_COL_HexCode");
            entity.Property(e => e.BitColModDate).HasColumnName("BIT_COL_ModDate");
            entity.Property(e => e.BitColModTime).HasColumnName("BIT_COL_ModTime");
            entity.Property(e => e.BitColNameAr).HasColumnName("BIT_COL_NameAR");
            entity.Property(e => e.BitColNameEn).HasColumnName("BIT_COL_NameEN");
        });

        modelBuilder.Entity<BitCrlCartLog>(entity =>
        {
            entity.HasKey(e => e.BitCrlId);

            entity.ToTable("BIT_CRL_CartLog");

            entity.Property(e => e.BitCrlId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_CRL_ID");
            entity.Property(e => e.BitCrlBitCrtid).HasColumnName("BIT_CRL__BIT_CRTID");
            entity.Property(e => e.BitCrlBitItcid).HasColumnName("BIT_CRL__BIT_ITCID");
            entity.Property(e => e.BitCrlBitUsrid).HasColumnName("BIT_CRL__BIT_USRID");
            entity.Property(e => e.BitCrlBitUsridCancelledUser).HasColumnName("BIT_CRL__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitCrlBitUsridEnterUser).HasColumnName("BIT_CRL__BIT_USRID_EnterUser");
            entity.Property(e => e.BitCrlBitUsridModUser).HasColumnName("BIT_CRL__BIT_USRID_ModUser");
            entity.Property(e => e.BitCrlCancelled).HasColumnName("BIT_CRL_Cancelled");
            entity.Property(e => e.BitCrlCancelledDate).HasColumnName("BIT_CRL_CancelledDate");
            entity.Property(e => e.BitCrlCancelledTime).HasColumnName("BIT_CRL_CancelledTime");
            entity.Property(e => e.BitCrlEnterDate).HasColumnName("BIT_CRL_EnterDate");
            entity.Property(e => e.BitCrlEnterTime).HasColumnName("BIT_CRL_EnterTime");
            entity.Property(e => e.BitCrlModDate).HasColumnName("BIT_CRL_ModDate");
            entity.Property(e => e.BitCrlModTime).HasColumnName("BIT_CRL_ModTime");
            entity.Property(e => e.BitCrlPoints).HasColumnName("BIT_CRL_Points");
            entity.Property(e => e.BitCrlQuantity).HasColumnName("BIT_CRL_Quantity");
            entity.Property(e => e.BitCrlStatus).HasColumnName("BIT_CRL_Status");

            entity.HasOne(d => d.BitCrlBitCrt).WithMany(p => p.BitCrlCartLogs)
                .HasForeignKey(d => d.BitCrlBitCrtid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_CRL_CartLog_BIT_CRL__BIT_CRTID_BIT_CRT_Cart_BIT_CRT_ID");

            entity.HasOne(d => d.BitCrlBitItc).WithMany(p => p.BitCrlCartLogs)
                .HasForeignKey(d => d.BitCrlBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_CRL_CartLog_BIT_CRL__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BitCrtCart>(entity =>
        {
            entity.HasKey(e => e.BitCrtId).HasName("PK__BIT_CRT___C944CDFCF9B115B0");

            entity.ToTable("BIT_CRT_Cart");

            entity.Property(e => e.BitCrtId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_CRT_ID");
            entity.Property(e => e.BitCrtBitItcid).HasColumnName("BIT_CRT__BIT_ITCID");
            entity.Property(e => e.BitCrtBitUsrid).HasColumnName("BIT_CRT__BIT_USRID");
            entity.Property(e => e.BitCrtBitUsridCancelledUser).HasColumnName("BIT_CRT__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitCrtBitUsridEnterUser).HasColumnName("BIT_CRT__BIT_USRID_EnterUser");
            entity.Property(e => e.BitCrtBitUsridModUser).HasColumnName("BIT_CRT__BIT_USRID_ModUser");
            entity.Property(e => e.BitCrtCancelled).HasColumnName("BIT_CRT_Cancelled");
            entity.Property(e => e.BitCrtCancelledDate).HasColumnName("BIT_CRT_CancelledDate");
            entity.Property(e => e.BitCrtCancelledTime).HasColumnName("BIT_CRT_CancelledTime");
            entity.Property(e => e.BitCrtEnterDate).HasColumnName("BIT_CRT_EnterDate");
            entity.Property(e => e.BitCrtEnterTime).HasColumnName("BIT_CRT_EnterTime");
            entity.Property(e => e.BitCrtItemSource).HasColumnName("BIT_CRT_ItemSource");
            entity.Property(e => e.BitCrtModDate).HasColumnName("BIT_CRT_ModDate");
            entity.Property(e => e.BitCrtModTime).HasColumnName("BIT_CRT_ModTime");
            entity.Property(e => e.BitCrtQuantity).HasColumnName("BIT_CRT_Quantity");
            entity.Property(e => e.BitCrtStatus).HasColumnName("BIT_CRT_Status");

            entity.HasOne(d => d.BitCrtBitItc).WithMany(p => p.BitCrtCarts)
                .HasForeignKey(d => d.BitCrtBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_CRT_Cart_BIT_CRT__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BitIciItemsColorImage>(entity =>
        {
            entity.HasKey(e => e.BitIciId).HasName("PK__BIT_ICI___72C3AD6A01567452");

            entity.ToTable("BIT_ICI_ItemsColorImages");

            entity.Property(e => e.BitIciId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_ICI_ID");
            entity.Property(e => e.BitIciBitItcid).HasColumnName("BIT_ICI__BIT_ITCID");
            entity.Property(e => e.BitIciBitUsridCancelledUser).HasColumnName("BIT_ICI__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitIciBitUsridEnterUser).HasColumnName("BIT_ICI__BIT_USRID_EnterUser");
            entity.Property(e => e.BitIciBitUsridModUser).HasColumnName("BIT_ICI__BIT_USRID_ModUser");
            entity.Property(e => e.BitIciCancelled).HasColumnName("BIT_ICI_Cancelled");
            entity.Property(e => e.BitIciCancelledDate).HasColumnName("BIT_ICI_CancelledDate");
            entity.Property(e => e.BitIciCancelledTime).HasColumnName("BIT_ICI_CancelledTime");
            entity.Property(e => e.BitIciEnterDate).HasColumnName("BIT_ICI_EnterDate");
            entity.Property(e => e.BitIciEnterTime).HasColumnName("BIT_ICI_EnterTime");
            entity.Property(e => e.BitIciImageUrl)
                .IsUnicode(false)
                .HasColumnName("BIT_ICI_ImageURL");
            entity.Property(e => e.BitIciIsDefault).HasColumnName("BIT_ICI_IsDefault");
            entity.Property(e => e.BitIciModDate).HasColumnName("BIT_ICI_ModDate");
            entity.Property(e => e.BitIciModTime).HasColumnName("BIT_ICI_ModTime");
            entity.Property(e => e.BitIciSequence).HasColumnName("BIT_ICI_Sequence");
            entity.Property(e => e.BitIciStatus).HasColumnName("BIT_ICI_Status");

            entity.HasOne(d => d.BitIciBitItc).WithMany(p => p.BitIciItemsColorImages)
                .HasForeignKey(d => d.BitIciBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ICI_ItemsColorImages_BIT_ICI__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BitItcItemsColor>(entity =>
        {
            entity.HasKey(e => e.BitItcId).HasName("PK__BIT_ITC___4E3B33391F33C1DC");

            entity.ToTable("BIT_ITC_ItemsColor");

            entity.HasIndex(e => e.BitItcId, "UniqeKey_COLID__ITMID").IsUnique();

            entity.Property(e => e.BitItcId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_ITC_ID");
            entity.Property(e => e.BitItcBitColid).HasColumnName("BIT_ITC__BIT_COLID");
            entity.Property(e => e.BitItcBitItmid).HasColumnName("BIT_ITC__BIT_ITMID");
            entity.Property(e => e.BitItcBitUsridCancelledUser).HasColumnName("BIT_ITC__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitItcBitUsridEnterUser).HasColumnName("BIT_ITC__BIT_USRID_EnterUser");
            entity.Property(e => e.BitItcBitUsridModUser).HasColumnName("BIT_ITC__BIT_USRID_ModUser");
            entity.Property(e => e.BitItcCancelled).HasColumnName("BIT_ITC_Cancelled");
            entity.Property(e => e.BitItcCancelledDate).HasColumnName("BIT_ITC_CancelledDate");
            entity.Property(e => e.BitItcCancelledTime).HasColumnName("BIT_ITC_CancelledTime");
            entity.Property(e => e.BitItcEnterDate).HasColumnName("BIT_ITC_EnterDate");
            entity.Property(e => e.BitItcEnterTime).HasColumnName("BIT_ITC_EnterTime");
            entity.Property(e => e.BitItcModDate).HasColumnName("BIT_ITC_ModDate");
            entity.Property(e => e.BitItcModTime).HasColumnName("BIT_ITC_ModTime");
            entity.Property(e => e.BitItcStatus).HasColumnName("BIT_ITC_Status");

            entity.HasOne(d => d.BitItcBitCol).WithMany(p => p.BitItcItemsColors)
                .HasForeignKey(d => d.BitItcBitColid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ITC_ItemsColor_BIT_ITC__BIT_COLID_BIT_COL_Colors_BIT_COL_ID");

            entity.HasOne(d => d.BitItcBitItm).WithMany(p => p.BitItcItemsColors)
                .HasForeignKey(d => d.BitItcBitItmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ITC_ItemsColor_BIT_ITC__BIT_ITMID_BIT_ITM_Items_BIT_ITM_ID");
        });

        modelBuilder.Entity<BitItmItem>(entity =>
        {
            entity.HasKey(e => e.BitItmId).HasName("PK__BIT_ITM___913D097FEC5C4E4D");

            entity.ToTable("BIT_ITM_Items");

            entity.Property(e => e.BitItmId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_ITM_ID");
            entity.Property(e => e.BitItmBitCatid).HasColumnName("BIT_ITM__BIT_CATID");
            entity.Property(e => e.BitItmBitUsridCancelledUser).HasColumnName("BIT_ITM__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitItmBitUsridEnterUser).HasColumnName("BIT_ITM__BIT_USRID_EnterUser");
            entity.Property(e => e.BitItmBitUsridModUser).HasColumnName("BIT_ITM__BIT_USRID_ModUser");
            entity.Property(e => e.BitItmCancelled).HasColumnName("BIT_ITM_Cancelled");
            entity.Property(e => e.BitItmCancelledDate).HasColumnName("BIT_ITM_CancelledDate");
            entity.Property(e => e.BitItmCancelledTime).HasColumnName("BIT_ITM_CancelledTime");
            entity.Property(e => e.BitItmDescriptionAr).HasColumnName("BIT_ITM_DescriptionAR");
            entity.Property(e => e.BitItmDescriptionEn).HasColumnName("BIT_ITM_DescriptionEN");
            entity.Property(e => e.BitItmEnterDate).HasColumnName("BIT_ITM_EnterDate");
            entity.Property(e => e.BitItmEnterTime).HasColumnName("BIT_ITM_EnterTime");
            entity.Property(e => e.BitItmModDate).HasColumnName("BIT_ITM_ModDate");
            entity.Property(e => e.BitItmModTime).HasColumnName("BIT_ITM_ModTime");
            entity.Property(e => e.BitItmNameAr).HasColumnName("BIT_ITM_NameAR");
            entity.Property(e => e.BitItmNameEn).HasColumnName("BIT_ITM_NameEN");
            entity.Property(e => e.BitItmPoints).HasColumnName("BIT_ITM_Points");
            entity.Property(e => e.BitItmStatus).HasColumnName("BIT_ITM_Status");

            entity.HasOne(d => d.BitItmBitCat).WithMany(p => p.BitItmItems)
                .HasForeignKey(d => d.BitItmBitCatid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ITM_Items_BIT_ITM__BIT_CATID_BIT_CAT_Category_BIT_CAT_ID");
        });

        modelBuilder.Entity<BitOfdOfferDetail>(entity =>
        {
            entity.HasKey(e => e.BitOfdId).HasName("PK__BIT_OFF___225BBA05A1C063C4");

            entity.ToTable("BIT_OFD_OfferDetails");

            entity.Property(e => e.BitOfdId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_OFD_ID");
            entity.Property(e => e.BitOfdBitItcid).HasColumnName("BIT_OFD__BIT_ITCID");
            entity.Property(e => e.BitOfdBitOfhid).HasColumnName("BIT_OFD__BIT_OFHID");
            entity.Property(e => e.BitOfdBitUsridCancelledUser).HasColumnName("BIT_OFD__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitOfdBitUsridEnterUser).HasColumnName("BIT_OFD__BIT_USRID_EnterUser");
            entity.Property(e => e.BitOfdBitUsridModUser).HasColumnName("BIT_OFD__BIT_USRID_ModUser");
            entity.Property(e => e.BitOfdCancelled).HasColumnName("BIT_OFD_Cancelled");
            entity.Property(e => e.BitOfdCancelledDate).HasColumnName("BIT_OFD_CancelledDate");
            entity.Property(e => e.BitOfdCancelledTime).HasColumnName("BIT_OFD_CancelledTime");
            entity.Property(e => e.BitOfdDiscount).HasColumnName("BIT_OFD_Discount");
            entity.Property(e => e.BitOfdDiscountValue).HasColumnName("BIT_OFD_DiscountValue");
            entity.Property(e => e.BitOfdEnterDate).HasColumnName("BIT_OFD_EnterDate");
            entity.Property(e => e.BitOfdEnterTime).HasColumnName("BIT_OFD_EnterTime");
            entity.Property(e => e.BitOfdItemPoints).HasColumnName("BIT_OFD_ItemPoints");
            entity.Property(e => e.BitOfdItemPointsNet).HasColumnName("BIT_OFD_ItemPointsNet");
            entity.Property(e => e.BitOfdModDate).HasColumnName("BIT_OFD_ModDate");
            entity.Property(e => e.BitOfdModTime).HasColumnName("BIT_OFD_ModTime");
            entity.Property(e => e.BitOfdStatus).HasColumnName("BIT_OFD_Status");

            entity.HasOne(d => d.BitOfdBitItc).WithMany(p => p.BitOfdOfferDetails)
                .HasForeignKey(d => d.BitOfdBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_OFD_OfferDetails_BIT_OFD__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");

            entity.HasOne(d => d.BitOfdBitOfh).WithMany(p => p.BitOfdOfferDetails)
                .HasForeignKey(d => d.BitOfdBitOfhid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_OFD_OfferDetails_BIT_OFD__BIT_OFHID_BIT_OFH_OfferHeader_BIT_OFH_ID");
        });

        modelBuilder.Entity<BitOfhOfferHeader>(entity =>
        {
            entity.HasKey(e => e.BitOfhId).HasName("PK__BIT_OFF___A3DE69823C3D5F5B");

            entity.ToTable("BIT_OFH_OfferHeader");

            entity.Property(e => e.BitOfhId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_OFH_ID");
            entity.Property(e => e.BitOfhBitUsridCancelledUser).HasColumnName("BIT_OFH__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitOfhBitUsridEnterUser).HasColumnName("BIT_OFH__BIT_USRID_EnterUser");
            entity.Property(e => e.BitOfhBitUsridModUser).HasColumnName("BIT_OFH__BIT_USRID_ModUser");
            entity.Property(e => e.BitOfhCancelled).HasColumnName("BIT_OFH_Cancelled");
            entity.Property(e => e.BitOfhCancelledDate).HasColumnName("BIT_OFH_CancelledDate");
            entity.Property(e => e.BitOfhCancelledTime).HasColumnName("BIT_OFH_CancelledTime");
            entity.Property(e => e.BitOfhEnterDate).HasColumnName("BIT_OFH_EnterDate");
            entity.Property(e => e.BitOfhEnterTime).HasColumnName("BIT_OFH_EnterTime");
            entity.Property(e => e.BitOfhModDate).HasColumnName("BIT_OFH_ModDate");
            entity.Property(e => e.BitOfhModTime).HasColumnName("BIT_OFH_ModTime");
            entity.Property(e => e.BitOfhOfferEndDate).HasColumnName("BIT_OFH_OfferEndDate");
            entity.Property(e => e.BitOfhOfferName).HasColumnName("BIT_OFH_OfferName");
            entity.Property(e => e.BitOfhOfferStartDate).HasColumnName("BIT_OFH_OfferStartDate");
            entity.Property(e => e.BitOfhStatus).HasColumnName("BIT_OFH_Status");
        });

        modelBuilder.Entity<BitOrdOrderDetail>(entity =>
        {
            entity.HasKey(e => e.BitOrdId).HasName("PK__BIT_ORD___CE5669E9A262465E");

            entity.ToTable("BIT_ORD_OrderDetails");

            entity.Property(e => e.BitOrdId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_ORD_ID");
            entity.Property(e => e.BitOrdBitCrtid).HasColumnName("BIT_ORD__BIT_CRTID");
            entity.Property(e => e.BitOrdBitItcid).HasColumnName("BIT_ORD__BIT_ITCID");
            entity.Property(e => e.BitOrdBitOfdid).HasColumnName("BIT_ORD__BIT_OFDID");
            entity.Property(e => e.BitOrdBitOrhid).HasColumnName("BIT_ORD__BIT_ORHID");
            entity.Property(e => e.BitOrdBitUsridCancelledUser).HasColumnName("BIT_ORD__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitOrdBitUsridEnterUser).HasColumnName("BIT_ORD__BIT_USRID_EnterUser");
            entity.Property(e => e.BitOrdBitUsridModUser).HasColumnName("BIT_ORD__BIT_USRID_ModUser");
            entity.Property(e => e.BitOrdCancelled).HasColumnName("BIT_ORD_Cancelled");
            entity.Property(e => e.BitOrdCancelledDate).HasColumnName("BIT_ORD_CancelledDate");
            entity.Property(e => e.BitOrdCancelledTime).HasColumnName("BIT_ORD_CancelledTime");
            entity.Property(e => e.BitOrdEnterDate).HasColumnName("BIT_ORD_EnterDate");
            entity.Property(e => e.BitOrdEnterTime).HasColumnName("BIT_ORD_EnterTime");
            entity.Property(e => e.BitOrdModDate).HasColumnName("BIT_ORD_ModDate");
            entity.Property(e => e.BitOrdModTime).HasColumnName("BIT_ORD_ModTime");
            entity.Property(e => e.BitOrdPoints).HasColumnName("BIT_ORD_Points");
            entity.Property(e => e.BitOrdQuantity).HasColumnName("BIT_ORD_Quantity");
            entity.Property(e => e.BitOrdStatus).HasColumnName("BIT_ORD_Status");

            entity.HasOne(d => d.BitOrdBitCrt).WithMany(p => p.BitOrdOrderDetails)
                .HasForeignKey(d => d.BitOrdBitCrtid)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_CRTID_BIT_CRT_Cart_BIT_CRT_ID");

            entity.HasOne(d => d.BitOrdBitItc).WithMany(p => p.BitOrdOrderDetails)
                .HasForeignKey(d => d.BitOrdBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");

            entity.HasOne(d => d.BitOrdBitOfd).WithMany(p => p.BitOrdOrderDetails)
                .HasForeignKey(d => d.BitOrdBitOfdid)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_OFDID_BIT_OFD_OfferDetails_BIT_OFD_ID");

            entity.HasOne(d => d.BitOrdBitOrh).WithMany(p => p.BitOrdOrderDetails)
                .HasForeignKey(d => d.BitOrdBitOrhid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ORD_OrderDetails_BIT_ORD__BIT_ORHID_BIT_ORH_OrderHeader_BIT_ORH_ID");
        });

        modelBuilder.Entity<BitOrhOrderHeader>(entity =>
        {
            entity.HasKey(e => e.BitOrhId).HasName("PK__BIT_ORD___15C4032E6AE3CFB0");

            entity.ToTable("BIT_ORH_OrderHeader");

            entity.Property(e => e.BitOrhId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_ORH_ID");
            entity.Property(e => e.BitOrhBitUsrid).HasColumnName("BIT_ORH__BIT_USRID");
            entity.Property(e => e.BitOrhBitUsridCancelledUser).HasColumnName("BIT_ORH__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitOrhBitUsridEnterUser).HasColumnName("BIT_ORH__BIT_USRID_EnterUser");
            entity.Property(e => e.BitOrhBitUsridModUser).HasColumnName("BIT_ORH__BIT_USRID_ModUser");
            entity.Property(e => e.BitOrhCancelled).HasColumnName("BIT_ORH_Cancelled");
            entity.Property(e => e.BitOrhCancelledDate).HasColumnName("BIT_ORH_CancelledDate");
            entity.Property(e => e.BitOrhCancelledTime).HasColumnName("BIT_ORH_CancelledTime");
            entity.Property(e => e.BitOrhEnterDate).HasColumnName("BIT_ORH_EnterDate");
            entity.Property(e => e.BitOrhEnterTime).HasColumnName("BIT_ORH_EnterTime");
            entity.Property(e => e.BitOrhModDate).HasColumnName("BIT_ORH_ModDate");
            entity.Property(e => e.BitOrhModTime).HasColumnName("BIT_ORH_ModTime");
            entity.Property(e => e.BitOrhOrderNumber).HasColumnName("BIT_ORH_OrderNumber");
            entity.Property(e => e.BitOrhOrderSource).HasColumnName("BIT_ORH_OrderSource");
            entity.Property(e => e.BitOrhStatus).HasColumnName("BIT_ORH_Status");
        });

        modelBuilder.Entity<BitOruOrderUpdate>(entity =>
        {
            entity.HasKey(e => e.BitOruId).HasName("PK__BIT_ORU___DE3793F5D71C423F");

            entity.ToTable("BIT_ORU_OrderUpdate");

            entity.Property(e => e.BitOruId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_ORU_ID");
            entity.Property(e => e.BitOruBitOrhid).HasColumnName("BIT_ORU__BIT_ORHID");
            entity.Property(e => e.BitOruBitUsridCancelledUser).HasColumnName("BIT_ORU__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitOruBitUsridEnterUser).HasColumnName("BIT_ORU__BIT_USRID_EnterUser");
            entity.Property(e => e.BitOruBitUsridModUser).HasColumnName("BIT_ORU__BIT_USRID_ModUser");
            entity.Property(e => e.BitOruCancelled).HasColumnName("BIT_ORU_Cancelled");
            entity.Property(e => e.BitOruCancelledDate).HasColumnName("BIT_ORU_CancelledDate");
            entity.Property(e => e.BitOruCancelledTime).HasColumnName("BIT_ORU_CancelledTime");
            entity.Property(e => e.BitOruEnterDate).HasColumnName("BIT_ORU_EnterDate");
            entity.Property(e => e.BitOruEnterTime).HasColumnName("BIT_ORU_EnterTime");
            entity.Property(e => e.BitOruModDate).HasColumnName("BIT_ORU_ModDate");
            entity.Property(e => e.BitOruModTime).HasColumnName("BIT_ORU_ModTime");
            entity.Property(e => e.BitOruStatus).HasColumnName("BIT_ORU_Status");
            entity.Property(e => e.BitOruUpdateDescrption).HasColumnName("BIT_ORU_UpdateDescrption");

            entity.HasOne(d => d.BitOruBitOrh).WithMany(p => p.BitOruOrderUpdates)
                .HasForeignKey(d => d.BitOruBitOrhid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_ORU_OrderUpdate_BIT_ORU__BIT_ORHID_BIT_ORH_OrderHeader_BIT_ORH_ID");
        });

        modelBuilder.Entity<BitOtrOrderTrack>(entity =>
        {
            entity.HasKey(e => e.BitOtrId).HasName("PK__BIT_OTR___BA1E2FF1163B35C3");

            entity.ToTable("BIT_OTR_OrderTrack");

            entity.Property(e => e.BitOtrId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_OTR_ID");
            entity.Property(e => e.BitOtrBitOrhid).HasColumnName("BIT_OTR__BIT_ORHID");
            entity.Property(e => e.BitOtrBitUsridCancelledUser).HasColumnName("BIT_OTR__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitOtrBitUsridEnterUser).HasColumnName("BIT_OTR__BIT_USRID_EnterUser");
            entity.Property(e => e.BitOtrBitUsridModUser).HasColumnName("BIT_OTR__BIT_USRID_ModUser");
            entity.Property(e => e.BitOtrCancelled).HasColumnName("BIT_OTR_Cancelled");
            entity.Property(e => e.BitOtrCancelledDate).HasColumnName("BIT_OTR_CancelledDate");
            entity.Property(e => e.BitOtrCancelledTime).HasColumnName("BIT_OTR_CancelledTime");
            entity.Property(e => e.BitOtrEnterDate).HasColumnName("BIT_OTR_EnterDate");
            entity.Property(e => e.BitOtrEnterTime).HasColumnName("BIT_OTR_EnterTime");
            entity.Property(e => e.BitOtrModDate).HasColumnName("BIT_OTR_ModDate");
            entity.Property(e => e.BitOtrModTime).HasColumnName("BIT_OTR_ModTime");
            entity.Property(e => e.BitOtrOrderStatus).HasColumnName("BIT_OTR_OrderStatus");
            entity.Property(e => e.BitOtrStatus).HasColumnName("BIT_OTR_Status");

            entity.HasOne(d => d.BitOtrBitOrh).WithMany(p => p.BitOtrOrderTracks)
                .HasForeignKey(d => d.BitOtrBitOrhid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_OTR_OrderTrack_BIT_OTR__BIT_ORHID_BIT_ORH_OrderHeader_BIT_ORH_ID");
        });

        modelBuilder.Entity<BitSidSupplierInvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.BitSidId);

            entity.ToTable("BIT_SID_SupplierInvoiceDetails");

            entity.HasIndex(e => e.BitSidId, "UniqeKey_SIHID__ITCID").IsUnique();

            entity.Property(e => e.BitSidId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_SID_ID");
            entity.Property(e => e.BitSidBitItcid).HasColumnName("BIT_SID__BIT_ITCID");
            entity.Property(e => e.BitSidBitSihid).HasColumnName("BIT_SID__BIT_SIHID");
            entity.Property(e => e.BitSidBitUsridCancelledUser).HasColumnName("BIT_SID__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitSidBitUsridEnterUser).HasColumnName("BIT_SID__BIT_USRID_EnterUser");
            entity.Property(e => e.BitSidBitUsridModUser).HasColumnName("BIT_SID__BIT_USRID_ModUser");
            entity.Property(e => e.BitSidCancelled).HasColumnName("BIT_SID_Cancelled");
            entity.Property(e => e.BitSidCancelledDate).HasColumnName("BIT_SID_CancelledDate");
            entity.Property(e => e.BitSidCancelledTime).HasColumnName("BIT_SID_CancelledTime");
            entity.Property(e => e.BitSidEnterDate).HasColumnName("BIT_SID_EnterDate");
            entity.Property(e => e.BitSidEnterTime).HasColumnName("BIT_SID_EnterTime");
            entity.Property(e => e.BitSidModDate).HasColumnName("BIT_SID_ModDate");
            entity.Property(e => e.BitSidModTime).HasColumnName("BIT_SID_ModTime");
            entity.Property(e => e.BitSidQuantity).HasColumnName("BIT_SID_Quantity");
            entity.Property(e => e.BitSidStatus).HasColumnName("BIT_SID_Status");
            entity.Property(e => e.BitSidUnitPrice).HasColumnName("BIT_SID_UnitPrice");

            entity.HasOne(d => d.BitSidBitItc).WithMany(p => p.BitSidSupplierInvoiceDetails)
                .HasForeignKey(d => d.BitSidBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_SID_SupplierInvoiceDetails_BIT_SID__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");

            entity.HasOne(d => d.BitSidBitSih).WithMany(p => p.BitSidSupplierInvoiceDetails)
                .HasForeignKey(d => d.BitSidBitSihid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_SID_SupplierInvoiceDetails_BIT_SID__BIT_SIHID_BIT_SIH_SupplierInvoiceHeader_BIT_SIH_ID");
        });

        modelBuilder.Entity<BitSihSupplierInvoiceHeader>(entity =>
        {
            entity.HasKey(e => e.BitSihId).HasName("PK__BIT_SUP1__9B6F70C9D31536E8");

            entity.ToTable("BIT_SIH_SupplierInvoiceHeader");

            entity.Property(e => e.BitSihId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_SIH_ID");
            entity.Property(e => e.BitSihBitUsridCancelledUser).HasColumnName("BIT_SIH__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitSihBitUsridEnterUser).HasColumnName("BIT_SIH__BIT_USRID_EnterUser");
            entity.Property(e => e.BitSihBitUsridModUser).HasColumnName("BIT_SIH__BIT_USRID_ModUser");
            entity.Property(e => e.BitSihCancelled).HasColumnName("BIT_SIH_Cancelled");
            entity.Property(e => e.BitSihCancelledDate).HasColumnName("BIT_SIH_CancelledDate");
            entity.Property(e => e.BitSihCancelledTime).HasColumnName("BIT_SIH_CancelledTime");
            entity.Property(e => e.BitSihEnterDate).HasColumnName("BIT_SIH_EnterDate");
            entity.Property(e => e.BitSihEnterTime).HasColumnName("BIT_SIH_EnterTime");
            entity.Property(e => e.BitSihModDate).HasColumnName("BIT_SIH_ModDate");
            entity.Property(e => e.BitSihModTime).HasColumnName("BIT_SIH_ModTime");
            entity.Property(e => e.BitSihStatus).HasColumnName("BIT_SIH_Status");
            entity.Property(e => e.BitSihSupplierId).HasColumnName("BIT_SIH_SupplierID");
            entity.Property(e => e.BitSihSupplierInvDate).HasColumnName("BIT_SIH_SupplierInvDate");
            entity.Property(e => e.BitSihSupplierInvNo).HasColumnName("BIT_SIH_SupplierInvNo");
            entity.Property(e => e.BitSihSupplierInvoiceAmountNet).HasColumnName("BIT_SIH_SupplierInvoiceAmountNet");
        });

        modelBuilder.Entity<BitStkStock>(entity =>
        {
            entity.HasKey(e => e.BitStkId).HasName("PK__BIT_STK___CD8A00C82AAA65B0");

            entity.ToTable("BIT_STK_Stock");

            entity.Property(e => e.BitStkId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_STK_ID");
            entity.Property(e => e.BitStkAvailableQuantity).HasColumnName("BIT_STK_AvailableQuantity");
            entity.Property(e => e.BitStkBitItcid).HasColumnName("BIT_STK__BIT_ITCID");
            entity.Property(e => e.BitStkBitUsridCancelledUser).HasColumnName("BIT_STK__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitStkBitUsridEnterUser).HasColumnName("BIT_STK__BIT_USRID_EnterUser");
            entity.Property(e => e.BitStkBitUsridModUser).HasColumnName("BIT_STK__BIT_USRID_ModUser");
            entity.Property(e => e.BitStkCancelled).HasColumnName("BIT_STK_Cancelled");
            entity.Property(e => e.BitStkCancelledDate).HasColumnName("BIT_STK_CancelledDate");
            entity.Property(e => e.BitStkCancelledTime).HasColumnName("BIT_STK_CancelledTime");
            entity.Property(e => e.BitStkEnterDate).HasColumnName("BIT_STK_EnterDate");
            entity.Property(e => e.BitStkEnterTime).HasColumnName("BIT_STK_EnterTime");
            entity.Property(e => e.BitStkModDate).HasColumnName("BIT_STK_ModDate");
            entity.Property(e => e.BitStkModTime).HasColumnName("BIT_STK_ModTime");
            entity.Property(e => e.BitStkQuantityIn).HasColumnName("BIT_STK_QuantityIn");
            entity.Property(e => e.BitStkQuantityOut).HasColumnName("BIT_STK_QuantityOut");
            entity.Property(e => e.BitStkReservedQuantity).HasColumnName("BIT_STK_ReservedQuantity");
            entity.Property(e => e.BitStkStatus).HasColumnName("BIT_STK_Status");

            entity.HasOne(d => d.BitStkBitItc).WithMany(p => p.BitStkStocks)
                .HasForeignKey(d => d.BitStkBitItcid)
                .HasConstraintName("FK_BIT_STK_Stock_BIT_STK__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BitStkStockTransaction>(entity =>
        {
            entity.HasKey(e => e.BitStkId).HasName("PK__BIT_STK___CD8A00C8F1DD4556");

            entity.ToTable("BIT_STK_StockTransactions");

            entity.Property(e => e.BitStkId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_STK_ID");
            entity.Property(e => e.BitStkBitItcid).HasColumnName("BIT_STK__BIT_ITCID");
            entity.Property(e => e.BitStkBitUsridCancelledUser).HasColumnName("BIT_STK__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitStkBitUsridEnterUser).HasColumnName("BIT_STK__BIT_USRID_EnterUser");
            entity.Property(e => e.BitStkBitUsridModUser).HasColumnName("BIT_STK__BIT_USRID_ModUser");
            entity.Property(e => e.BitStkCancelled).HasColumnName("BIT_STK_Cancelled");
            entity.Property(e => e.BitStkCancelledDate).HasColumnName("BIT_STK_CancelledDate");
            entity.Property(e => e.BitStkCancelledTime).HasColumnName("BIT_STK_CancelledTime");
            entity.Property(e => e.BitStkEnterDate).HasColumnName("BIT_STK_EnterDate");
            entity.Property(e => e.BitStkEnterTime).HasColumnName("BIT_STK_EnterTime");
            entity.Property(e => e.BitStkItemQuantity).HasColumnName("BIT_STK_ItemQuantity");
            entity.Property(e => e.BitStkModDate).HasColumnName("BIT_STK_ModDate");
            entity.Property(e => e.BitStkModTime).HasColumnName("BIT_STK_ModTime");
            entity.Property(e => e.BitStkStatus).HasColumnName("BIT_STK_Status");
            entity.Property(e => e.BitStkTransactionNo).HasColumnName("BIT_STK_TransactionNo");
            entity.Property(e => e.BitStkTransactionType).HasColumnName("BIT_STK_TransactionType");

            entity.HasOne(d => d.BitStkBitItc).WithMany(p => p.BitStkStockTransactions)
                .HasForeignKey(d => d.BitStkBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_STK_StockTransactions_BIT_STK__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        modelBuilder.Entity<BitUfvUserFavorite>(entity =>
        {
            entity.HasKey(e => e.BitUfvId).HasName("PK__BIT_UFV___FE1EE66B4F35FA0D");

            entity.ToTable("BIT_UFV_UserFavorites");

            entity.Property(e => e.BitUfvId)
                .ValueGeneratedNever()
                .HasColumnName("BIT_UFV_ID");
            entity.Property(e => e.BitUfvBitItcid).HasColumnName("BIT_UFV__BIT_ITCID");
            entity.Property(e => e.BitUfvBitUsrid).HasColumnName("BIT_UFV__BIT_USRID");
            entity.Property(e => e.BitUfvBitUsridCancelledUser).HasColumnName("BIT_UFV__BIT_USRID_CancelledUser");
            entity.Property(e => e.BitUfvBitUsridEnterUser).HasColumnName("BIT_UFV__BIT_USRID_EnterUser");
            entity.Property(e => e.BitUfvBitUsridModUser).HasColumnName("BIT_UFV__BIT_USRID_ModUser");
            entity.Property(e => e.BitUfvCancelled).HasColumnName("BIT_UFV_Cancelled");
            entity.Property(e => e.BitUfvCancelledDate).HasColumnName("BIT_UFV_CancelledDate");
            entity.Property(e => e.BitUfvCancelledTime).HasColumnName("BIT_UFV_CancelledTime");
            entity.Property(e => e.BitUfvEnterDate).HasColumnName("BIT_UFV_EnterDate");
            entity.Property(e => e.BitUfvEnterTime).HasColumnName("BIT_UFV_EnterTime");
            entity.Property(e => e.BitUfvModDate).HasColumnName("BIT_UFV_ModDate");
            entity.Property(e => e.BitUfvModTime).HasColumnName("BIT_UFV_ModTime");
            entity.Property(e => e.BitUfvStatus).HasColumnName("BIT_UFV_Status");

            entity.HasOne(d => d.BitUfvBitItc).WithMany(p => p.BitUfvUserFavorites)
                .HasForeignKey(d => d.BitUfvBitItcid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BIT_UFV_UserFavorites_BIT_UFV__BIT_ITCID_BIT_ITC_ItemsColor_BIT_ITC_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
