using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{
    public class GetColorsListResult
    {
        public int ColorID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string HexCode { get; set; } = null!;
    }
}
