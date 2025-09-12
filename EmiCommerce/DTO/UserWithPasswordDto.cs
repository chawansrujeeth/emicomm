namespace EmiCommerce.DTO
{
    public class UserWithPasswordDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordHash { get; set; }
    }
}
