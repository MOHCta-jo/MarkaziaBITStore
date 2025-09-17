using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_OTR_OrderTrack
{
    public int BIT_OTR_ID { get; set; }

    public int BIT_OTR__BIT_ORHID { get; set; }

    public int? BIT_OTR_OrderStatus { get; set; }

    public int? BIT_OTR_Status { get; set; }

    public int BIT_OTR__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_OTR_EnterDate { get; set; }

    public TimeOnly BIT_OTR_EnterTime { get; set; }

    public int? BIT_OTR__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_OTR_ModDate { get; set; }

    public TimeOnly? BIT_OTR_ModTime { get; set; }

    public int? BIT_OTR__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_OTR_Cancelled { get; set; }

    public DateOnly? BIT_OTR_CancelledDate { get; set; }

    public TimeOnly? BIT_OTR_CancelledTime { get; set; }

    public virtual BIT_ORH_OrderHeader BIT_OTR__BIT_ORH { get; set; } = null!;
}
