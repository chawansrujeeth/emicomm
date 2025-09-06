namespace EmiCommerce.DTO
{
    public class UserDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Token { get; set; }
    }
}
