using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_OFD_OfferDetails
{
    public int BIT_OFD_ID { get; set; }

    public int BIT_OFD__BIT_OFHID { get; set; }

    public int BIT_OFD__BIT_ITCID { get; set; }

    public double? BIT_OFD_DiscountPercent { get; set; }

    public double? BIT_OFD_DiscountFixValue { get; set; }

    public int? BIT_OFD_Status { get; set; }

    public int BIT_OFD__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_OFD_EnterDate { get; set; }

    public TimeOnly BIT_OFD_EnterTime { get; set; }

    public int? BIT_OFD__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_OFD_ModDate { get; set; }

    public TimeOnly? BIT_OFD_ModTime { get; set; }

    public int? BIT_OFD__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_OFD_Cancelled { get; set; }

    public DateOnly? BIT_OFD_CancelledDate { get; set; }

    public TimeOnly? BIT_OFD_CancelledTime { get; set; }

    public virtual BIT_ITC_ItemsColor BIT_OFD__BIT_ITC { get; set; } = null!;

    public virtual BIT_OFH_OfferHeader BIT_OFD__BIT_OFH { get; set; } = null!;

    public virtual ICollection<BIT_ORD_OrderDetails> BIT_ORD_OrderDetails { get; set; } = new List<BIT_ORD_OrderDetails>();
}
