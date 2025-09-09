using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitStkStockTransaction
{
    public int BitStkId { get; set; }

    public int BitStkBitItcid { get; set; }

    public int? BitStkTransactionType { get; set; }

    public int? BitStkTransactionNo { get; set; }

    public int? BitStkItemQuantity { get; set; }

    public int? BitStkStatus { get; set; }

    public int BitStkBitUsridEnterUser { get; set; }

    public DateOnly BitStkEnterDate { get; set; }

    public TimeOnly BitStkEnterTime { get; set; }

    public int? BitStkBitUsridModUser { get; set; }

    public DateOnly? BitStkModDate { get; set; }

    public TimeOnly? BitStkModTime { get; set; }

    public int? BitStkBitUsridCancelledUser { get; set; }

    public bool? BitStkCancelled { get; set; }

    public DateOnly? BitStkCancelledDate { get; set; }

    public TimeOnly? BitStkCancelledTime { get; set; }

    public virtual BitItcItemsColor BitStkBitItc { get; set; } = null!;
}
