using System.ComponentModel.DataAnnotations;

namespace EmiCommerce.DTO
{
    public class AddToCartDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
