using EmiCommerce.DTO;

namespace EmiCommerce.Service
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterUserDto dto);          // for user registration
        Task<UserDto?> ValidateUserAsync(LoginDto dto);           // for user login
        Task<UserDto?> AdminLoginAsync(LoginDto dto);             // for admin login
    }
}
