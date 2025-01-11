using ExpenseTrackerv1.Data;
using ExpenseTrackerv1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerv1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult IndexCategory()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            ViewBag.Action = "Add";
            var model = new Category { };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (category.CategoryId == 0)
                    {
                        _context.Categories.Add(category);
                    }
                    else
                    {
                        _context.Categories.Update(category);
                    }
                    _context.SaveChanges();
                    return RedirectToAction("IndexCategory", "Category");
                }
                else
                {
                    // Log ModelState errors for debugging
                    foreach (var state in ModelState.Values)
                    {
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }
                    ViewBag.Action = category.CategoryId == 0 ? "Add" : "Update";
                    return View(category);  // Return the current view if ModelState is invalid
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return an error view or handle the error appropriately
                return View("Error", new { errorMessage = ex.Message });  // Show error view with exception details
            }
        }
    }
}
