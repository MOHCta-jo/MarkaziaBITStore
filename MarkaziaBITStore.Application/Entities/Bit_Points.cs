using System;
using System.Collections.Generic;

namespace MarkaziaBITStore.Application.Entities;

public partial class Bit_Points
{
    public int? ToUserID { get; set; }

    public string? UserName { get; set; }

    public int? Points { get; set; }
}
