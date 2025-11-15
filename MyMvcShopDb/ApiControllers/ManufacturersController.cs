using Microsoft.AspNetCore.Mvc;
using MyMvcShopDb.Core.DTO;
using MyMvcShopDb.Core.Interfaces;

namespace MyMvcShopDb.Controllers.Api
{
    [Route("api/[controller]")] // Шлях: /api/manufacturers
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _repository;

        public ManufacturersController(IManufacturerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/manufacturers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        // GET: api/manufacturers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var manufacturer = await _repository.GetByIdAsync(id);
            if (manufacturer == null)
            {
                return NotFound(); // 404
            }
            return Ok(manufacturer); // 200
        }

        // POST: api/manufacturers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateManufacturerDto dto)
        {
            var newManufacturer = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newManufacturer.Id }, newManufacturer); // 201
        }

        // PUT: api/manufacturers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateManufacturerDto dto)
        {
            var success = await _repository.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound(); // 404
            }
            return NoContent(); // 204
        }

        // DELETE: api/manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success)
            {
                return NotFound(); // 404
            }
            return NoContent(); // 204
        }
    }
}