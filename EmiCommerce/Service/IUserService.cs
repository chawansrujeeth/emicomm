using EmiCommerce.DTO;

namespace EmiCommerce.Service
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterUserDto dto);          // for user registration
        Task<UserDto?> LoginAsync(LoginDto dto);                  // for role-based login
        Task<UserDto?> AdminLoginAsync(AdminLoginDto dto);        // for admin login with username/email
    }
}
