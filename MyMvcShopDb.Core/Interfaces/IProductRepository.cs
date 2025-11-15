using MyMvcShopDb.Core.DTO;
using MyMvcShopDb.Core.DTOs;
namespace MyMvcShopDb.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
        Task<bool> UpdateProductAsync(int id, CreateProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}