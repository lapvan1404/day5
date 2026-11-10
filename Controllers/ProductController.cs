using day3.Data;
using day3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace day3.Controllers
{
    public class ProductController : Controller
    {
        //gia lap csdl
        public static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Laptop", Price = 1000, Description = "High-performance laptop", CategoryId = 1 },
            new Product() { Id = 2, Name = "Phone", Price = 500, Description = "Latest smartphone", CategoryId = 2 },
            new Product() { Id = 3, Name = "Tablet", Price = 300, Description = "Portable tablet", CategoryId = 3 },
        };
        // GET: ProductController
        public ActionResult Index(string? searchString, string? sortOrder, int pageNumber = 1)
        {
            const int pageSize = 2;
            sortOrder ??= "name";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentSearch"] = searchString ?? string.Empty;

            var product = products.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            IEnumerable<ProductViewModel> result = product.Select(ToViewModel);
            result = sortOrder switch
            {
                "name" => result.OrderBy(p => p.Name),
                "name_desc" => result.OrderByDescending(p => p.Name),
                "price" => result.OrderBy(p => p.Price),
                "price_desc" => result.OrderByDescending(p => p.Price),
                "category" => result.OrderBy(p => p.CategoryName),
                "category_desc" => result.OrderByDescending(p => p.CategoryName),
                _ => result.OrderBy(p => p.Name)
            };

            var totalPages = Math.Max(1, (int)Math.Ceiling(result.Count() / (double)pageSize));
            pageNumber = Math.Clamp(pageNumber, 1, totalPages);
            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        private static ProductViewModel ToViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryName = DataSeeds.categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name ?? string.Empty
            };
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            product.Id = products.Count + 1;
            products.Add(product);
            return RedirectToAction(nameof(Index));

        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCondition(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
