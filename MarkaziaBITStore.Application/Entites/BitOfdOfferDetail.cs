using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitOfdOfferDetail
{
    public int BitOfdId { get; set; }

    public int BitOfdBitOfhid { get; set; }

    public int BitOfdBitItcid { get; set; }

    public double? BitOfdItemPoints { get; set; }

    public double? BitOfdDiscount { get; set; }

    public double? BitOfdDiscountValue { get; set; }

    public double? BitOfdItemPointsNet { get; set; }

    public int? BitOfdStatus { get; set; }

    public int BitOfdBitUsridEnterUser { get; set; }

    public DateOnly BitOfdEnterDate { get; set; }

    public TimeOnly BitOfdEnterTime { get; set; }

    public int? BitOfdBitUsridModUser { get; set; }

    public DateOnly? BitOfdModDate { get; set; }

    public TimeOnly? BitOfdModTime { get; set; }

    public int? BitOfdBitUsridCancelledUser { get; set; }

    public bool? BitOfdCancelled { get; set; }

    public DateOnly? BitOfdCancelledDate { get; set; }

    public TimeOnly? BitOfdCancelledTime { get; set; }

    public virtual BitItcItemsColor BitOfdBitItc { get; set; } = null!;

    public virtual BitOfhOfferHeader BitOfdBitOfh { get; set; } = null!;

    public virtual ICollection<BitOrdOrderDetail> BitOrdOrderDetails { get; set; } = new List<BitOrdOrderDetail>();
}
