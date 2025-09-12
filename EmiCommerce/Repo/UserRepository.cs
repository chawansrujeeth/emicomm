using EmiCommerce.Data;
using EmiCommerce.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.UserProfiles)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailOnlyUserAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(User user)
    {

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task AddUserProfileAsync(UserProfile profile)
    {
        await _context.UserProfiles.AddAsync(profile);
        await _context.SaveChangesAsync();
    }

    public async Task<UserProfile?> GetProfileByUserIdAsync(Guid userId)
    {
        return await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == usernameOrEmail || u.UserName == usernameOrEmail);
    }
}
