using ExpenseTrackerv1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerv1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Transportation", MaxBudget = 50 },
                new Category { CategoryId = 2, CategoryName = "Food", MaxBudget = 200},
                new Category { CategoryId = 3, CategoryName = "Entertainment", MaxBudget = 100}
            );

            builder.Entity<Expense>().HasData(
                new Expense
                {
                    ExpenseId = 1,
                    Amount = 12.50,
                    ExpenseDate = new DateTime(2024, 12, 24),
                    UserId = "admin@gmail.com",
                    CategoryId = 1,
                    IsRecurring = true
                });
        }
    }
}