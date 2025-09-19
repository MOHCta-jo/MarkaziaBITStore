using MarkaziaBITStore.Shared.CustomAttributes;
using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_CRT_Cart
{
    [NextId]
    public int BIT_CRT_ID { get; set; }

    public int BIT_CRT__MAS_USRID { get; set; }

    public int BIT_CRT__BIT_ITCID { get; set; }

    public int? BIT_CRT_ItemSource { get; set; }

    public int BIT_CRT_Quantity { get; set; }

    public int? BIT_CRT_Status { get; set; }

    public int BIT_CRT__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_CRT_EnterDate { get; set; }

    public TimeOnly BIT_CRT_EnterTime { get; set; }

    public int? BIT_CRT__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_CRT_ModDate { get; set; }

    public TimeOnly? BIT_CRT_ModTime { get; set; }

    public int? BIT_CRT__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_CRT_Cancelled { get; set; }

    public DateOnly? BIT_CRT_CancelledDate { get; set; }

    public TimeOnly? BIT_CRT_CancelledTime { get; set; }

    public virtual ICollection<BIT_CRL_CartLog> BIT_CRL_CartLog { get; set; } = new List<BIT_CRL_CartLog>();

    public virtual BIT_ITC_ItemsColor BIT_CRT__BIT_ITC { get; set; } = null!;

    public virtual ICollection<BIT_ORD_OrderDetails> BIT_ORD_OrderDetails { get; set; } = new List<BIT_ORD_OrderDetails>();
}
