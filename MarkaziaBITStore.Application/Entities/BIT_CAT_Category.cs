using MarkaziaBITStore.Shared.CustomAttributes;
using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_CAT_Category
{
    [NextId]
    public int BIT_CAT_ID { get; set; }

    public string BIT_CAT_NameEN { get; set; } = null!;

    public string? BIT_CAT_NameAR { get; set; }

    public string? BIT_CAT_IconURL { get; set; }

    public bool BIT_CAT_IsActive { get; set; }

    public int BIT_CAT__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_CAT_EnterDate { get; set; }

    public TimeOnly BIT_CAT_EnterTime { get; set; }

    public int? BIT_CAT__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_CAT_ModDate { get; set; }

    public TimeOnly? BIT_CAT_ModTime { get; set; }

    public int? BIT_CAT__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_CAT_Cancelled { get; set; }

    public DateOnly? BIT_CAT_CancelledDate { get; set; }

    public TimeOnly? BIT_CAT_CancelledTime { get; set; }

    public virtual ICollection<BIT_ITM_Items> BIT_ITM_Items { get; set; } = new List<BIT_ITM_Items>();
}
