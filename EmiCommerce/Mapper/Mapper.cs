using EmiCommerce.DTO;
using EmiCommerce.Models;

public static class UserMapper
{
    public static User ToUserEntity(RegisterUserDto dto, string passwordHash)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = "User"
        };
    }

    public static UserProfile ToUserProfileEntity(RegisterUserDto dto, Guid userId)
    {
        return new UserProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FullName = dto.FullName,
            MobileNumber = dto.MobileNumber,
            Dob = dto.Dob
        };
    }
    public static User ToUserEntity(RegisterUserDto dto, string passwordHash, string role = "User")
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = role
        };
    }
}
