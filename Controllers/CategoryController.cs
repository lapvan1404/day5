using day3.Data;
using day3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace bai3.Controllers
{
    public class CategoryController : Controller
    {
    private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        // GET: CetegoryController
        [HttpGet]
        public ActionResult Index(string? sortOrder, int pageNumber = 1)
        {
            const int pageSize = 2;
            ViewData["CurrentSort"] = sortOrder ?? string.Empty;
            ViewData["IdSortParam"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";

            IEnumerable<Category> categories = DataSeeds.categories;
            categories = sortOrder switch
            {
                "id" => categories.OrderBy(c => c.Id),
                "id_desc" => categories.OrderByDescending(c => c.Id),
                "name" => categories.OrderBy(c => c.Name),
                "name_desc" => categories.OrderByDescending(c => c.Name),
                _ => categories
            };

            var totalPages = Math.Max(1, (int)Math.Ceiling(categories.Count() / (double)pageSize));
            pageNumber = Math.Clamp(pageNumber, 1, totalPages);
            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(categories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        // GET: CetegoryController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Fetching details for category with ID: {CategoryId}", id);
            var category = DataSeeds.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CetegoryController/Create
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

        // POST: CetegoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            { 
            category.Id = DataSeeds.categories.Count + 1;
            DataSeeds.categories.Add(category);
            return RedirectToAction(nameof(Index));
        }
            return View(category);
        }

        // GET: CetegoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var EditCategory = DataSeeds.categories.FirstOrDefault(c => c.Id == id);
            if (EditCategory == null)
            {
                return NotFound();
            }

            return View(EditCategory);
        }

        // POST: CetegoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            var existingCategory = DataSeeds.categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.Name = category.Name;
            return RedirectToAction(nameof(Index));
        }

        // GET: CetegoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = DataSeeds.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CetegoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = DataSeeds.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            DataSeeds.categories.Remove(category);
            return RedirectToAction(nameof(Index));
        }

    }
}
