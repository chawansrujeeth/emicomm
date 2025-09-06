using EmiCommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiCommerce.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> AddAsync(ProductDto productDto);
        Task<ProductDto> UpdateAsync(ProductDto productDto);
        Task<bool> DeleteAsync(int id);
    }
}
