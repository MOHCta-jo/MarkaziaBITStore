namespace MarkaziaBITStore.Application.DTOs.RequestDTOs
{
    public class ItemColorImageRequestDto
    {
        public required int Sequence { get; set; }
        public required string ImageUrl { get; set; }
        public required bool IsDefault { get; set; }
        public required int Status { get; set; }
    }
}
