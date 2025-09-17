using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_SIH_SupplierInvoiceHeader
{
    public int BIT_SIH_ID { get; set; }

    public int BIT_SIH_SupplierID { get; set; }

    public int BIT_SIH_SupplierInvNo { get; set; }

    public DateOnly BIT_SIH_SupplierInvDate { get; set; }

    public double BIT_SIH_SupplierInvoiceAmountNet { get; set; }

    public int? BIT_SIH_Status { get; set; }

    public int BIT_SIH__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_SIH_EnterDate { get; set; }

    public TimeOnly BIT_SIH_EnterTime { get; set; }

    public int? BIT_SIH__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_SIH_ModDate { get; set; }

    public TimeOnly? BIT_SIH_ModTime { get; set; }

    public int? BIT_SIH__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_SIH_Cancelled { get; set; }

    public DateOnly? BIT_SIH_CancelledDate { get; set; }

    public TimeOnly? BIT_SIH_CancelledTime { get; set; }

    public virtual ICollection<BIT_SID_SupplierInvoiceDetails> BIT_SID_SupplierInvoiceDetails { get; set; } = new List<BIT_SID_SupplierInvoiceDetails>();
}
