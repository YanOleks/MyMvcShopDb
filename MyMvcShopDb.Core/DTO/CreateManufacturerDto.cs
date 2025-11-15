using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.Core.DTO
{
    public class CreateManufacturerDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }
    }
}