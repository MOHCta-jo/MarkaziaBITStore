using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitColColor
{
    public int BitColId { get; set; }

    public string? BitColNameEn { get; set; }

    public string? BitColNameAr { get; set; }

    public string BitColHexCode { get; set; } = null!;

    public int BitColBitUsridEnterUser { get; set; }

    public DateOnly BitColEnterDate { get; set; }

    public TimeOnly BitColEnterTime { get; set; }

    public int? BitColBitUsridModUser { get; set; }

    public DateOnly? BitColModDate { get; set; }

    public TimeOnly? BitColModTime { get; set; }

    public int? BitColBitUsridCancelledUser { get; set; }

    public bool? BitColCancelled { get; set; }

    public DateOnly? BitColCancelledDate { get; set; }

    public TimeOnly? BitColCancelledTime { get; set; }

    public virtual ICollection<BitItcItemsColor> BitItcItemsColors { get; set; } = new List<BitItcItemsColor>();
}
