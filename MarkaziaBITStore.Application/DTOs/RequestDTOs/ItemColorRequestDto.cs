namespace MarkaziaBITStore.Application.DTOs.RequestDTOs
{
    public class ItemColorRequestDto
    {
        public required int ItemId { get; set; }
        public required int ColorId { get; set; }
        public  required int Status { get; set; }
    }

}
