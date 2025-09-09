using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitItcItemsColor
{
    public int BitItcId { get; set; }

    public int BitItcBitItmid { get; set; }

    public int BitItcBitColid { get; set; }

    public int? BitItcStatus { get; set; }

    public int BitItcBitUsridEnterUser { get; set; }

    public DateOnly BitItcEnterDate { get; set; }

    public TimeOnly BitItcEnterTime { get; set; }

    public int? BitItcBitUsridModUser { get; set; }

    public DateOnly? BitItcModDate { get; set; }

    public TimeOnly? BitItcModTime { get; set; }

    public int? BitItcBitUsridCancelledUser { get; set; }

    public bool? BitItcCancelled { get; set; }

    public DateOnly? BitItcCancelledDate { get; set; }

    public TimeOnly? BitItcCancelledTime { get; set; }

    public virtual ICollection<BitCrlCartLog> BitCrlCartLogs { get; set; } = new List<BitCrlCartLog>();

    public virtual ICollection<BitCrtCart> BitCrtCarts { get; set; } = new List<BitCrtCart>();

    public  ICollection<BitIciItemsColorImage> BitIciItemsColorImages { get; set; } = new List<BitIciItemsColorImage>();

    public virtual BitColColor BitItcBitCol { get; set; } = null!;

    public virtual BitItmItem BitItcBitItm { get; set; } = null!;

    public virtual ICollection<BitOfdOfferDetail> BitOfdOfferDetails { get; set; } = new List<BitOfdOfferDetail>();

    public virtual ICollection<BitOrdOrderDetail> BitOrdOrderDetails { get; set; } = new List<BitOrdOrderDetail>();

    public virtual ICollection<BitSidSupplierInvoiceDetail> BitSidSupplierInvoiceDetails { get; set; } = new List<BitSidSupplierInvoiceDetail>();

    public virtual ICollection<BitStkStockTransaction> BitStkStockTransactions { get; set; } = new List<BitStkStockTransaction>();

    public virtual ICollection<BitStkStock> BitStkStocks { get; set; } = new List<BitStkStock>();

    public virtual ICollection<BitUfvUserFavorite> BitUfvUserFavorites { get; set; } = new List<BitUfvUserFavorite>();
}
