using EmiCommerce.DTO;
using EmiCommerce.Models;

public static class UserMapper
{
    public static User ToUserEntity(RegisterUserDto dto, string passwordHash)
    {
        return new User
        {
            UserName = dto.Username,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = dto.Role
        };
    }

    public static UserProfile ToUserProfileEntity(RegisterUserDto dto, int userId)
    {
        return new UserProfile
        {
            UserId = userId,
            FullName = null, // Not provided in new DTO structure
            MobileNumber = dto.Phone,
            Dob = null // Not provided in new DTO structure
        };
    }
}
