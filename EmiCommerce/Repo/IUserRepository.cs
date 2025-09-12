using EmiCommerce.Models;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email); 
    Task<User?> GetByEmailOnlyUserAsync(string email);
    Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);
    Task AddUserAsync(User user);
    Task AddUserProfileAsync(UserProfile profile);
    Task<UserProfile?> GetProfileByUserIdAsync(Guid userId); 
}
