namespace MarkaziaBITStore.Application.DTOs.RequestDTOs
{
    public class ItemRequestDto
    {
        public required int CategoryId { get; set; }
        public required string NameEn { get; set; }
        public required string? NameAr { get; set; }
        public  string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public required int Points { get; set; }
        public int? Status { get; set; }
    }
}
