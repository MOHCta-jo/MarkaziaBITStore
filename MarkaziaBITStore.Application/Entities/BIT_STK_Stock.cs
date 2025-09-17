using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_STK_Stock
{
    public int BIT_STK_ID { get; set; }

    public int? BIT_STK__BIT_ITCID { get; set; }

    public int? BIT_STK_QuantityIn { get; set; }

    public int? BIT_STK_QuantityOut { get; set; }

    public int? BIT_STK_ReservedQuantity { get; set; }

    public int? BIT_STK_AvailableQuantity { get; set; }

    public int? BIT_STK_Status { get; set; }

    public int BIT_STK__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_STK_EnterDate { get; set; }

    public TimeOnly BIT_STK_EnterTime { get; set; }

    public int? BIT_STK__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_STK_ModDate { get; set; }

    public TimeOnly? BIT_STK_ModTime { get; set; }

    public int? BIT_STK__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_STK_Cancelled { get; set; }

    public DateOnly? BIT_STK_CancelledDate { get; set; }

    public TimeOnly? BIT_STK_CancelledTime { get; set; }

    public virtual BIT_ITC_ItemsColor? BIT_STK__BIT_ITC { get; set; }
}
