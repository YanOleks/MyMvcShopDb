using Microsoft.EntityFrameworkCore;
using MyMvcShopDb.Core.DTO;
using MyMvcShopDb.Core.Interfaces;
using MyMvcShopDb.Core.Models;
using MyMvcShopDb.Infrastructure.Data;

namespace MyMvcShopDb.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManufacturerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ManufacturerDto>> GetAllAsync()
        {
            return await _context.Manufacturers
                .Select(m => new ManufacturerDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Country = m.Country
                })
                .ToListAsync();
        }

        public async Task<ManufacturerDto?> GetByIdAsync(int id)
        {
            return await _context.Manufacturers
                .Where(m => m.Id == id)
                .Select(m => new ManufacturerDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Country = m.Country
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ManufacturerDto> CreateAsync(CreateManufacturerDto dto)
        {
            var manufacturer = new Manufacturer
            {
                Name = dto.Name,
                Country = dto.Country
            };

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            return new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country = manufacturer.Country
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateManufacturerDto dto)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return false;
            }

            manufacturer.Name = dto.Name;
            manufacturer.Country = dto.Country;

            _context.Entry(manufacturer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return false;
            }

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}