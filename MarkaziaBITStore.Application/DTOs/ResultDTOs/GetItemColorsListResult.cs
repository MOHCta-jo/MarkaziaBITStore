using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{

    public class GetItemColorsListResult
    {
        public int ItemColorID { get; set; }
        public int? Status { get; set; }
        public int ItemID { get; set; }
        public string? ItemNameEn { get; set; }
        public string? ItemNameAr { get; set; }
        public int ColorID { get; set; }
        public string? ColorNameEn { get; set; }
        public string? ColorNameAr { get; set; }
        public string? ColorHexCode { get; set; }
        public int ImageCount { get; set; }
    }
}
