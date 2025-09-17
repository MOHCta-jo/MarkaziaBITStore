using MarkaziaWebCommon.Utils.CustomAttribute;
using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_ITM_Items
{
    [NextId]
    public int BIT_ITM_ID { get; set; }

    public int BIT_ITM__BIT_CATID { get; set; }

    public string BIT_ITM_NameEN { get; set; } = null!;

    public string? BIT_ITM_NameAR { get; set; }

    public string? BIT_ITM_DescriptionEN { get; set; }

    public string? BIT_ITM_DescriptionAR { get; set; }

    public int? BIT_ITM_Points { get; set; }

    public int? BIT_ITM_Status { get; set; }

    public int BIT_ITM__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_ITM_EnterDate { get; set; }

    public TimeOnly BIT_ITM_EnterTime { get; set; }

    public int? BIT_ITM__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_ITM_ModDate { get; set; }

    public TimeOnly? BIT_ITM_ModTime { get; set; }

    public int? BIT_ITM__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_ITM_Cancelled { get; set; }

    public DateOnly? BIT_ITM_CancelledDate { get; set; }

    public TimeOnly? BIT_ITM_CancelledTime { get; set; }

    public virtual ICollection<BIT_ITC_ItemsColor> BIT_ITC_ItemsColor { get; set; } = new List<BIT_ITC_ItemsColor>();

    public virtual BIT_CAT_Category BIT_ITM__BIT_CAT { get; set; } = null!;
}
