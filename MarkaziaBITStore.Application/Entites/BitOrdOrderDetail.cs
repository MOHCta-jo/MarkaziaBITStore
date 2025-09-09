using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitOrdOrderDetail
{
    public int BitOrdId { get; set; }

    public int BitOrdBitOrhid { get; set; }

    public int BitOrdBitItcid { get; set; }

    public int? BitOrdBitCrtid { get; set; }

    public int? BitOrdBitOfdid { get; set; }

    public int? BitOrdQuantity { get; set; }

    public int? BitOrdPoints { get; set; }

    public int? BitOrdStatus { get; set; }

    public int BitOrdBitUsridEnterUser { get; set; }

    public DateOnly BitOrdEnterDate { get; set; }

    public TimeOnly BitOrdEnterTime { get; set; }

    public int? BitOrdBitUsridModUser { get; set; }

    public DateOnly? BitOrdModDate { get; set; }

    public TimeOnly? BitOrdModTime { get; set; }

    public int? BitOrdBitUsridCancelledUser { get; set; }

    public bool? BitOrdCancelled { get; set; }

    public DateOnly? BitOrdCancelledDate { get; set; }

    public TimeOnly? BitOrdCancelledTime { get; set; }

    public virtual BitCrtCart? BitOrdBitCrt { get; set; }

    public virtual BitItcItemsColor BitOrdBitItc { get; set; } = null!;

    public virtual BitOfdOfferDetail? BitOrdBitOfd { get; set; }

    public virtual BitOrhOrderHeader BitOrdBitOrh { get; set; } = null!;
}
