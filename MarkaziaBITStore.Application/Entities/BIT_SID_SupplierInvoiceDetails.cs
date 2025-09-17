using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_SID_SupplierInvoiceDetails
{
    public int BIT_SID_ID { get; set; }

    public int BIT_SID__BIT_SIHID { get; set; }

    public int BIT_SID__BIT_ITCID { get; set; }

    public int BIT_SID_Quantity { get; set; }

    public double? BIT_SID_UnitPrice { get; set; }

    public int? BIT_SID_Status { get; set; }

    public int BIT_SID__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_SID_EnterDate { get; set; }

    public TimeOnly BIT_SID_EnterTime { get; set; }

    public int? BIT_SID__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_SID_ModDate { get; set; }

    public TimeOnly? BIT_SID_ModTime { get; set; }

    public int? BIT_SID__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_SID_Cancelled { get; set; }

    public DateOnly? BIT_SID_CancelledDate { get; set; }

    public TimeOnly? BIT_SID_CancelledTime { get; set; }

    public virtual BIT_ITC_ItemsColor BIT_SID__BIT_ITC { get; set; } = null!;

    public virtual BIT_SIH_SupplierInvoiceHeader BIT_SID__BIT_SIH { get; set; } = null!;
}
