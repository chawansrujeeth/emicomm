using System.ComponentModel.DataAnnotations;

namespace EmiCommerce.DTO
{
    public class UpdateCartItemDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CartItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
