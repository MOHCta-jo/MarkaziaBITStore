using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitItmItem
{
    public int BitItmId { get; set; }

    public int BitItmBitCatid { get; set; }

    public string BitItmNameEn { get; set; } = null!;

    public string? BitItmNameAr { get; set; }

    public string? BitItmDescriptionEn { get; set; }

    public string? BitItmDescriptionAr { get; set; }

    public int? BitItmPoints { get; set; }

    public int? BitItmStatus { get; set; }

    public int BitItmBitUsridEnterUser { get; set; }

    public DateOnly BitItmEnterDate { get; set; }

    public TimeOnly BitItmEnterTime { get; set; }

    public int? BitItmBitUsridModUser { get; set; }

    public DateOnly? BitItmModDate { get; set; }

    public TimeOnly? BitItmModTime { get; set; }

    public int? BitItmBitUsridCancelledUser { get; set; }

    public bool? BitItmCancelled { get; set; }

    public DateOnly? BitItmCancelledDate { get; set; }

    public TimeOnly? BitItmCancelledTime { get; set; }

    public virtual ICollection<BitItcItemsColor> BitItcItemsColors { get; set; } = new List<BitItcItemsColor>();

    public virtual BitCatCategory BitItmBitCat { get; set; } = null!;
}
