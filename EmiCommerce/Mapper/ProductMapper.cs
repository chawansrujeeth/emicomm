using EmiCommerce.DTO;
using EmiCommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmiCommerce.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto MapProductToDto(Product product)
        {
            if (product == null) return new ProductDto();
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt
            };
        }
        public static Product MapDtoToProduct(ProductDto dto)
        {
            if (dto == null) return new Product();
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name ?? string.Empty,
                Description = dto.Description ?? string.Empty,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = dto.CreatedAt ?? DateTime.Now
            };
        }
        public static IEnumerable<ProductDto> MapProductsToDto(IEnumerable<Product> products)
        {
            return products?.Select(MapProductToDto) ?? Enumerable.Empty<ProductDto>();
        }
    }
}
