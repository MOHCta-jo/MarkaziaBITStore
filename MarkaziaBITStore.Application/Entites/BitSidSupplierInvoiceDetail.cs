using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitSidSupplierInvoiceDetail
{
    public int BitSidId { get; set; }

    public int BitSidBitSihid { get; set; }

    public int BitSidBitItcid { get; set; }

    public int? BitSidQuantity { get; set; }

    public double? BitSidUnitPrice { get; set; }

    public int? BitSidStatus { get; set; }

    public int BitSidBitUsridEnterUser { get; set; }

    public DateOnly BitSidEnterDate { get; set; }

    public TimeOnly BitSidEnterTime { get; set; }

    public int? BitSidBitUsridModUser { get; set; }

    public DateOnly? BitSidModDate { get; set; }

    public TimeOnly? BitSidModTime { get; set; }

    public int? BitSidBitUsridCancelledUser { get; set; }

    public bool? BitSidCancelled { get; set; }

    public DateOnly? BitSidCancelledDate { get; set; }

    public TimeOnly? BitSidCancelledTime { get; set; }

    public virtual BitItcItemsColor BitSidBitItc { get; set; } = null!;

    public virtual BitSihSupplierInvoiceHeader BitSidBitSih { get; set; } = null!;
}
