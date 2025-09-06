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
            if (product == null) return null;
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }
        public static Product MapDtoToProduct(ProductDto dto)
        {
            if (dto == null) return null;
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }
        public static IEnumerable<ProductDto> MapProductsToDto(IEnumerable<Product> products)
        {
            return products?.Select(MapProductToDto);
        }
    }
}
