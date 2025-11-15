using System.ComponentModel.DataAnnotations;

namespace MyMvcShopDb.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        public required string Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}