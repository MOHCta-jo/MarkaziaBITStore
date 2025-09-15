using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{
    public class GetCategoriesListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        CategoryID (2,3)
        NameEn (4,5)
        NameAr (6,7)
        IsActive (8,9)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? CategoryID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public bool? IsActive { get; set; }
    }
}
