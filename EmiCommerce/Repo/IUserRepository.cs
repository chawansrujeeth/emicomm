using EmiCommerce.Models;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email); 
    Task<User?> GetByEmailOnlyUserAsync(string email);
    Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByPhoneAsync(string phone);
    Task AddUserAsync(User user);
}
