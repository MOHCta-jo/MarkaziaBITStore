using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_ORU_OrderUpdate
{
    public int BIT_ORU_ID { get; set; }

    public int BIT_ORU__BIT_ORHID { get; set; }

    public string BIT_ORU_UpdateDescrption { get; set; } = null!;

    public int? BIT_ORU_Status { get; set; }

    public int BIT_ORU__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_ORU_EnterDate { get; set; }

    public TimeOnly BIT_ORU_EnterTime { get; set; }

    public int? BIT_ORU__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_ORU_ModDate { get; set; }

    public TimeOnly? BIT_ORU_ModTime { get; set; }

    public int? BIT_ORU__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_ORU_Cancelled { get; set; }

    public DateOnly? BIT_ORU_CancelledDate { get; set; }

    public TimeOnly? BIT_ORU_CancelledTime { get; set; }

    public virtual BIT_ORH_OrderHeader BIT_ORU__BIT_ORH { get; set; } = null!;
}
