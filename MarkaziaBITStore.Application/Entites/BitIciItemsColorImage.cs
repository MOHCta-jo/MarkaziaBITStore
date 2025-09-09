using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitIciItemsColorImage
{
    public int BitIciId { get; set; }

    public int BitIciBitItcid { get; set; }

    public int BitIciSequence { get; set; }

    public string BitIciImageUrl { get; set; } = null!;

    public bool BitIciIsDefault { get; set; }

    public int? BitIciStatus { get; set; }

    public int BitIciBitUsridEnterUser { get; set; }

    public DateOnly BitIciEnterDate { get; set; }

    public TimeOnly BitIciEnterTime { get; set; }

    public int? BitIciBitUsridModUser { get; set; }

    public DateOnly? BitIciModDate { get; set; }

    public TimeOnly? BitIciModTime { get; set; }

    public int? BitIciBitUsridCancelledUser { get; set; }

    public bool? BitIciCancelled { get; set; }

    public DateOnly? BitIciCancelledDate { get; set; }

    public TimeOnly? BitIciCancelledTime { get; set; }

    public virtual BitItcItemsColor BitIciBitItc { get; set; } = null!;
}
