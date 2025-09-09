using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitCrtCart
{
    public int BitCrtId { get; set; }

    public int BitCrtBitUsrid { get; set; }

    public int BitCrtBitItcid { get; set; }

    public int? BitCrtItemSource { get; set; }

    public int BitCrtQuantity { get; set; }

    public int? BitCrtStatus { get; set; }

    public int BitCrtBitUsridEnterUser { get; set; }

    public DateOnly BitCrtEnterDate { get; set; }

    public TimeOnly BitCrtEnterTime { get; set; }

    public DateOnly? BitCrtModDate { get; set; }

    public int? BitCrtBitUsridModUser { get; set; }

    public TimeOnly? BitCrtModTime { get; set; }

    public int? BitCrtBitUsridCancelledUser { get; set; }

    public bool? BitCrtCancelled { get; set; }

    public DateOnly? BitCrtCancelledDate { get; set; }

    public TimeOnly? BitCrtCancelledTime { get; set; }

    public virtual ICollection<BitCrlCartLog> BitCrlCartLogs { get; set; } = new List<BitCrlCartLog>();

    public virtual BitItcItemsColor BitCrtBitItc { get; set; } = null!;

    public virtual ICollection<BitOrdOrderDetail> BitOrdOrderDetails { get; set; } = new List<BitOrdOrderDetail>();
}
