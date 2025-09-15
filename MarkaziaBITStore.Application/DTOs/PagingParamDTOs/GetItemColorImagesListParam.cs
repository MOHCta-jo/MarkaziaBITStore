using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{
    public class GetItemColorImagesListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        ImageID (2,3)
        ItemColorID (4,5)
        Sequence (6,7)
        IsDefault (8,9)
        Status (10,11)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? ImageID { get; set; }
        public int? ItemColorID { get; set; }
        public int? Sequence { get; set; }
        public bool? IsDefault { get; set; }
        public int? Status { get; set; }
    }
}
