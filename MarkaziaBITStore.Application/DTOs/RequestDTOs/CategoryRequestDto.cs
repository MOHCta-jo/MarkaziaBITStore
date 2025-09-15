namespace MarkaziaBITStore.Application.DTOs.RequestDTOs
{
    public class CategoryRequestDto
    {
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public required string IconUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
