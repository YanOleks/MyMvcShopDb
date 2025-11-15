using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.Core.DTO
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}