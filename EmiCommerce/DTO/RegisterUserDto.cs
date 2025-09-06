namespace EmiCommerce.DTO
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public string? MobileNumber { get; set; }
        public DateOnly? Dob { get; set; }
    }
}
