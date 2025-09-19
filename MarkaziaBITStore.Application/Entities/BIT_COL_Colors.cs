using MarkaziaBITStore.Shared.CustomAttributes;
using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_COL_Colors
{
    [NextId]
    public int BIT_COL_ID { get; set; }

    public string? BIT_COL_NameEN { get; set; }

    public string? BIT_COL_NameAR { get; set; }

    public string BIT_COL_HexCode { get; set; } = null!;

    public int BIT_COL__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_COL_EnterDate { get; set; }

    public TimeOnly BIT_COL_EnterTime { get; set; }

    public int? BIT_COL__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_COL_ModDate { get; set; }

    public TimeOnly? BIT_COL_ModTime { get; set; }

    public int? BIT_COL__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_COL_Cancelled { get; set; }

    public DateOnly? BIT_COL_CancelledDate { get; set; }

    public TimeOnly? BIT_COL_CancelledTime { get; set; }

    public virtual ICollection<BIT_ITC_ItemsColor> BIT_ITC_ItemsColor { get; set; } = new List<BIT_ITC_ItemsColor>();
}
