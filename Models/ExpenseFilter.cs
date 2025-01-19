using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerv1.Models
{
    public static class ExpenseFilter
    {

        public static IQueryable<Expense> FilterExpense(IQueryable<Expense> expenses, bool? isRecurringFilter, 
                                                            DateTime? startDate, DateTime? endDate)
        {
            if (isRecurringFilter.HasValue)
            {
                expenses = expenses.Where(e => e.IsRecurring == isRecurringFilter.Value);
            }

            if(startDate.HasValue)
            {
                expenses = expenses.Where(e => e.ExpenseDate >= startDate.Value);
            }

            if(endDate.HasValue)
            {
                expenses = expenses.Where(e => e.ExpenseDate <= endDate.Value);
            }
            return expenses;
        }

        public static IQueryable<Expense> SortExpense(IQueryable<Expense> expenses, string? sortOrder)
        {
            if(sortOrder == "amount_asc")
            {
                expenses = expenses.OrderBy(e => e.Amount);
            }
            else if(sortOrder == "amount_desc")
            {
                expenses = expenses.OrderByDescending(e => e.Amount);
            }
            else if(sortOrder == "category_asc")
            {
                expenses = expenses.OrderBy(e => e.Category.CategoryName);
            }
            else if(sortOrder == "category_desc")
            {
                expenses = expenses.OrderByDescending(e => e.Category.CategoryName);
            }
            else if(sortOrder == "date_asc")
            {
                expenses = expenses.OrderBy(e => e.ExpenseDate);
            }
            else if(sortOrder == "date_desc")
            {
                expenses = expenses.OrderByDescending(e => e.ExpenseDate);
            }
            return expenses;
        }

    }
}
