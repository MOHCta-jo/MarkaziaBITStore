using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_ORH_OrderHeader
{
    public int BIT_ORH_ID { get; set; }

    public int BIT_ORH__MAS_USRID { get; set; }

    public int? BIT_ORH_OrderNumber { get; set; }

    public int? BIT_ORH_OrderSource { get; set; }

    public int? BIT_ORH_Status { get; set; }

    public int BIT_ORH__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_ORH_EnterDate { get; set; }

    public TimeOnly BIT_ORH_EnterTime { get; set; }

    public int? BIT_ORH__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_ORH_ModDate { get; set; }

    public TimeOnly? BIT_ORH_ModTime { get; set; }

    public int? BIT_ORH__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_ORH_Cancelled { get; set; }

    public DateOnly? BIT_ORH_CancelledDate { get; set; }

    public TimeOnly? BIT_ORH_CancelledTime { get; set; }

    public virtual ICollection<BIT_ORD_OrderDetails> BIT_ORD_OrderDetails { get; set; } = new List<BIT_ORD_OrderDetails>();

    public virtual ICollection<BIT_ORU_OrderUpdate> BIT_ORU_OrderUpdate { get; set; } = new List<BIT_ORU_OrderUpdate>();

    public virtual ICollection<BIT_OTR_OrderTrack> BIT_OTR_OrderTrack { get; set; } = new List<BIT_OTR_OrderTrack>();
}
