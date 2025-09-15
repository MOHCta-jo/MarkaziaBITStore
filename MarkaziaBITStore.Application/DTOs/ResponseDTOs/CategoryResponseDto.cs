using System.Text.Json.Serialization;

namespace MarkaziaBITStore.Application.DTOs.ResponseDTOs
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string? NameAr { get; set; }
        public string? IconUrl { get; set; }
        public bool IsActive { get; set; }

        public List<ItemResponseDto> Items { get; set; } = new();
    }
}
