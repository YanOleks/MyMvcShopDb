using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcShopDb.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]        
        public required string Name { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; } 

        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer? Manufacturer { get; set; }
    }
}
