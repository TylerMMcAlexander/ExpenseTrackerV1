using ExpenseTrackerv1.Data;
using ExpenseTrackerv1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace ExpenseTrackerv1.Controllers
{
    public class ExpenseController : Controller
    {

        private readonly ApplicationDbContext _context;
        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult IndexExpense()
        {
            ViewBag.Category = _context.Categories.FirstOrDefault();
            var userId = User.Identity.Name;
            var expense = _context.Expenses.Where(e => e.UserId == userId).Include(e => e.Category).ToList();
            return View(expense);
        }

        [HttpGet]
        public IActionResult AddExpense()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = _context.Categories.ToList();
            var model = new Expense
            {
                ExpenseDate = DateTime.Today
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult EditExpense(int id)
        {
            ViewBag.Action = "Update";
            ViewBag.Categories = _context.Categories.ToList();
            var expense = _context.Expenses.Find(id);
            return View("AddExpense", expense);
        }

        [HttpPost]
        public IActionResult AddExpense(Expense expense)
        {
            try
            {
                ViewBag.Categories = _context.Categories.ToList();
                var userId = User.Identity.Name;
                expense.UserId = userId;

                if (ModelState.IsValid)
                {

                    if (expense.ExpenseId == 0)
                    {
                        _context.Expenses.Add(expense);
                    }
                    else
                    {
                        _context.Expenses.Update(expense);
                    }
                    _context.SaveChanges();
                    return RedirectToAction("IndexExpense", "Expense");
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
                    return View(expense);  // Return the current view if ModelState is invalid
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Return an error view or handle the error appropriately
                return View("Error", new { errorMessage = ex.Message });  // Show error view with exception details
            }
        }

        [HttpGet]
        public IActionResult DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);
            return View(expense);
        }

        [HttpPost]
        public IActionResult DeleteExpense(Expense expense)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction("IndexExpense", "Expense");
        }
    }
}
