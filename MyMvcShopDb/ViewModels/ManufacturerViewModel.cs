using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.ViewModels
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Manufacturer name is required")]
        [Display(Name = "Manufacturer Name")]
        public required string Name { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }
    }
}