using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entites;

public partial class BitOruOrderUpdate
{
    public int BitOruId { get; set; }

    public int BitOruBitOrhid { get; set; }

    public string BitOruUpdateDescrption { get; set; } = null!;

    public int? BitOruStatus { get; set; }

    public int BitOruBitUsridEnterUser { get; set; }

    public DateOnly BitOruEnterDate { get; set; }

    public TimeOnly BitOruEnterTime { get; set; }

    public int? BitOruBitUsridModUser { get; set; }

    public DateOnly? BitOruModDate { get; set; }

    public TimeOnly? BitOruModTime { get; set; }

    public int? BitOruBitUsridCancelledUser { get; set; }

    public bool? BitOruCancelled { get; set; }

    public DateOnly? BitOruCancelledDate { get; set; }

    public TimeOnly? BitOruCancelledTime { get; set; }

    public virtual BitOrhOrderHeader BitOruBitOrh { get; set; } = null!;
}
