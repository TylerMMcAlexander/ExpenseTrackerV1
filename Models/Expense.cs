using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerv1.Models
{
    public class Expense
    {
        [Required(ErrorMessage = "Expense Id is required.")]
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "Please enter an expense amount.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Please enter an expense date.")]
        public DateTime ExpenseDate { get; set; }

        public string UserId { get; set; } = string.Empty;

        

        public Category? Category { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
    }
}
