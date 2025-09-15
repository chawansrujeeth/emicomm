using EmiCommerce.DTO;
using EmiCommerce.Models;
using EmiCommerce.Repo;
using System.Threading.Tasks;
using System.Linq;

namespace EmiCommerce.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<CartDto> GetOrCreateCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            
            if (cart == null)
            {
                cart = await _cartRepository.CreateCartAsync(userId);
            }

            return await MapCartToDto(cart);
        }

        public async Task<CartDto> AddToCartAsync(int userId, AddToCartDto addToCartDto)
        {
            // Get or create cart
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = await _cartRepository.CreateCartAsync(userId);
            }

            // Get product details
            var product = await _productRepository.GetByIdAsync(addToCartDto.ProductId);
            if (product == null || product.Id == 0)
            {
                throw new ArgumentException("Product not found");
            }

            // Check if product is in stock
            if (product.Stock < addToCartDto.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock");
            }

            // Check if item already exists in cart
            var existingCartItem = await _cartRepository.GetCartItemAsync(cart.CartId, addToCartDto.ProductId);
            
            if (existingCartItem != null)
            {
                // Update quantity
                existingCartItem.Quantity += addToCartDto.Quantity;
                
                // Check stock again for updated quantity
                if (product.Stock < existingCartItem.Quantity)
                {
                    throw new InvalidOperationException("Insufficient stock for requested quantity");
                }
                
                await _cartRepository.UpdateCartItemAsync(existingCartItem);
            }
            else
            {
                // Add new item to cart
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    UnitPrice = product.Price,
                    AddedAt = DateTime.Now
                };
                
                await _cartRepository.AddCartItemAsync(cartItem);
            }

            // Return updated cart
            return await GetCartAsync(userId);
        }

        public async Task<CartDto> UpdateCartItemAsync(int userId, UpdateCartItemDto updateCartItemDto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new ArgumentException("Cart not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == updateCartItemDto.CartItemId);
            if (cartItem == null)
            {
                throw new ArgumentException("Cart item not found");
            }

            // Get product to check stock
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null || product.Stock < updateCartItemDto.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock");
            }

            cartItem.Quantity = updateCartItemDto.Quantity;
            await _cartRepository.UpdateCartItemAsync(cartItem);

            return await GetCartAsync(userId);
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int cartItemId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return false;
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
            if (cartItem == null)
            {
                return false;
            }

            return await _cartRepository.RemoveCartItemAsync(cartItemId);
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return false;
            }

            return await _cartRepository.ClearCartAsync(cart.CartId);
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Return empty cart
                return new CartDto
                {
                    UserId = userId,
                    CartItems = new List<CartItemDto>(),
                    TotalAmount = 0
                };
            }

            return await MapCartToDto(cart);
        }

        private async Task<CartDto> MapCartToDto(Cart cart)
        {
            var cartItems = await _cartRepository.GetCartItemsAsync(cart.CartId);
            var totalAmount = await _cartRepository.GetCartTotalAsync(cart.CartId);

            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt,
                CartItems = cartItems.Select(ci => new CartItemDto
                {
                    CartItemId = ci.CartItemId,
                    CartId = ci.CartId,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    ProductDescription = ci.Product.Description,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    TotalPrice = ci.Quantity * ci.UnitPrice,
                    AddedAt = ci.AddedAt
                }).ToList(),
                TotalAmount = totalAmount
            };
        }
    }
}
