using MarkaziaWebCommon.Utils.CustomAttribute;
using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_ITC_ItemsColor
{
    [NextId]
    public int BIT_ITC_ID { get; set; }

    public int BIT_ITC__BIT_ITMID { get; set; }

    public int BIT_ITC__BIT_COLID { get; set; }

    public int? BIT_ITC_Status { get; set; }

    public int BIT_ITC__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_ITC_EnterDate { get; set; }

    public TimeOnly BIT_ITC_EnterTime { get; set; }

    public int? BIT_ITC__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_ITC_ModDate { get; set; }

    public TimeOnly? BIT_ITC_ModTime { get; set; }

    public int? BIT_ITC__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_ITC_Cancelled { get; set; }

    public DateOnly? BIT_ITC_CancelledDate { get; set; }

    public TimeOnly? BIT_ITC_CancelledTime { get; set; }

    public virtual ICollection<BIT_CRL_CartLog> BIT_CRL_CartLog { get; set; } = new List<BIT_CRL_CartLog>();

    public virtual ICollection<BIT_CRT_Cart> BIT_CRT_Cart { get; set; } = new List<BIT_CRT_Cart>();

    public virtual ICollection<BIT_ICI_ItemsColorImages> BIT_ICI_ItemsColorImages { get; set; } = new List<BIT_ICI_ItemsColorImages>();

    public virtual BIT_COL_Colors BIT_ITC__BIT_COL { get; set; } = null!;

    public virtual BIT_ITM_Items BIT_ITC__BIT_ITM { get; set; } = null!;

    public virtual ICollection<BIT_OFD_OfferDetails> BIT_OFD_OfferDetails { get; set; } = new List<BIT_OFD_OfferDetails>();

    public virtual ICollection<BIT_ORD_OrderDetails> BIT_ORD_OrderDetails { get; set; } = new List<BIT_ORD_OrderDetails>();

    public virtual ICollection<BIT_SID_SupplierInvoiceDetails> BIT_SID_SupplierInvoiceDetails { get; set; } = new List<BIT_SID_SupplierInvoiceDetails>();

    public virtual ICollection<BIT_STK_Stock> BIT_STK_Stock { get; set; } = new List<BIT_STK_Stock>();

    public virtual ICollection<BIT_STK_StockTransactions> BIT_STK_StockTransactions { get; set; } = new List<BIT_STK_StockTransactions>();

    public virtual ICollection<BIT_UFV_UserFavorites> BIT_UFV_UserFavorites { get; set; } = new List<BIT_UFV_UserFavorites>();
}
