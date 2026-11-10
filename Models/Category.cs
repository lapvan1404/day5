using System.ComponentModel.DataAnnotations;

namespace day3.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;
        // Navigation property to products 
        public List<Product>? Products { get; set; }
    }
}
