using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.Core.DTO
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}