using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitSihSupplierInvoiceHeader
{
    public int BitSihId { get; set; }

    public int BitSihSupplierId { get; set; }

    public int BitSihSupplierInvNo { get; set; }

    public DateOnly BitSihSupplierInvDate { get; set; }

    public double BitSihSupplierInvoiceAmountNet { get; set; }

    public int? BitSihStatus { get; set; }

    public int BitSihBitUsridEnterUser { get; set; }

    public DateOnly BitSihEnterDate { get; set; }

    public TimeOnly BitSihEnterTime { get; set; }

    public int? BitSihBitUsridModUser { get; set; }

    public DateOnly? BitSihModDate { get; set; }

    public TimeOnly? BitSihModTime { get; set; }

    public int? BitSihBitUsridCancelledUser { get; set; }

    public bool? BitSihCancelled { get; set; }

    public DateOnly? BitSihCancelledDate { get; set; }

    public TimeOnly? BitSihCancelledTime { get; set; }

    public virtual ICollection<BitSidSupplierInvoiceDetail> BitSidSupplierInvoiceDetails { get; set; } = new List<BitSidSupplierInvoiceDetail>();
}
