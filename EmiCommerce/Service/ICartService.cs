using EmiCommerce.DTO;
using System.Threading.Tasks;

namespace EmiCommerce.Service
{
    public interface ICartService
    {
        Task<CartDto> GetOrCreateCartAsync(int userId);
        Task<CartDto> AddToCartAsync(int userId, AddToCartDto addToCartDto);
        Task<CartDto> UpdateCartItemAsync(int userId, UpdateCartItemDto updateCartItemDto);
        Task<bool> RemoveFromCartAsync(int userId, int cartItemId);
        Task<bool> ClearCartAsync(int userId);
        Task<CartDto> GetCartAsync(int userId);
    }
}
