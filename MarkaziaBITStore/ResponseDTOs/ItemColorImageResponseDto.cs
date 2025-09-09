namespace MarkaziaBITStore.ResponseDTOs
{
    public class ItemColorImageResponseDto
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int? Status { get; set; }
    }
}
