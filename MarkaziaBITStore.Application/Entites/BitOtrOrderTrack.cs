using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitOtrOrderTrack
{
    public int BitOtrId { get; set; }

    public int BitOtrBitOrhid { get; set; }

    public int? BitOtrOrderStatus { get; set; }

    public int? BitOtrStatus { get; set; }

    public int BitOtrBitUsridEnterUser { get; set; }

    public DateOnly BitOtrEnterDate { get; set; }

    public TimeOnly BitOtrEnterTime { get; set; }

    public int? BitOtrBitUsridModUser { get; set; }

    public DateOnly? BitOtrModDate { get; set; }

    public TimeOnly? BitOtrModTime { get; set; }

    public int? BitOtrBitUsridCancelledUser { get; set; }

    public bool? BitOtrCancelled { get; set; }

    public DateOnly? BitOtrCancelledDate { get; set; }

    public TimeOnly? BitOtrCancelledTime { get; set; }

    public virtual BitOrhOrderHeader BitOtrBitOrh { get; set; } = null!;
}
