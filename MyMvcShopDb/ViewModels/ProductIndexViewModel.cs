using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.ViewModels
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public required string Name { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "Manufacturer")]
        public string? ManufacturerName { get; set; }
    }
}