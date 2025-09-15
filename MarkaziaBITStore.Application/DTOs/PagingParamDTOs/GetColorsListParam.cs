using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{
    public class GetColorsListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        ColorID (2,3)
        NameEn (4,5)
        NameAr (6,7)
        HexCode (8,9)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? ColorID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? HexCode { get; set; }
    }
}
