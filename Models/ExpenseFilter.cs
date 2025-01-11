using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerv1.Models
{
    public static class ExpenseFilter
    {
        public static async Task<List<Expense>> FilterExpenseAsync(
            IQueryable<Expense> expenses,
            bool? isRecurringFilter)
        {
            if(isRecurringFilter.HasValue && isRecurringFilter.Value)
            {
                expenses = expenses.Where(e => e.IsRecurring);
            }
            return await expenses.ToListAsync();
        }
    }
}
