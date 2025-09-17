using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_ORD_OrderDetails
{
    public int BIT_ORD_ID { get; set; }

    public int BIT_ORD__BIT_ORHID { get; set; }

    public int BIT_ORD__BIT_ITCID { get; set; }

    public int? BIT_ORD__BIT_CRTID { get; set; }

    public int? BIT_ORD__BIT_OFDID { get; set; }

    public int? BIT_ORD_Quantity { get; set; }

    public int? BIT_ORD_Points { get; set; }

    public int? BIT_ORD_Status { get; set; }

    public int BIT_ORD__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_ORD_EnterDate { get; set; }

    public TimeOnly BIT_ORD_EnterTime { get; set; }

    public int? BIT_ORD__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_ORD_ModDate { get; set; }

    public TimeOnly? BIT_ORD_ModTime { get; set; }

    public int? BIT_ORD__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_ORD_Cancelled { get; set; }

    public DateOnly? BIT_ORD_CancelledDate { get; set; }

    public TimeOnly? BIT_ORD_CancelledTime { get; set; }

    public virtual BIT_CRT_Cart? BIT_ORD__BIT_CRT { get; set; }

    public virtual BIT_ITC_ItemsColor BIT_ORD__BIT_ITC { get; set; } = null!;

    public virtual BIT_OFD_OfferDetails? BIT_ORD__BIT_OFD { get; set; }

    public virtual BIT_ORH_OrderHeader BIT_ORD__BIT_ORH { get; set; } = null!;
}
