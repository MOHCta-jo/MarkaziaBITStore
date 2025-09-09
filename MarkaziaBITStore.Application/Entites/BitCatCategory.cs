using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitCatCategory
{
    public int BitCatId { get; set; }

    public string BitCatNameEn { get; set; } = null!;

    public string? BitCatNameAr { get; set; }

    public string? BitCatIconUrl { get; set; }

    public bool BitCatIsActive { get; set; }

    public int BitCatBitUsridEnterUser { get; set; }

    public DateOnly BitCatEnterDate { get; set; }

    public TimeOnly BitCatEnterTime { get; set; }

    public int? BitCatBitUsridModUser { get; set; }

    public DateOnly? BitCatModDate { get; set; }

    public TimeOnly? BitCatModTime { get; set; }

    public int? BitCatBitUsridCancelledUser { get; set; }

    public bool? BitCatCancelled { get; set; }

    public DateOnly? BitCatCancelledDate { get; set; }

    public TimeOnly? BitCatCancelledTime { get; set; }

    public virtual ICollection<BitItmItem> BitItmItems { get; set; } = new List<BitItmItem>();
}
