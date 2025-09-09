using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitOfhOfferHeader
{
    public int BitOfhId { get; set; }

    public string? BitOfhOfferName { get; set; }

    public DateOnly BitOfhOfferStartDate { get; set; }

    public DateOnly BitOfhOfferEndDate { get; set; }

    public int? BitOfhStatus { get; set; }

    public int BitOfhBitUsridEnterUser { get; set; }

    public DateOnly BitOfhEnterDate { get; set; }

    public TimeOnly BitOfhEnterTime { get; set; }

    public int? BitOfhBitUsridModUser { get; set; }

    public DateOnly? BitOfhModDate { get; set; }

    public TimeOnly? BitOfhModTime { get; set; }

    public int? BitOfhBitUsridCancelledUser { get; set; }

    public bool? BitOfhCancelled { get; set; }

    public DateOnly? BitOfhCancelledDate { get; set; }

    public TimeOnly? BitOfhCancelledTime { get; set; }

    public virtual ICollection<BitOfdOfferDetail> BitOfdOfferDetails { get; set; } = new List<BitOfdOfferDetail>();
}
