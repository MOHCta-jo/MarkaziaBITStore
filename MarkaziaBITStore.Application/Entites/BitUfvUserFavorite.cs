using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitUfvUserFavorite
{
    public int BitUfvId { get; set; }

    public int BitUfvBitUsrid { get; set; }

    public int BitUfvBitItcid { get; set; }

    public int? BitUfvStatus { get; set; }

    public int BitUfvBitUsridEnterUser { get; set; }

    public DateOnly BitUfvEnterDate { get; set; }

    public TimeOnly BitUfvEnterTime { get; set; }

    public int? BitUfvBitUsridModUser { get; set; }

    public DateOnly? BitUfvModDate { get; set; }

    public TimeOnly? BitUfvModTime { get; set; }

    public int? BitUfvBitUsridCancelledUser { get; set; }

    public bool? BitUfvCancelled { get; set; }

    public DateOnly? BitUfvCancelledDate { get; set; }

    public TimeOnly? BitUfvCancelledTime { get; set; }

    public virtual BitItcItemsColor BitUfvBitItc { get; set; } = null!;
}
