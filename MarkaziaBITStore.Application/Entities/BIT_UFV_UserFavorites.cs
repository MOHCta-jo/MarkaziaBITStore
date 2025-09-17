using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_UFV_UserFavorites
{
    public int BIT_UFV_ID { get; set; }

    public int BIT_UFV__MAS_USRID { get; set; }

    public int BIT_UFV__BIT_ITCID { get; set; }

    public int? BIT_UFV_Status { get; set; }

    public int BIT_UFV__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_UFV_EnterDate { get; set; }

    public TimeOnly BIT_UFV_EnterTime { get; set; }

    public int? BIT_UFV__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_UFV_ModDate { get; set; }

    public TimeOnly? BIT_UFV_ModTime { get; set; }

    public int? BIT_UFV__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_UFV_Cancelled { get; set; }

    public DateOnly? BIT_UFV_CancelledDate { get; set; }

    public TimeOnly? BIT_UFV_CancelledTime { get; set; }

    public virtual BIT_ITC_ItemsColor BIT_UFV__BIT_ITC { get; set; } = null!;
}
