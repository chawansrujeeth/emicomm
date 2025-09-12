using EmiCommerce.DTO;
using EmiCommerce.JWTHelper;
using System.Security.Cryptography;
using System.Text;

namespace EmiCommerce.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            // Duplicate checks to align with DB constraints and prevent SaveChanges errors
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            var existingUsername = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existingUsername != null)
                throw new Exception("Username already exists");

            var existingPhone = await _userRepository.GetByPhoneAsync(dto.Phone);
            if (existingPhone != null)
                throw new Exception("Phone already exists");

            var passwordHash = HashPassword(dto.Password);

            var user = UserMapper.ToUserEntity(dto, passwordHash);
            user.IsActive = true; // ensure non-nullable value for DB insert
            await _userRepository.AddUserAsync(user);

            return new UserDto
            {
                UserName = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = null 
            };
        }
        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailOnlyUserAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Email, user.Role);

            return new UserDto
            {
                UserName = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }
        
        public async Task<UserDto?> AdminLoginAsync(AdminLoginDto dto)
        {
            var user = await _userRepository.GetByUsernameOrEmailAsync(dto.UsernameOrEmail);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            // Ensure only admin role can login through this endpoint
            if (user.Role != "Admin")
                return null;

            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Email, user.Role);

            return new UserDto
            {
                UserName = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }
        
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string entered, string storedHash)
        {
            return HashPassword(entered) == storedHash;
        }
    }
}
