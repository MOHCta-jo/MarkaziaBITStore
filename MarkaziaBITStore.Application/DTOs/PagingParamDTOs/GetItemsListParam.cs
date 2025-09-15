using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{
    public class GetItemsListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        ItemID (2,3)
        NameEn (4,5)
        NameAr (6,7)
        Points (8,9)
        CategoryId (10,11)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? ItemID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public int? Points { get; set; }
        public int? CategoryId { get; set; }
    }
}
