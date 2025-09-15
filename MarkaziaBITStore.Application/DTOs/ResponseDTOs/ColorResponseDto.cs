namespace MarkaziaBITStore.Application.DTOs.ResponseDTOs
{
    public class ColorResponseDto
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string HexCode { get; set; } = null!;

        public List<ItemColorResponseDto> ItemsColors { get; set; } = new();
    }
}
