using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class BIT_OFH_OfferHeader
{
    public int BIT_OFH_ID { get; set; }

    public string BIT_OFH_OfferName { get; set; } = null!;

    public DateOnly BIT_OFH_OfferStartDate { get; set; }

    public DateOnly BIT_OFH_OfferEndDate { get; set; }

    public int? BIT_OFH_Status { get; set; }

    public int BIT_OFH__MAS_USRID_EnterUser { get; set; }

    public DateOnly BIT_OFH_EnterDate { get; set; }

    public TimeOnly BIT_OFH_EnterTime { get; set; }

    public int? BIT_OFH__MAS_USRID_ModUser { get; set; }

    public DateOnly? BIT_OFH_ModDate { get; set; }

    public TimeOnly? BIT_OFH_ModTime { get; set; }

    public int? BIT_OFH__MAS_USRID_CancelledUser { get; set; }

    public bool? BIT_OFH_Cancelled { get; set; }

    public DateOnly? BIT_OFH_CancelledDate { get; set; }

    public TimeOnly? BIT_OFH_CancelledTime { get; set; }

    public virtual ICollection<BIT_OFD_OfferDetails> BIT_OFD_OfferDetails { get; set; } = new List<BIT_OFD_OfferDetails>();
}
