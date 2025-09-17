using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_ICI_ItemsColorImages
{
    public int BIT_ICI_ID { get; set; }

    public int BIT_ICI__BIT_ITCID { get; set; }

    public int BIT_ICI_ScreenSequence { get; set; }

    public string BIT_ICI_ImageURL { get; set; } = null!;

    public bool BIT_ICI_IsDefault { get; set; }

    public int? BIT_ICI_Status { get; set; }

    public int BIT_ICI__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_ICI_EnterDate { get; set; }

    public TimeOnly BIT_ICI_EnterTime { get; set; }

    public int? BIT_ICI__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_ICI_ModDate { get; set; }

    public TimeOnly? BIT_ICI_ModTime { get; set; }

    public int? BIT_ICI__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_ICI_Cancelled { get; set; }

    public DateOnly? BIT_ICI_CancelledDate { get; set; }

    public TimeOnly? BIT_ICI_CancelledTime { get; set; }

    public virtual BIT_ITC_ItemsColor BIT_ICI__BIT_ITC { get; set; } = null!;
}
