using MarkaziaBITStore.Shared.CustomAttributes;
using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_CRL_CartLog
{
    [NextId]
    public int BIT_CRL_ID { get; set; }

    public int BIT_CRL__BIT_CRTID { get; set; }

    public int BIT_CRL__MAS_USRID { get; set; }

    public int BIT_CRL__BIT_ITCID { get; set; }

    public int BIT_CRL_Quantity { get; set; }

    public int? BIT_CRL_Points { get; set; }

    public int? BIT_CRL_Status { get; set; }

    public int BIT_CRL__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_CRL_EnterDate { get; set; }

    public TimeOnly BIT_CRL_EnterTime { get; set; }

    public int? BIT_CRL__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_CRL_ModDate { get; set; }

    public TimeOnly? BIT_CRL_ModTime { get; set; }

    public int? BIT_CRL__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_CRL_Cancelled { get; set; }

    public DateOnly? BIT_CRL_CancelledDate { get; set; }

    public TimeOnly? BIT_CRL_CancelledTime { get; set; }

    public virtual BIT_CRT_Cart BIT_CRL__BIT_CRT { get; set; } = null!;

    public virtual BIT_ITC_ItemsColor BIT_CRL__BIT_ITC { get; set; } = null!;
}
