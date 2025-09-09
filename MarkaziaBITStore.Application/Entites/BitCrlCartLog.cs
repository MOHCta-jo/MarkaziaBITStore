using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitCrlCartLog
{
    public int BitCrlId { get; set; }

    public int BitCrlBitCrtid { get; set; }

    public int BitCrlBitUsrid { get; set; }

    public int BitCrlBitItcid { get; set; }

    public int BitCrlQuantity { get; set; }

    public int? BitCrlPoints { get; set; }

    public int? BitCrlStatus { get; set; }

    public int BitCrlBitUsridEnterUser { get; set; }

    public DateOnly BitCrlEnterDate { get; set; }

    public TimeOnly BitCrlEnterTime { get; set; }

    public int? BitCrlBitUsridModUser { get; set; }

    public DateOnly? BitCrlModDate { get; set; }

    public TimeOnly? BitCrlModTime { get; set; }

    public int? BitCrlBitUsridCancelledUser { get; set; }

    public bool? BitCrlCancelled { get; set; }

    public DateOnly? BitCrlCancelledDate { get; set; }

    public TimeOnly? BitCrlCancelledTime { get; set; }

    public virtual BitCrtCart BitCrlBitCrt { get; set; } = null!;

    public virtual BitItcItemsColor BitCrlBitItc { get; set; } = null!;
}
