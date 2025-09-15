namespace MarkaziaBITStore.Application.DTOs.ResponseDTOs
{
    public class ItemResponseDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string? NameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public int? Points { get; set; }
        public int? Status { get; set; }

        public CategoryResponseDto Category { get; set; } = null!;
        public List<ItemColorResponseDto> Colors { get; set; } = new();
    }
}
