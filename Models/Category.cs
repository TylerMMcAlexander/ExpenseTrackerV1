using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerv1.Models
{
    public class Category
    {

        [Key]
        [Required(ErrorMessage = "Category Is Lacking ID")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name.")]
        public string? CategoryName { get; set; } = string.Empty;
    }
}
