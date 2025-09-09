namespace MarkaziaBITStore.RequestDTOs
{
    public class CategoryRequestDto
    {
        public string NameEn { get; set; } = null!;
        public string? NameAr { get; set; }
        public string? IconUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
