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
        public async Task<IActionResult> IndexExpense(bool? isRecurringFilter, string? sortOrder, 
                                                        DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Category = _context.Categories.FirstOrDefault();
            var userId = User.Identity.Name;
            var expense = _context.Expenses.Where(e => e.UserId == userId).Include(e => e.Category).AsQueryable();

            expense = ExpenseFilter.FilterExpense(expense, isRecurringFilter, startDate, endDate);
            expense = ExpenseFilter.SortExpense(expense, sortOrder);
            var expenseByCategory = await expense
                                        .GroupBy(e => e.Category.CategoryName)
                                        .Select(g => new
                                        {
                                            CategoryName = g.Key,
                                            TotalAmount = g.Sum(e => e.Amount)
                                        }).ToListAsync();
            ViewData["ExpenseByCategory"] = expenseByCategory;
            var sortedAndFilteredExpenses = await expense.ToListAsync();
            ViewData["IsRecurringFilter"] = isRecurringFilter;
            ViewData["SortOrder"] = sortOrder;
            ViewData["startDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["endDate"] = endDate?.ToString("yyyy-MM-dd");
            return View(sortedAndFilteredExpenses);
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

                    var totalExpenses = _context.Expenses
                                        .Where(e => e.CategoryId == expense.CategoryId)
                                        .Sum(e => e.Amount);

                    if(totalExpenses + expense.Amount > expense.Category.MaxBudget)
                    {
                        TempData["Notification"] = $"Caution: This expense will exceed the max budget for category '{expense.Category.CategoryName}." +
                            $" Total:'{totalExpenses + expense.Amount}, Max: {expense.Category.MaxBudget}'";
                    }


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
