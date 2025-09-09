namespace MarkaziaBITStore.RequestDTOs
{
    public class ItemColorImageRequestDto
    {
        public int Sequence { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int? Status { get; set; }
    }
}
