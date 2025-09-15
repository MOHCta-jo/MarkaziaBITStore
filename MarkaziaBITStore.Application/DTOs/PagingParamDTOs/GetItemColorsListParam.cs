using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{
    public class GetItemColorsListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        ItemColorID (2,3)
        ItemID (4,5)
        ColorID (6,7)
        Status (8,9)
        ItemNameEn (10,11)
        ColorNameEn (12,13)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? ItemColorID { get; set; }
        public int? ItemID { get; set; }
        public int? ColorID { get; set; }
        public int? Status { get; set; }
        public string? ItemNameEn { get; set; }
        public string? ColorNameEn { get; set; }
    }
}
