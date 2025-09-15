using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{
    public class GetCategoriesListResult
    {
        public int CategoryID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? IconUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
