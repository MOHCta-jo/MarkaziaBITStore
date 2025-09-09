using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitOrhOrderHeader
{
    public int BitOrhId { get; set; }

    public int BitOrhBitUsrid { get; set; }

    public int? BitOrhOrderNumber { get; set; }

    public int? BitOrhOrderSource { get; set; }

    public int? BitOrhStatus { get; set; }

    public int BitOrhBitUsridEnterUser { get; set; }

    public DateOnly BitOrhEnterDate { get; set; }

    public TimeOnly BitOrhEnterTime { get; set; }

    public int? BitOrhBitUsridModUser { get; set; }

    public DateOnly? BitOrhModDate { get; set; }

    public TimeOnly? BitOrhModTime { get; set; }

    public int? BitOrhBitUsridCancelledUser { get; set; }

    public bool? BitOrhCancelled { get; set; }

    public DateOnly? BitOrhCancelledDate { get; set; }

    public TimeOnly? BitOrhCancelledTime { get; set; }

    public virtual ICollection<BitOrdOrderDetail> BitOrdOrderDetails { get; set; } = new List<BitOrdOrderDetail>();

    public virtual ICollection<BitOruOrderUpdate> BitOruOrderUpdates { get; set; } = new List<BitOruOrderUpdate>();

    public virtual ICollection<BitOtrOrderTrack> BitOtrOrderTracks { get; set; } = new List<BitOtrOrderTrack>();
}
