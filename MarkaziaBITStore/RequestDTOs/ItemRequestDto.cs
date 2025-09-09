namespace MarkaziaBITStore.RequestDTOs
{
    public class ItemRequestDto
    {
        public int CategoryId { get; set; }
        public string NameEn { get; set; } = null!;
        public string? NameAr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
        public int? Points { get; set; }
        public int? Status { get; set; }
    }
}
