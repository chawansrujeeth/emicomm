using EmiCommerce.DTO;
using EmiCommerce.Models;

public static class UserMapper
{
    public static User ToUserEntity(RegisterUserDto dto, string passwordHash)
    {
        return new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = dto.Role,
            Phone = dto.Phone,
            CreatedAt = DateTime.Now,
            IsActive = true
        };
    }
}
