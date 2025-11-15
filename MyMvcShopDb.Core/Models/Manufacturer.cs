using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.Core.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
