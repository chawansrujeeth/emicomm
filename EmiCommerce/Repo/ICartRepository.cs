using EmiCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiCommerce.Repo
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<Cart> CreateCartAsync(int userId);
        Task<CartItem?> GetCartItemAsync(int cartId, int productId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> RemoveCartItemAsync(int cartItemId);
        Task<bool> ClearCartAsync(int cartId);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId);
        Task<decimal> GetCartTotalAsync(int cartId);
    }
}
