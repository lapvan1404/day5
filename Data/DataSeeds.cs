using day3.Models;

namespace day3.Data
{
    public class DataSeeds
    {
        // gia lap csdl 
        public static List<Category> categories = new List<Category>()
        {
            new Category() { Id = 1, Name = "Laptop" },
            new Category() { Id = 2, Name = "Phone" },
            new Category() { Id = 3, Name = "Tablet" },
        };

        public static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "MacBook Pro", Price = 1999.99m, Description = "High-performance laptop", CategoryId = 1 },
            new Product() { Id = 2, Name = "iPhone 13", Price = 999.99m, Description = "Latest smartphone", CategoryId = 2 },
            new Product() { Id = 3, Name = "iPad Air", Price = 599.99m, Description = "Versatile tablet", CategoryId = 3 },
        };
    }
}
