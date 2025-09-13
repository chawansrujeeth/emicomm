using EmiCommerce.DTO;
using EmiCommerce.Models;
using EmiCommerce.Repo;
using EmiCommerce.Mapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiCommerce.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return ProductMapper.MapProductsToDto(products);
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            return ProductMapper.MapProductToDto(product);
        }
        public async Task<ProductDto> AddAsync(ProductDto productDto)
        {
            var product = ProductMapper.MapDtoToProduct(productDto);
            product.CreatedAt = DateTime.Now; // Ensure CreatedAt is set for new products
            var added = await _repo.AddAsync(product);
            return ProductMapper.MapProductToDto(added);
        }
        public async Task<ProductDto> UpdateAsync(ProductDto productDto)
        {
            var product = ProductMapper.MapDtoToProduct(productDto);
            var updated = await _repo.UpdateAsync(product);
            return ProductMapper.MapProductToDto(updated);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
