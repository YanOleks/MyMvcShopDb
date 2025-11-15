using MyMvcShopDb.Core.DTO;

namespace MyMvcShopDb.Core.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<ManufacturerDto>> GetAllAsync();
        Task<ManufacturerDto?> GetByIdAsync(int id);
        Task<ManufacturerDto> CreateAsync(CreateManufacturerDto dto);
        Task<bool> UpdateAsync(int id, CreateManufacturerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}