namespace MarkaziaBITStore.Application.DTOs
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int AvailablePoints { get; set; }
        public string Token { get; set; } = null!;
    }
}
