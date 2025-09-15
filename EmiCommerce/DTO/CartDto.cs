using System;
using System.Collections.Generic;

namespace EmiCommerce.DTO
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public decimal TotalAmount { get; set; }
    }
}
