namespace MarkaziaBITStore.Application.DTOs.ResponseDTOs
{
    public class ItemColorResponseDto
    {
        public int Id { get; set; }
        public int? Status { get; set; }

        public ColorResponseDto Color { get; set; } = null!;
        public ItemResponseDto Item { get; set; } = null!;
        public List<ItemColorImageResponseDto> Images { get; set; } = new();
    }
}
